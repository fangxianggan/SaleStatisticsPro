using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Config;
using System;
using System.Data.SqlClient;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Core.Sql;
using System.Data;
using System.Collections.Generic;
using FXKJ.Infrastructure.Auth.Auth;

namespace FXKJ.Infrastructure.Log
{
    public class SqlLogHandler : BaseHandler<SqlLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlLogHandler(string operateSql,
            DateTime endDateTime,
            double elapsedTime,
            string parameter
            )
            : base("SqlLogToDatabase")
        {
            AuthInfoViewModel authInfo = FormAuthenticationExtension.CurrentAuth();
            if (authInfo == null)
            {
                authInfo = new AuthInfoViewModel();
                authInfo.Name = "测试用户";
                authInfo.PhoneNumber = "15255458934";
                authInfo.GuidId = new Guid("00000000-0000-0000-0000-00000000");
            }
            log = new SqlLog
            {
                SqlLogId = Guid.NewGuid(),
                OperateSql = operateSql,
                ElapsedTime = elapsedTime,
                EndDateTime = endDateTime,
                Parameter = parameter,
                CreateTime = DateTime.Now,
                CreateUserId = authInfo.GuidId,
                CreateUserCode = authInfo.PhoneNumber,
                CreateUserName = authInfo.Name
            };
        }

        public override void WriteLog()
        {
            base.WriteLog();
            WriteSqlLogData(log);
        }

        private int WriteSqlLogData(SqlLog log)
        {
            int result = 0;  
            try
            {
                string sql = string.Format(@"insert into [dbo].[Log_SqlLog] 
                         (
                          SqlLogId,
                          OperateSql,
                          ElapsedTime,
                          EndDateTime,
                          Parameter,  
                          CreateTime,
                          CreateUserId,
                          CreateUserCode,
                          CreateUserName
                         )
                         values (
                          @SqlLogId,
                          @OperateSql,
                          @ElapsedTime,
                          @EndDateTime,
                          @Parameter,  
                          @CreateTime,
                          @CreateUserId,
                          @CreateUserCode,
                          @CreateUserName)");
                List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter{
                      ParameterName = "SqlLogId",
                      Value = log.SqlLogId,
                     },
                      new SqlParameter{
                      ParameterName = "OperateSql",
                      Value = log.OperateSql,
                     },
                        new SqlParameter{
                      ParameterName = "ElapsedTime",
                      Value = log.ElapsedTime,
                     },
                          new SqlParameter{
                      ParameterName = "EndDateTime",
                      Value = log.EndDateTime,
                     },
                        new SqlParameter{
                      ParameterName = "Parameter",
                      Value = log.Parameter,
                     },
                      new SqlParameter{
                      ParameterName = "CreateTime",
                      Value = log.CreateTime,
                      DbType=DbType.DateTime
                     },
                       new SqlParameter{
                      ParameterName = "CreateUserId",
                      Value = log.CreateUserId,
                     },
                       new SqlParameter{
                      ParameterName = "CreateUserCode",
                      Value = log.CreateUserCode,
                     },  new SqlParameter{
                      ParameterName = "CreateUserName",
                      Value = log.CreateUserName,
                     }
                };
                result = SqlHelper.ExecuteNonQuery(GlobalParams.ReadConnectionString(), CommandType.Text, sql, list.ToArray());
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
