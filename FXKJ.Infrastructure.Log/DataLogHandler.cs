﻿using FXKJ.Infrastructure.Auth;
using System;
using System.Data.SqlClient;
using EntitiesModels.Models.SysModels;
using System.Collections.Generic;
using System.Data;
using FXKJ.Infrastructure.Auth.Auth;
using FXKJ.Infrastructure.Core.Helper;
using FXKJ.Infrastructure.Core.Util;
using EntitiesModels;

namespace FXKJ.Infrastructure.Log
{/// <summary>
 /// 数据操作日志记录
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
            AuthInfoViewModel authInfo = FormAuthenticationExtension.CurrentAuth();
            if (authInfo == null)
            {
                authInfo = new AuthInfoViewModel();
                authInfo.Name = "测试用户";
                authInfo.PhoneNumber = "15255458934";
                authInfo.GuidId = new Guid("00000000-0000-0000-0000-000000000000");
            }
            log = new DataLog()
            {
                OperateType = operateType,
                OperateTable = operateTable,
                OperationBefore = operationBefore,
                OperationAfterData = operateAfterData,
                CreateTime = DateTime.Now,
                CreateUserId = authInfo.GuidId,
                CreateUserCode = authInfo.PhoneNumber,
                CreateUserName = authInfo.Name,
                DataLogId = Guid.NewGuid()
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
            string sql = string.Format(@"insert into [dbo].[Log_DataLog] 
                         (
                          DataLogId,
                          OperateTable,
                          OperateType,
                          OperationBefore,
                          OperationAfterData, 
                          CreateTime,
                          CreateUserId,
                          CreateUserCode,
                          CreateUserName
                          )
                         values (
                         @DataLogId,
                         @OperateTable,
                         @OperateType,
                         @OperationBefore,
                         @OperationAfterData, 
                         @CreateTime,
                         @CreateUserId,
                         @CreateUserCode,
                         @CreateUserName )");
            List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter{
                      ParameterName = "DataLogId",
                      Value = log.DataLogId,
                     },
                      new SqlParameter{
                      ParameterName = "OperateTable",
                      Value = log.OperateTable,
                     },
                        new SqlParameter{
                      ParameterName = "OperateType",
                      Value = log.OperateType,
                     },
                          new SqlParameter{
                      ParameterName = "OperationBefore",
                      Value = log.OperationBefore,
                     },
                            new SqlParameter{
                      ParameterName = "OperationAfterData",
                      Value = log.OperationAfterData,
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
            result = SqlUtil.ExecuteNonQuery(GlobalParamsHelper.ReadConnectionString(), CommandType.Text, sql, list.ToArray());
            return result;
        }
    }
}
