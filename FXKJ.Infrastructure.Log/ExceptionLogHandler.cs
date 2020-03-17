using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web;
using FXKJ.Infrastructure.Log.Log4NetWrite;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Config;
using FXKJ.Infrastructure.Log.LogModel;
using System.Net;

namespace FXKJ.Infrastructure.Log
{
    public class ExceptionLogHandler : BaseHandler<ExceptionLog>
    {
       
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">错误信息</param>
        public ExceptionLogHandler(Exception exception)
            : base("ExceptionLogToDatabase")
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
            log = new ExceptionLog
            {
                ExceptionLogId=Guid.NewGuid(),
                ExceptionTime = DateTime.Now,
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                ExceptionType = exception.GetType().FullName,
                CreateUserCode = principalUser.Code,
                CreateUserName = principalUser.Name,
                ServerHost = string.Format("{0}【{1}】", Dns.GetHostName(), Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString()),
                ClientHost = string.Format("{0}", Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString()),
                Runtime = "Web"
            };
            //获取服务器信息
            var request = HttpContext.Current.Request;
            log.RequestUrl = string.Format("{0} ", request.Url);
            log.HttpMethod = request.HttpMethod;
            log.UserAgent = request.UserAgent;
            //var inputStream = request.InputStream;
            //var streamReader = new StreamReader(inputStream);
            //var requestData = HttpUtility.UrlDecode(streamReader.ReadToEnd());
            //log.RequestData = requestData;
            log.InnerException = exception.InnerException != null ? GetExceptionFullMessage(exception.InnerException) : "";
        }

        /// <summary>
        /// 重写基类
        /// </summary>
        public override void WriteLog()
        {
         
            //写入基类
            base.WriteLog();

            //写入数据库
           // WriteExceptionLogData(log);

            string exceptionHtml = ExceptionHtml(log);
            //是否发送邮件
            //if (bool.Parse(GlobalParams.Get("errorSendMail").ToString()))
            //{
            //    string fromAccount = GlobalParams.Get("errorSendMailFromAccount").ToString();
            //    string fromPwd = GlobalParams.Get("errorSendMailFromPwd").ToString();
            //    string toAccount = GlobalParams.Get("errorSendMailToAccount").ToString();
            //    string toSmtp = GlobalParams.Get("errorSendMailSmtp").ToString();
            //    bool toSmtpSsl = bool.Parse(GlobalParams.Get("errorSendMailSmtpSsl").ToString());
            //    EmailUtil email = new EmailUtil(toAccount, fromAccount, "错误接受者", "系统错误提醒", "系统发生错误", exceptionHtml, true);
            //    email.SetSmtp(fromPwd, toSmtp, toSmtpSsl);
            //}
            //写入文本
            LogWriter.WriteLog(FolderName.Exception, exceptionHtml);
        }

