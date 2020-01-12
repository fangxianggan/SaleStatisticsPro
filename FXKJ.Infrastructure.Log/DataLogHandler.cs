using FXKJ.Infrastructure.Config;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Log.LogModel;
using System;
using System.Data.SqlClient;
using System.Web;

namespace FXKJ.Infrastructure.Log
{/// <summary>
    /// 数据日志记录
    /// </summary>
    public class DataLogHandler : BaseHandler<DataLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataLogHandler(string operateType,
            string operateTable,
            string operationBefore,
            string operateAfterData)
            : base("DataLogToDatabase")
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
            log = new DataLog()
            {
                OperateType = operateType,
                OperateTable = operateTable,
                OperationBefore = operationBefore,
                OperationAfterData = operateAfterData,
                OperateTime = DateTime.Now,
                CreateUserId = principalUser.UserId,
                CreateUserCode = principalUser.Code,
                CreateUserName = principalUser.Name,
                DataLogId =Guid.NewGuid()
            };
        }


        public override void WriteLog()
        {
            base.WriteLog();

            //加入数据库
            WriteDataLogData(log);
        }

        private int WriteDataLogData(DataLog log)
        {
            //写入sql日志
            int result = 0;
            try
            {
                string conStr = GlobalParams.ReadConnectionString();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    //插入sql语句
                    string sqlStr = "insert into [dbo].[Sys_DataLog] ([DataLogId],[OperateTable],[OperateType],[OperationBefore],[OperationAfterData],[CreateUserId],[CreateUserCode],[CreateUserName],[OperateTime]) values('" + log.DataLogId + "','" + log.OperateTable + "','" + log.OperateType + "','" + log.OperationBefore + "','" + log.OperationAfterData + "','" + log.CreateUserId + "','" + log.CreateUserCode + "','" + log.CreateUserName + "','" + log.OperateTime.ToString(DateTimeConfig.DateTimeFormatS) + "');";
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
