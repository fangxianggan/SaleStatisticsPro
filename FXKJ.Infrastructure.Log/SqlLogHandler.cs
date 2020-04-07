using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Config;
using System;
using System.Data.SqlClient;
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Core.Sql;
using System.Data;
using System.Collections.Generic;

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
                authInfo.Name = "测试用户";
                authInfo.PhoneNumber = "15255458934";
                authInfo.GuidId = new Guid("00000000-0000-0000-0000-00000000");
            }
            log = new SqlLog
            {
                SqlLogId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CreateUserId = authInfo.GuidId,
                CreateUserCode = authInfo.PhoneNumber,
                CreateUserName = authInfo.Name,
                OperateSql = operateSql,
                ElapsedTime = elapsedTime,
                EndDateTime = endDateTime,
                Parameter = parameter
            };
        }

        public override void WriteLog()
        {
            base.WriteLog();
            WriteSqlLogData(log);
        }

        private int WriteSqlLogData(SqlLog log)
        {
            int result = 0;  //接收sql返回的结果
            //写入sql日志
            try
            {
                string sql = string.Format(@"insert into [dbo].[Log_SqlLog] values (
                         @SqlLogId,
                         @CreateTime,
                         @CreateUserId,
                         @CreateUserCode,
                         @CreateUserName,
                         @OperateSql,
                         @ElapsedTime,
                         @EndDateTime,
                         @Parameter
                         )");
                List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter{
                      ParameterName = "SqlLogId",
                      Value = log.SqlLogId,
                     },
                      new SqlParameter{
                      ParameterName = "CreateTime",
                      Value = log.CreateTime,
                     },
                        new SqlParameter{
                      ParameterName = "CreateUserId",
                      Value = log.CreateUserId,
                     },
                          new SqlParameter{
                      ParameterName = "CreateUserCode",
                      Value = log.CreateUserCode,
                     },
                           new SqlParameter{
                      ParameterName = "CreateUserName",
                      Value = log.CreateUserName,
                     },
                            new SqlParameter{
                      ParameterName = "OperateSql",
                      Value = log.OperateSql,
                     },
                              new SqlParameter{
                      ParameterName = "Parameter",
                      Value = log.Parameter,
                     },
                               new SqlParameter{
                      ParameterName = "EndDateTime",
                      Value = log.SqlLogId,
                     },
                                new SqlParameter{
                      ParameterName = "ElapsedTime",
                      Value = log.SqlLogId,
                     },
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
