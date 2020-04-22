using FXKJ.Infrastructure.Core.Transactions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FXKJ.Infrastructure.WebApi.Filter
{
    public class ApiCustmTransactionScopeAttribute : ActionFilterAttribute
    {
        private static TransactionScope transaction;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            //事务
            transaction = new TransactionScope("MyStrConn");

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            //事务提交
            if (transaction != null)
            {
                if (actionExecutedContext.Exception == null)
                {
                    transaction.Complete();
                }
                transaction.Dispose();
            }
        }
    }
}
