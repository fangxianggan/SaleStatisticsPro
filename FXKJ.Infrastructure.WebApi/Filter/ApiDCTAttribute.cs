using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FXKJ.Infrastructure.WebApi.Filter
{
    /// <summary>
    /// 事务处理
    /// </summary>
    public class ApiDTCAttribute : ActionFilterAttribute
    {
        private static readonly string TransactionID = "TransactionToken";

        private static TransactionScope transaction;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            //事务
            if (actionContext.Request.Headers.Contains(TransactionID))
            {
                //客户端跳过来的 携带header 头里 事务id 
                var values = actionContext.Request.Headers.GetValues(TransactionID);
                if (values != null && values.Any())
                {
                    var token = Convert.FromBase64String(values.First());
                    var trans = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);
                    var transactionScope = new TransactionScope(trans);
                    actionContext.Request.Properties.Add(TransactionID, transactionScope);
                }
            }
            else
            {
                //单个系统里
                transaction = new TransactionScope();
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            //事务提交
            if (actionExecutedContext.Request.Properties.ContainsKey(TransactionID))
            {
                var tScope = (TransactionScope)actionExecutedContext.Request.Properties[TransactionID];

                if (tScope != null)
                {
                    if (actionExecutedContext.Exception != null)
                    {
                        Transaction.Current.Rollback(actionExecutedContext.Exception);
                    }
                    else
                    {
                        tScope.Complete();
                    }
                    tScope.Dispose();
                    actionExecutedContext.Request.Properties.Remove(TransactionID);
                }
            }
            else {
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
}
