using EntitiesModels.DtoModels;
using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Config;
using FXKJ.Infrastructure.Log.Util;
using System;
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
                authInfo.Name = "测试用户";
                authInfo.PhoneNumber = "15255458934";
                authInfo.GuidId = new Guid("00000000-0000-0000-0000-00000000");
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
            //写入sql日志
            int result = 0;
            try
            {
                string conStr = GlobalParams.ReadConnectionString();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    //插入sql语句
                    string sqlStr = "insert into [dbo].[Log_LoginLog] ([LoginLogId],[CreateUserId],[CreateUserCode],[CreateUserName],[IpAddressName],[ServerHost],[ClientHost],[UserAgent],[OsVersion],[LoginTime],[LoginOutTime],[StandingTime]) values('" + log.LoginLogId + "','" + log.CreateUserId + "','" + log.CreateUserCode + "','" + log.CreateUserName + "','" + log.IpAddressName + "','" + log.ServerHost + "','" + log.ClientHost + "','" + log.UserAgent + "','" + log.OsVersion + "','" + log.LoginTime.ToString(DateTimeConfig.DateTimeFormatS) + "','','');";
                    using (SqlCommand cmd = new SqlCommand(sqlStr, con))
                    {
                        con.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }




            }
            catch (Exception ex)
            {

            }
            return result;
        }


    }
}
