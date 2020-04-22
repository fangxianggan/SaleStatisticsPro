using FXKJ.Infrastructure.Core.Helper;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace FXKJ.Infrastructure.Core.Transactions
{

    public class TransactionScope : IDisposable
    {
        private Transaction transaction = Transaction.Current;
        public bool Completed { get; private set; }

        public TransactionScope(string connectionStringName, IsolationLevel isolationLevel = IsolationLevel.Unspecified,
            Func<string, DbProviderFactory> getFactory = null)
        {
            if (null == transaction)
            {
                if (null == getFactory)
                {
                    getFactory = cnnstringName => DbHelper.GetFactory(cnnstringName);
                }
                DbProviderFactory factory = getFactory(connectionStringName);
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                connection.Open();
                DbTransaction dbTransaction = connection.BeginTransaction(isolationLevel);
                Transaction.Current = new CommittableTransaction(dbTransaction);
            }
            else
            {
                Transaction.Current = transaction.DependentClone();
            }
        }

        public void Complete()
        {
            this.Completed = true;
        }
        public void Dispose()
        {
            Transaction current = Transaction.Current;
            Transaction.Current = transaction;
            if (!this.Completed)
            {
                current.Rollback();
            }
            CommittableTransaction committableTransaction = current as CommittableTransaction;
            if (null != committableTransaction)
            {
                if (this.Completed)
                {
                    committableTransaction.Commit();
                }
                committableTransaction.Dispose();
            }
        }
    }
}
