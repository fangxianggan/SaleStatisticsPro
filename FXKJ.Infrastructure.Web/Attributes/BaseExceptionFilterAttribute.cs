//using FXKJ.Infrastructure.Log;
//using System;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace FXKJ.Infrastructure.Web.Attributes
//{
//    /// <summary>
//    ///  异常拦截器
//    /// 对操作使用该特性类或者是方法,不能在同一位置多次使用
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    // public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
//    public class BaseExceptionFilterAttribute : HandleErrorAttribute
//    {
//        /// <summary>
//        ///     异常发生记录日志
//        /// </summary>
//        /// <param name="filterContext"></param>
//        public override void OnException(ExceptionContext filterContext)
//        {
//            ExceptionLogHandler exceptionLogHandler = new ExceptionLogHandler(filterContext.Exception);
//            Task.Factory.StartNew(() => exceptionLogHandler.WriteLog());
//        }
//    }
//}
