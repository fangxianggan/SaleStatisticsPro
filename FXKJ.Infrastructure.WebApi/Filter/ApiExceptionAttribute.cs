using FXKJ.Infrastructure.Log;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace FXKJ.Infrastructure.WebApi.Filter
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常发生记录日志
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            //记录错误日志
            ExceptionLogHandler exceptionLogHandler = new ExceptionLogHandler(actionExecutedContext.Exception);
            Task.Factory.StartNew(() => exceptionLogHandler.WriteLog());
            base.OnException(actionExecutedContext);
        }
    }
}
