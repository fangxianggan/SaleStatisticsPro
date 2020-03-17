using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Config;
using FXKJ.Infrastructure.Log.Util;
using FXKJ.Infrastructure.Log.LogModel;
using System;
using System.Data.SqlClient;
using System.Web;

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
            PrincipalUser principalUser = new PrincipalUser
            {
                Name = "匿名用户",
                UserId = Guid.Empty
            };
            var current = HttpContext.Current;
            if (current != null)
            {
                principalUser = FormAuthenticationExtension.Current(HttpContext.Current.Request);
            }
            if (principalUser == null)
            {
                principalUser = new PrincipalUser()
                {
                    Name = "匿名用户",
                    UserId = Guid.Empty
                };
            }
            log = new SqlLog
            {
                SqlLogId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CreateUserId = principalUser.UserId,
                CreateUserCode = principalUser.Code,
                CreateUserName = principalUser.Name,
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
                string conStr = GlobalParams.ReadConnectionString();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    //插入sql语句
                    string sqlStr = @"insert into [dbo].[SqlLog] (
[SqlLogId],
[OperateSql],
[EndDateTime],
[ElapsedTime],
[Parameter],
[CreateUserCode],
[CreateTime],
[UpdateTime]
) values('" +
log.SqlLogId + "','" +
log.OperateSql + "','" +
log.EndDateTime.ToString(DateTimeConfig.DateTimeFormatS) + "','" +
log.ElapsedTime + "','" +
log.Parameter + "','" +
log.CreateUserCode + "','" +
log.CreateTime.ToString(DateTimeConfig.DateTimeFormatS) +"','"+
log.CreateTime.ToString(DateTimeConfig.DateTimeFormatS) +
"');";
                    using (SqlCommand cmd = new SqlCommand(sqlStr, con))
                    {
                        con.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }
}
