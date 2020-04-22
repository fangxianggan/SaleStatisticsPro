using FXKJ.Infrastructure.Core.Attributes;
using FXKJ.Infrastructure.Log;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FXKJ.Infrastructure.WebApi.Filter
{
    public class ApiFilterAttribute : ActionFilterAttribute
    {
       
        private static readonly string key = "enterTime";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            //记录进入请求的时间
            actionContext.Request.Properties[key] = DateTime.Now.ToBinary();
           
            //数据验证 存储
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //浏览记录
            object beginTime = null;
            if (actionExecutedContext.Request.Properties.TryGetValue(key, out beginTime))
            {
                DateTime time = DateTime.FromBinary(Convert.ToInt64(beginTime));
                var actionRecord= actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<ActionRecordAttribute>().FirstOrDefault();
                OperationLogHandler _operationLogHandler = new OperationLogHandler();
                _operationLogHandler.log.ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                _operationLogHandler.log.ControllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                _operationLogHandler.log.ActionExecutionTime = (DateTime.Now - time).TotalSeconds;
                _operationLogHandler.log.ResponseStatus = HttpContext.Current.Response.Status;
                _operationLogHandler.log.Describe = actionRecord == null ? "" : actionRecord.Describe;
                _operationLogHandler.WriteLog();
            }

           
            
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
