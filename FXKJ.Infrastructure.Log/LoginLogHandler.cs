using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Auth.Auth;
using FXKJ.Infrastructure.Core.Helper;
using FXKJ.Infrastructure.Core.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web;
namespace FXKJ.Infrastructure.Log
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLogHandler : BaseHandler<LoginLog>
    {

        public LoginLogHandler()
            : base("LoginLogToDatabase")
        {
            AuthInfoViewModel authInfo = FormAuthenticationExtension.CurrentAuth();
            if (authInfo == null)
            {
                authInfo = new AuthInfoViewModel();
                authInfo.Name = "测试用户";
                authInfo.PhoneNumber = "15255458934";
                authInfo.GuidId = new Guid("00000000-0000-0000-0000-000000000000");
            }
            var request = HttpContext.Current.Request;
            log = new LoginLog
            {
                LoginLogId = Guid.NewGuid(),
                ServerHost = string.Format("{0}【{1}】", Dns.GetHostName(), Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString()),
                ClientHost = string.Format("{0}", Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString()),
                UserAgent = request.Browser.Browser + "【" + request.Browser.Version + "】",
                OsVersion = IpBrowserUtil.GetOsVersion(),
                LoginTime = DateTime.Now,
                IpAddressName = IpBrowserUtil.GetAddressByApi(),
                CreateTime = DateTime.Now,
                CreateUserId = authInfo.GuidId,
                CreateUserCode = authInfo.PhoneNumber,
                CreateUserName = authInfo.Name
            };


        }

        public override void WriteLog()
        {
            base.WriteLog();
            WriteLoginLogData(log);
        }

        private int WriteLoginLogData(LoginLog log)
        {
            int result = 0;
            //写入sql日志
            try
            {
                string sql = string.Format(@"insert into [dbo].[Log_LoginLog] 
                         (
                          LoginLogId,
                          ServerHost,
                          ClientHost,
                          UserAgent,
                          OsVersion,  
                          LoginTime,
                          IpAddressName,
                          CreateTime,
                          CreateUserId,
                          CreateUserCode,
                          CreateUserName
                         )
                         values (
                          @LoginLogId,
                          @ServerHost,
                          @ClientHost,
                          @UserAgent,
                          @OsVersion,  
                          @LoginTime,
                          @IpAddressName,
                          @CreateTime,
                          @CreateUserId,
                          @CreateUserCode,
                          @CreateUserName)");
                List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter{
                      ParameterName = "LoginLogId",
                      Value = log.LoginLogId,
                     },
                      new SqlParameter{
                      ParameterName = "ServerHost",
                      Value = log.ServerHost,
                     },
                        new SqlParameter{
                      ParameterName = "ClientHost",
                      Value = log.ClientHost,
                     },
                          new SqlParameter{
                      ParameterName = "UserAgent",
                      Value = log.UserAgent,
                     },
                        new SqlParameter{
                      ParameterName = "OsVersion",
                      Value = log.OsVersion,
                     },
                     new SqlParameter{
                      ParameterName = "LoginTime",
                      Value = log.LoginTime,
                        DbType=DbType.DateTime
                     },
                     new SqlParameter{
                      ParameterName = "IpAddressName",
                      Value = log.IpAddressName,
                     },
                      new SqlParameter{
                      ParameterName = "CreateTime",
                      Value = log.CreateTime,
                      DbType=DbType.DateTime
                     },
                       new SqlParameter{
                      ParameterName = "CreateUserId",
                      Value = log.CreateUserId,
                     },
                       new SqlParameter{
                      ParameterName = "CreateUserCode",
                      Value = log.CreateUserCode,
                     },  new SqlParameter{
                      ParameterName = "CreateUserName",
                      Value = log.CreateUserName,
                     }
                };
                result = SqlUtil.ExecuteNonQuery(GlobalParamsHelper.ReadConnectionString(), CommandType.Text, sql, list.ToArray());
            }
            catch (Exception ex)
            {

            }
            return result;

        }


    }
}
