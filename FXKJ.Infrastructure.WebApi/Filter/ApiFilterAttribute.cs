using FXKJ.Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FXKJ.Infrastructure.WebApi.Filter
{
    public class ApiFilterAttribute : ActionFilterAttribute
    {
        private OperationLogHandler _operationLogHandler;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            //获取Action特性
            var descriptionAttribute = actionContext.ActionDescriptor;
            if (descriptionAttribute != null)
            {
                _operationLogHandler = new OperationLogHandler(actionContext.Request);
                _operationLogHandler.log.ControllerName = descriptionAttribute.ControllerDescriptor.ControllerType.FullName;
                _operationLogHandler.log.ActionName = descriptionAttribute.ActionName;
                _operationLogHandler.log.Describe = "";
            }



           

            //数据验证 存储
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
           
            _operationLogHandler.ResultExecuted();
            _operationLogHandler.WriteLog();


            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
