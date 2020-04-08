using EntitiesModels.Enum;
using EntitiesModels.HttpResponse;
using FXKJ.Infrastructure.Auth.IBLL;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FXKJ.Infrastructure.WebApi.Filter
{
    /// <summary>
    /// 访问菜单权限
    /// </summary>
    public class ApiMenuAuthAttribute : ActionFilterAttribute
    {


        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            //控制器
            string controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //方法
            string action = actionContext.ActionDescriptor.ActionName;
            var routeData = actionContext.RequestContext.RouteData;
            string userName = "15255459034";
            string menuUrl = "/" + controller + "/" + action;
            //注入
            var _tokenBLL = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ITokenBLL)) as ITokenBLL;
            if (_tokenBLL != null)
            {
                
                    HttpReponseModel httpReponse = new HttpReponseModel();
                    httpReponse.Code = StatusCode.Unauthorized;
                    httpReponse.Message = "未授权";
                    httpReponse.Flag = true;
                    httpReponse.ResultSign = ResultSign.Warning;
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, httpReponse, "application/json");
                
               
            }
            else
            {

            }
        }
    }
}