        private int WriteExceptionLogData(ExceptionLog log)
        {
            //写入sql日志
            int result=0;
            try
            {
                string conStr = GlobalParams.ReadConnectionString();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    //插入sql语句
                    string sqlStr = "insert into [dbo].[Sys_ExceptionLog] ([ExceptionLogId],[ExceptionTime],[CreateUserId],[CreateUserCode],[CreateUserName],[Message],[StackTrace],[InnerException],[ExceptionType],[ServerHost],[ClientHost],[Runtime],[RequestUrl],[RequestData],[UserAgent],[HttpMethod]) values('" + log.ExceptionLogId + "','" + log.ExceptionTime.ToString(DateTimeConfig.DateTimeFormatS) + "','" + log.CreateUserId + "','" + log.CreateUserCode + "','" + log.CreateUserName + "','" + log.Message + "','" + log.StackTrace + "','" + log.InnerException + "','" + log.ExceptionType + "','" + log.ServerHost + "','" + log.ClientHost + "','" + log.Runtime + "','" + log.RequestUrl + "','" + log.RequestData + "','" + log.UserAgent + "','" + log.HttpMethod +"');";
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

        /// <summary>
        /// 获取异常Html
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private  string ExceptionHtml(ExceptionLog log)
        {
            string html = @"<style>
                                .edit-table {
                                    border-top: none;
                                    border-right: none;
                                    margin: 10px auto;
                                    width: 100%;
                                    background: #fff;
                                    border-collapse: collapse;
                                    border-spacing: 0;
                                }

                                    .edit-table th {
                                        background-color: #fff;
                                        border-bottom: 1px dashed #ccc;
                                        font-weight: normal;
                                        height: 27px;
                                        line-height: 27px;
                                        padding-right: 5px;
                                        text-align: right;
                                        white-space: nowrap;
                                        width: 80px;
                                    }

                                    .edit-table td {
                                        background-color: #fff;
                                        border-bottom: 1px dashed #ccc;
                                        height: 27px;
                                        /*line-height: 27px;*/
                                        padding: 2px;
                                        width: auto;
                                    }

                                    .edit-table input[type='text'],
                                    .edit-table input[type='password'] input[type='radio'],
                                    .edit-table input[type='checkbox'],
                                    .edit-table select,
                                    .edit-table textarea {
                                        width: 98%;
                                        border: 1px solid #C6C6C6;
                                        outline: none;
                                    }

                                        .edit-table input[type='text']:hover,
                                        .edit-table input[type='password'] input[type='radio']:hover,
                                        .edit-table input[type='checkbox']:hover,
                                        .edit-table select:hover,
                                        .edit-table textarea:hover {
                                            width: 98%;
                                            border: 1px solid #559AEE;
                                            outline: none;
                                        }

                                    .edit-table input[type='text'],
                                    .edit-table input[type='password'] {
                                        width: 98%;
                                        /*padding: 1px 5px;*/
                                        height: 20px;
                                    }

                                    .edit-table input[type='radio'] {
                                        position: relative;
                                        top: 2px;
                                        left: 10px;
                                        margin-right: 13px;
                                    }

                                    .edit-table input[type='checkbox'] {
                                        position: relative;
                                        top: 3px;
                                        left: 10px;
                                        margin-right: 13px;
                                    }

                                    .edit-table textarea {
                                        width: 98%;
                                        height: 100px;
                                        margin-top: 4px;
                                    }

                                    .edit-table select {
                                        width: 98%;
                                        height: 25px;
                                        line-height: 25px;
                                    }</style>";
            html += string.Format(@"

              <table class='edit-table'>
                <tbody>
                    <tr>
                        <th>
                            异常时间：
                        </th>
                        <td>
                            <label id='OperateTime'>{0}</label></td>
                    </tr>
                    <tr>
                        <th>
                            登录名：
                        </th>
                        <td>
                            <label id='Code'>{1}</label></td>
                    </tr>
                    <tr>
                        <th>
                            真实姓名：
                        </th>
                        <td>
                            <label id='Name'>{2}</label></td>
                    </tr>
                    <tr>
                        <th>
                            错误信息：
                        </th>
                        <td>
                            <label id='Message'>{3}</label></td>
                    </tr>
                    <tr>
                        <th>
                            堆栈信息：
                        </th>
                        <td>
                            <label id='StackTrace'>{4}</label></td>
                    </tr>
                    <tr>
                        <th>
                            内部异常：
                        </th>
                        <td>
                            <label id='InnerException'>{5}</label></td>
                    </tr>
                    <tr>
                        <th>
                            异常类型：
                        </th>
                        <td>
                            <label id='ExceptionType'>{6}</label></td>
                    </tr>
                    <tr>
                        <th>
                            请求Url：
                        </th>
                        <td>
                            <label id='RequestUrl'>{7}</label></td>
                    </tr>
                    <tr>
                        <th>
                            浏览器信息：
                        </th>
                        <td>
                            <label id='UserAgent'>{8}</label></td>
                    </tr>
                    <tr>
                        <th>
                            请求方式：
                        </th>
                        <td>
                            <label id='HttpMethod'>{9}</label></td>
                    </tr>
                    <tr>
                        <th>
                            请求数据：
                        </th>
                        <td>
                            <label id='RequestData'>
                        </label>{10}</td>
                    </tr>
                    <tr>
                        <th>
                            服务器：
                        </th>
                        <td>
                            <label id='ServerHost'>{11}</label></td>
                    </tr>
                    <tr>
                        <th>
                            客户端：
                        </th>
                        <td>
                            <label id='ClientHost'>{12}</label></td>
                    </tr>
                </tbody>
            </table>", log.ExceptionTime.ToString(CultureInfo.InvariantCulture), log.CreateUserCode, log.CreateUserName, log.Message, log.StackTrace, log.InnerException, log.ExceptionType, log.RequestUrl, log.UserAgent, log.HttpMethod, log.RequestData, log.ServerHost, log.ClientHost);
            return html;
        }

        /// <summary>
        /// 获取完整的异常消息，包括内部异常消息
        /// </summary>
        /// <returns></returns>
        private  string GetExceptionFullMessage(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }
            var message = new StringBuilder(exception.Message);
            var innerException = exception.InnerException;
            while (innerException != null)
            {
                message.Append("--->");
                message.Append(innerException.Message);
                innerException = innerException.InnerException;
            }
            return message.ToString();
        }
    }
}
