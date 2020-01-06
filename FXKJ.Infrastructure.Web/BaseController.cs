using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Log;
using FXKJ.Infrastructure.Web.Attributes;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace FXKJ.Infrastructure.Web
{

    [BaseAuthorizeFilter]
    [BaseActionFilter]
    [BaseExceptionFilter]
  
    public class BaseController : Controller
    {

        public BaseController()
        {
            ViewBag.BasicPath = "http://localhost:63403";
        }

        #region 属性

        /// <summary>
        ///     当前登录用户信息
        /// </summary>
        protected virtual PrincipalUser CurrentUser { get; set; }

        /// <summary>
        ///     用户操作日志
        /// </summary>
        private OperationLogHandler _operationLogHandler;

        #endregion

        #region 初始化

        /// <summary>
        ///     重写Controller,此处可写入登录日志
        /// </summary>
        /// <param name="requestContext">上下文对象</param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //从Cookie里面获取用户信息
            CurrentUser = FormAuthenticationExtension.Current(System.Web.HttpContext.Current.Request);
            if (CurrentUser != null)
            {
                _operationLogHandler = new OperationLogHandler(Request)
                {
                    log =
                    {
                        CreateUserCode = CurrentUser.Code,
                        CreateUserName = CurrentUser.Name
                    }
                };
            }
        }

        #endregion

        #region 错误

        /// <summary>
        ///     当请求与此控制器匹配但在此控制器中找不到任何具有指定操作名称的方法时调用
        /// </summary>
        /// <param name="actionName"></param>
        protected override void HandleUnknownAction(string actionName)
        {
            IController errorController = new ErrorController();
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "Error");
            errorRoute.Values.Add("action", "ActionUnknown");
            if (Request.Url != null) errorRoute.Values.Add("url", Request.Url.OriginalString);
            errorRoute.Values.Add("unkonwnAction", actionName);
            errorController.Execute(new RequestContext(HttpContext, errorRoute));
        }

        /// <summary>
        ///     出现错误
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            //跳转页面
            IController errorController = new ErrorController();
            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "Error");
            errorRoute.Values.Add("action", "Exception");
            errorRoute.Values.Add("msg", filterContext.Exception);
            errorController.Execute(new RequestContext(HttpContext, errorRoute));
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }

        #endregion

        #region 默认的加载页面
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Form()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Details()
        {
            return View();
        }
        #endregion

        #region Action

        /// <summary>
        ///     Action开始执行触发
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取Action特性
            var descriptionAttribute = filterContext.ActionDescriptor.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            if (descriptionAttribute.Any())
            {
                var info = descriptionAttribute[0] as DescriptionAttribute;
                if (info != null)
                {
                    var description = info.Description;
                    _operationLogHandler.log.ControllerName =
                        filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;
                    _operationLogHandler.log.ActionName = filterContext.ActionDescriptor.ActionName;
                    _operationLogHandler.log.Describe = description;
                }
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        ///     Action结束执行触发
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (!string.IsNullOrEmpty(_operationLogHandler.log.Describe))
            {
                _operationLogHandler.ActionExecuted();
                _operationLogHandler.WriteLog();
            }
            
        }

        /// <summary>
        ///     开始返回结果
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            //记录日志
            if (filterContext.Result.GetType() == typeof(RedirectResult))
            {
                if (!string.IsNullOrEmpty(_operationLogHandler.log.Describe))
                {
                    _operationLogHandler.ActionExecuted();
                }
            }
        }

        /// <summary>
        ///     结果返回结束
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            //记录日志
            if (!string.IsNullOrEmpty(_operationLogHandler.log.Describe))
            {
                _operationLogHandler.ResultExecuted(Response);
                _operationLogHandler.WriteLog();
            }
        }
      
        
        #endregion


    }
}
