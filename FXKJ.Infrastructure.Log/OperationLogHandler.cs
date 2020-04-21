using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Auth.Auth;
using FXKJ.Infrastructure.Core.Helper;
using FXKJ.Infrastructure.Core.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Web;

namespace FXKJ.Infrastructure.Log
{
    public class OperationLogHandler : BaseHandler<OperateLog>
    {
        /// <summary>
        /// 操作浏览日志
        /// </summary>
        /// <param name="httpRequestBase"></param>
        public OperationLogHandler(HttpRequestMessage httpRequestBase)
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
            var request = HttpContext.Current.Request;
            log = new OperateLog()
            {
                OperationLogId = Guid.NewGuid(),
                ServerHost = string.Format("{0}【{1}】", IpBrowserUtil.GetServerHost(), IpBrowserUtil.GetServerHostIp()),
                ClientHost = string.Format("{0}", IpBrowserUtil.GetClientIp()),
                RequestContentLength = (int)httpRequestBase.Content.Headers.ContentLength,
                RequestType = httpRequestBase.Method.ToString(),
                UserAgent = httpRequestBase.Headers.UserAgent.ToString(),
                CreateTime = DateTime.Now,
                CreateUserId = authInfo.GuidId,
                CreateUserCode = authInfo.PhoneNumber,
                CreateUserName = authInfo.Name
            };

            var inputStream = request.InputStream;
            var streamReader = new StreamReader(inputStream);
            var requestData = HttpUtility.UrlDecode(streamReader.ReadToEnd());
            log.RequestData = requestData;
            if (httpRequestBase.RequestUri != null)
            {
                log.Url = httpRequestBase.RequestUri.AbsoluteUri;
            }
            log.Version = httpRequestBase.Version.ToString();
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
        public void ResultExecuted()
        {
            log.ResponseStatus = HttpContext.Current.Response.Status;
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
            int result = 0;  //接收sql返回的结果
            //写入sql日志
            try
            {
                string sql = string.Format(@"insert into [dbo].[Log_OperateLog] 
                         (
                          OperationLogId,
                          ServerHost,
                          ClientHost,
                          RequestContentLength,
                          RequestType,  
                          UserAgent,
                          Url,
                          Version,
                          RequestData,
                          ActionExecutionTime,
                          ResponseStatus,            
                          ResultExecutionTime,
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
                          @Url,
                          @Version,
                          @RequestData,
                          @ActionExecutionTime,
                          @ResponseStatus,            
                          @ResultExecutionTime,
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
                      ParameterName = "Url",
                      Value = log.Url,
                     },
                      new SqlParameter{
                      ParameterName = "Version",
                      Value = log.Version,
                     },
                     new SqlParameter{
                      ParameterName = "RequestData",
                      Value = log.RequestData,
                     },
                      new SqlParameter{
                      ParameterName = "ActionExecutionTime",
                      Value = log.ActionExecutionTime,
                     },
                      new SqlParameter{
                      ParameterName = "ResponseStatus",
                      Value = log.ResponseStatus,
                     },
                     new SqlParameter{
                      ParameterName = "ResultExecutionTime",
                      Value = log.ResultExecutionTime,
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
                result = SqlUtil.ExecuteNonQuery(GlobalParamsHelper.ReadConnectionString(), CommandType.Text, sql, list.ToArray());
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
