using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Auth.Auth;
using FXKJ.Infrastructure.Config;
using FXKJ.Infrastructure.Core.Sql;
using FXKJ.Infrastructure.Core.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace FXKJ.Infrastructure.Log
{
    public class OperationLogHandler : BaseHandler<OperateLog>
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="httpRequestBase"></param>
        public OperationLogHandler(HttpRequestBase httpRequestBase)
            : base("OperateLogToDatabase")
        {

            AuthInfoViewModel authInfo = FormAuthenticationExtension.CurrentAuth();
            if (authInfo == null)
            {
                authInfo = new AuthInfoViewModel();
                authInfo.Name = "测试用户";
                authInfo.PhoneNumber = "15255458934";
                authInfo.GuidId = new Guid("00000000-0000-0000-0000-00000000");
            }
            var request =HttpContext.Current.Request;
            log = new OperateLog()
            {
                OperationLogId = Guid.NewGuid(),
                ServerHost = string.Format("{0}【{1}】", IpBrowserUtil.GetServerHost(), IpBrowserUtil.GetServerHostIp()),
                ClientHost = string.Format("{0}", IpBrowserUtil.GetClientIp()),
                RequestContentLength = httpRequestBase.ContentLength,
                RequestType = httpRequestBase.RequestType,
                UserAgent = httpRequestBase.UserAgent,
                CreateTime = DateTime.Now,
                CreateUserId = authInfo.GuidId,
                CreateUserCode = authInfo.PhoneNumber,
                CreateUserName = authInfo.Name
            };

            var inputStream = request.InputStream;
            var streamReader = new StreamReader(inputStream);
            var requestData = HttpUtility.UrlDecode(streamReader.ReadToEnd());
            log.RequestData = requestData;
            if (httpRequestBase.Url != null)
            {
                log.Url = httpRequestBase.Url.ToString();
            }
            if (httpRequestBase.UrlReferrer != null)
            {
                log.UrlReferrer = httpRequestBase.UrlReferrer.ToString();
            }
        }

        /// <summary>
        /// 执行时间
        /// </summary>
        public void ActionExecuted()
        {
            log.ActionExecutionTime = (DateTime.Now - log.CreateTime).TotalSeconds;
        }

        /// <summary>
        /// 页面展示时间
        /// </summary>
        /// <param name="responseBase"></param>
        public void ResultExecuted(HttpResponseBase responseBase)
        {
            log.ResponseStatus = responseBase.Status;
            //页面展示时间
            log.ResultExecutionTime = (DateTime.Now - log.CreateTime).TotalSeconds;
        }


        public override void WriteLog()
        {
            base.WriteLog();

            WriteOperateLog(log);
        }


        /// <summary>
        /// 操作日志
        /// </summary>
        private int WriteOperateLog(OperateLog log)
        {
            int result=0;  //接收sql返回的结果
            //写入sql日志
            try
            {
                string sql = string.Format(@"insert into [dbo].[Log_LoginLog] 
                         (
                          OperationLogId,
                          ServerHost,
                          ClientHost,
                          RequestContentLength,
                          RequestType,  
                          UserAgent,
                          CreateTime,
                          CreateUserId,
                          CreateUserCode,
                          CreateUserName
                         )
                         values (
                          @OperationLogId,
                          @ServerHost,
                          @ClientHost,
                          @RequestContentLength,
                          @RequestType,  
                          @UserAgent,
                          @CreateTime,
                          @CreateUserId,
                          @CreateUserCode,
                          @CreateUserName)");
                List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter{
                      ParameterName = "OperationLogId",
                      Value = log.OperationLogId,
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
                      ParameterName = "RequestContentLength",
                      Value = log.RequestContentLength,
                     },
                     new SqlParameter{
                      ParameterName = "RequestType",
                      Value = log.RequestType,
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
                result = SqlHelper.ExecuteNonQuery(GlobalParams.ReadConnectionString(), CommandType.Text, sql, list.ToArray());
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
