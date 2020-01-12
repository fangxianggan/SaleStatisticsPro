using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Config;
using FXKJ.Infrastructure.Log.LogModel;
using FXKJ.Infrastructure.Log.Util;
using System;
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

            PrincipalUser principalUser = new PrincipalUser
            {
                Name = "匿名用户",
                UserId = Guid.Empty
            };
            var current = HttpContext.Current;
            if (current != null)
            {
                principalUser = FormAuthenticationExtension.Current(HttpContext.Current.Request);
            }
            if (principalUser == null)
            {
                principalUser = new PrincipalUser()
                {
                    Name = "匿名用户",
                    UserId = Guid.Empty
                };
            }
            var request =System.Web.HttpContext.Current.Request;
            log = new OperateLog()
            {
                CreateUserId = principalUser.UserId,
                CreateUserCode = principalUser.Code,
                CreateUserName = principalUser.Name,
                OperationLogId = Guid.NewGuid(),
                OperateTime = DateTime.Now,
                ServerHost = String.Format("{0}【{1}】", IpBrowserUtil.GetServerHost(), IpBrowserUtil.GetServerHostIp()),
                ClientHost = String.Format("{0}", IpBrowserUtil.GetClientIp()),
                RequestContentLength = httpRequestBase.ContentLength,
                RequestType = httpRequestBase.RequestType,
                UserAgent = httpRequestBase.UserAgent
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
            log.ActionExecutionTime = (DateTime.Now - log.OperateTime).TotalSeconds;
        }

        /// <summary>
        /// 页面展示时间
        /// </summary>
        /// <param name="responseBase"></param>
        public void ResultExecuted(HttpResponseBase responseBase)
        {
            log.ResponseStatus = responseBase.Status;
            //页面展示时间
            log.ResultExecutionTime = (DateTime.Now - log.OperateTime).TotalSeconds;
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
                string conStr = GlobalParams.ReadConnectionString();
              
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    //插入sql语句
                    string sqlStr = "insert into [dbo].[Sys_OperationLog] ([OperationLogId],[OperateTime],[CreateUserId],[CreateUserCode],[CreateUserName],[ClientHost],[ServerHost],[RequestContentLength],[RequestType],[Url],[UrlReferrer],[RequestData],[UserAgent],[ControllerName],[ActionName],[ActionExecutionTime],[ResultExecutionTime],[ResponseStatus],[Describe]) values('" + log.OperationLogId + "','" + log.OperateTime.ToString(DateTimeConfig.DateTimeFormatS) + "','" + log.CreateUserId + "','" + log.CreateUserCode + "','" + log.CreateUserName + "','" + log.ClientHost + "','" + log.ServerHost + "'," + log.RequestContentLength + ",'" + log.RequestType + "','" + log.Url + "','" + log.UrlReferrer + "','" + log.RequestData + "','" + log.UserAgent + "','" + log.ControllerName + "','" + log.ActionName + "'," + log.ActionExecutionTime + "," + log.ResultExecutionTime + ",'" + log.ResponseStatus + "','" + log.Describe + "');";
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
