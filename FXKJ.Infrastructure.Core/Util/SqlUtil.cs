using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Core.Util
{
    public static class SqlUtil
    {

        ///   
        /// 获取局域网内的所有数据库服务器名称   
        ///   
        /// 服务器名称数组 
        public static List<string> GetSqlServerNames()
        {
            DataTable dataSources = SqlClientFactory.Instance.CreateDataSourceEnumerator().GetDataSources();

            DataColumn column = dataSources.Columns["InstanceName"];
            DataColumn column2 = dataSources.Columns["ServerName"];

            DataRowCollection rows = dataSources.Rows;
            List<string> Serverlist = new List<string>();
            string array = string.Empty;
            for (int i = 0; i < rows.Count; i++)
            {
                string str2 = rows[i][column2] as string;
                string str = rows[i][column] as string;
                if (((str == null) || (str.Length == 0)) || ("MSSQLSERVER" == str))
                {
                    array = str2;
                }
                else
                {
                    array = str2 + @"/" + str;
                }

                Serverlist.Add(array);
            }

            Serverlist.Sort();

            return Serverlist;
        }

        ///   
        /// 查询sql中的非系统库的名字   
        ///   
        ///   
        ///   
        public static List<string> DatabaseListNames(string connection)
        {
            List<string> getCataList = new List<string>();
            string cmdStirng = "select name from sys.databases where database_id > 4";
            SqlConnection connect = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(cmdStirng, connect);

            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
                IDataReader dr = cmd.ExecuteReader();
                getCataList.Clear();
                while (dr.Read())
                {
                    getCataList.Add(dr["name"].ToString());
                }
                dr.Close();
            }

            if (connect != null && connect.State == ConnectionState.Open)
            {
                connect.Dispose();
            }

            return getCataList;
        }

        ///   
        /// 获取数据库里所有的表名   
        ///   
        ///   
        ///   
        public static List<string> GetDataBaseTablesNames(string connection)
        {
            List<string> tablelist = new List<string>();
            SqlConnection objConnetion = new SqlConnection(connection);

            if (objConnetion.State == ConnectionState.Closed)
            {
                objConnetion.Open();
                DataTable objTable = objConnetion.GetSchema("Tables");
                foreach (DataRow row in objTable.Rows)
                {
                    tablelist.Add(row[2].ToString());
                }
            }



            if (objConnetion != null && objConnetion.State == ConnectionState.Closed)
            {
                objConnetion.Dispose();
            }


            return tablelist;
        }

        ///   
        /// 获取某个表里的所有字段   
        ///    
        public static List<string> GetTablesColumnFields(string connection, string TableName)
        {
            List<string> Columnlist = new List<string>();
            SqlConnection objConnetion = new SqlConnection(connection);

            if (objConnetion.State == ConnectionState.Closed)
            {
                objConnetion.Open();
            }

            SqlCommand cmd = new SqlCommand("Select Name FROM SysColumns Where id=Object_Id('" + TableName + "')", objConnetion);
            SqlDataReader objReader = cmd.ExecuteReader();

            while (objReader.Read())
            {
                Columnlist.Add(objReader[0].ToString());

            }

            objConnetion.Close();
            return Columnlist;
        }

        /// <summary>
        /// 数据备份
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="dbName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static int DataBaseBackup(string connection, string dbName, string filePath)
        {
           
            return ExecuteSqlCommand(connection, string.Format("backup database {0} to disk ='{1}'", dbName, filePath));
        }

        /// <summary>
        /// 数据还原
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="dbName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static int DataBaseRestore(string connection, string dbName, string filePath) 
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append("use master alter database " + "[" + dbName + "]" + " set offline with rollback immediate ");
            sb2.Append(" use master alter database " + "[" + dbName + "]" + " set online with rollback immediate ");
            string sqlRestore = sb.ToString() + "USE MASTER RESTORE DATABASE " + "[" + dbName + "]" + " FROM DISK='" + filePath + "' WITH REPLACE " + sb2.ToString();

            return ExecuteSqlCommand(connection, sqlRestore);
        }

        public static int ExecuteSqlCommand(string connection, string cmdText)
        {
            using (DbConnection conn = new SqlConnection(connection))
            {
                DbCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                return cmd.ExecuteNonQuery();
            }
        }
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }



    }
}
