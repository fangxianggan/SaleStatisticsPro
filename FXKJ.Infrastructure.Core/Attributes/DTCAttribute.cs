//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;
//using System.Web;
//using System.Web.Mvc;

//namespace FXKJ.Infrastructure.Core.Attributes
//{
//    public class DTCAttribute : ActionFilterAttribute
//    {
//        private static readonly string TransactionID = "TransactionToken";
//        private TransactionScope Scope = null;
//        private Transaction Trans = null;
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {



//            this.Scope = new TransactionScope();


//        }
//        public override void OnActionExecuted(ActionExecutedContext filterContext)
//        {

//            var tScope = this.Scope;

//            if (tScope != null)
//            {
//                if (filterContext.Exception != null)
//                {
//                    Transaction.Current.Rollback(filterContext.Exception);
//                }
//                else
//                {
//                    tScope.Complete();
//                }

//                tScope.Dispose();

//            }

//        }
//    }
//}
