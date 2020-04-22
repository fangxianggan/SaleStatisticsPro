using System.Configuration;
using System.Data.Common;

namespace FXKJ.Infrastructure.Core.Helper
{
    public class DbHelper
    {
        public DbProviderFactory DbProviderFactory { get; private set; }
        public string ConnectionString { get; private set; }
        public virtual DbParameter BuildDbParameter(string name, object value)
        {
            DbParameter parameter = this.DbProviderFactory.CreateParameter();
            parameter.ParameterName = "@" + name;
            parameter.Value = value;
            return parameter;
        }

        public DbHelper(string cnnStringName)
        {
            var cnnStringSection = ConfigurationManager.ConnectionStrings[cnnStringName];
            this.DbProviderFactory = DbProviderFactories.GetFactory(cnnStringSection.ProviderName);
            this.ConnectionString = cnnStringSection.ConnectionString;
        }
        public static DbProviderFactory GetFactory(string cnnStringName)
        {
            var cnnStringSection = ConfigurationManager.ConnectionStrings[cnnStringName];
            return DbProviderFactories.GetFactory(cnnStringSection.ProviderName);
        }

        //public int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters)
        //{
        //    DbConnection connection = null;
        //    DbCommand command = this.DbProviderFactory.CreateCommand();
        //    DbTransaction dbTransaction = null;
        //    try
        //    {
        //        command.CommandText = commandText;
        //        parameters = parameters ?? new Dictionary<string, object>();
        //        foreach (var item in parameters)
        //        {
        //            command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
        //        }
        //        if (null != Transactions.Transaction.Current)
        //        {
        //            command.Connection = Transactions.Transaction.Current.DbTransactionWrapper.DbTransaction.Connection;
        //            command.Transaction = Transactions.Transaction.Current.DbTransactionWrapper.DbTransaction;
        //        }
        //        else
        //        {
        //            connection = this.DbProviderFactory.CreateConnection();
        //            connection.ConnectionString = this.ConnectionString;
        //            command.Connection = connection;
        //            connection.Open();
        //            if (System.Transactions.Transaction.Current == null)
        //            {
        //                dbTransaction = connection.BeginTransaction();
        //                command.Transaction = dbTransaction;
        //            }
        //        }
        //        int result = command.ExecuteNonQuery();
        //        if (null != dbTransaction)
        //        {
        //            dbTransaction.Commit();
        //        }
        //        return result;
        //    }
        //    catch
        //    {
        //        if (null != dbTransaction)
        //        {
        //            dbTransaction.Rollback();
        //        }
        //        throw;
        //    }
        //    finally
        //    {
        //        if (null != connection)
        //        {
        //            connection.Dispose();
        //        }
        //        if (null != dbTransaction)
        //        {
        //            dbTransaction.Dispose();
        //        }
        //        command.Dispose();
        //    }
        //}
    }
}
