using EntitiesModels.QueryModels;
using FXKJ.Infrastructure.Core.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Dapper
{
    /// <summary>
    /// Dapper操作工具
    /// </summary>
    public static class SqlMapperUtil
    {
        public static string ConnectionName { set; get; }
        public static DbBase CreateDbBase()
        {
            var dbBase = new DbBase(ConnectionName);
            return dbBase;
        }

        #region 映射 增删改查

        #region 增

        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="t">实体</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时</param>
        /// <returns></returns>
        public static bool Insert<T>(T t, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.Insert(t, transaction, commandTimeout);
                return result;
            }
        }

        /// <summary>
        /// 增加实体  返回当前实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static T InsertReT<T>(T t, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertReT(t, transaction, commandTimeout);
                return result;
            }
        }

        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="t"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int InsertScalar<T>(T t, IDbTransaction transaction = null,
            int? commandTimeout = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertScalar(t, transaction, commandTimeout);
                return result;
            }
        }

        /// <summary>
        /// 批量增加实体
        /// </summary>
        /// <param name="lt"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int InsertBatch<T>(IEnumerable<T> lt, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertBatch(lt, transaction, commandTimeout);
                return result;
            }
        }

        /// <summary>
        /// 批量增加实体
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        public static int InsertWithBulkCopy<T>(List<T> lt, IDbTransaction transaction = null) where T : new()
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertWithBulkCopy(lt, transaction);
                return result;
            }
        }

        /// <summary>
        /// 批量添加 返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lt"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static Task<List<T>> InsertWithBulkCopyReT<T>(List<T> lt, IDbTransaction transaction = null) where T : new()
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertWithBulkCopy(lt, transaction);
                return Task.Factory.StartNew(() => lt);
            }
        }

        #endregion

        #region 修改
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update<T>(T t, IDbTransaction transaction = null, SqlQuery sql = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.Update(t, transaction, sql);
                return result;
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static T UpdateReT<T>(T t, IDbTransaction transaction = null, SqlQuery sql = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.Update(t, transaction, sql);
                return t;
            }
        }

        #endregion

        #region 删
        /// <summary>
        /// 删除所有
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete<T>(SqlQuery sql = null, IDbTransaction transaction = null, object id = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.Delete<T>(sql, transaction);
                return result;
            }
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteById<T>(IDbTransaction transaction = null, SqlQuery sql = null, object id = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.DeleteById<T>(sql, transaction, id);
                return result;
            }
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static int DeleteByIds<T>(string ids, IDbTransaction transaction = null, SqlQuery sql = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.DeleteByIds<T>(ids, sql, transaction);
                return result;
            }
        }

        #endregion

        #region 查
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(SqlQuery sql = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.Query<T>(sql);
                return result;
            }
        }

        /// <summary>
        /// 获取满足条件的第一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T SingleOrDefault<T>(SqlQuery sql) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.SingleOrDefault<T>(sql);
                return result;
            }
        }

        /// <summary>
        /// 获取满足条件的第一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T SingleOrDefault<T>(SqlQuery sql = null, object id = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.SingleOrDefault<T>(sql, id);
                return result;
            }
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Count<T>(SqlQuery sql = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                var result = db.Count<T>(sql);
                return result;
            }
        }


        /// <summary>
        /// 单表存储过程分页
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="queryParam">分页参数</param>
        /// <returns>返回值</returns>
        public static IEnumerable<T> PagingQueryProc<T>(QueryModel queryParam, SqlQuery sql = null) where T : class
        {
            using (var dbs = CreateDbBase())
            {
                if (sql == null)
                    sql = SqlQuery<T>.Builder(dbs);
                //类型
                var parms = new DynamicParameters();
                parms.Add("TableName", sql._ModelDes.TableName);
                parms.Add("PrimaryKey", DapperCacheCommon.GetPrimary(sql._ModelDes).Field);
                parms.Add("Fields", "*");
                var filter = "";
                if (queryParam.Items.Count() > 0)
                {
                    filter = SearchFilterHelper.ConvertFilters(queryParam.Items);
                }
                parms.Add("Filters", " 1=1 " + filter);
                parms.Add("PageIndex", queryParam.PageIndex);
                parms.Add("PageSize", queryParam.PageSize);
                //排序字段 
                var orderString = "";
                if (queryParam.OrderList.Count() > 0)
                {
                    orderString = string.Format("{0}", SearchFilterHelper.ConvertOrderBy(queryParam.OrderList));
                }
                parms.Add("Sort", orderString);
                parms.Add("RecordCount", value: 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = dbs.StoredProcWithParams<T>("System_Proc_Paging", parms);
                queryParam.Total = parms.Get<int>("RecordCount");
                return data;

            }
        }

        /// <summary>
        /// 分页 单表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParam"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static IEnumerable<T> PagingQuery<T>(QueryModel queryParam, SqlQuery sql = null) where T : class
        {
            using (var db = CreateDbBase())
            {
                int count = 0;
                var result = db.Page<T>(queryParam.PageIndex, queryParam.PageSize, out count, sql);
                queryParam.Total = count;
                return result;
            }
        }



        #endregion




        #endregion


        #region sql  to  dapper

        #region 增删改  纯sql语句执行
        /// <summary>
        ///     执行增加删除修改语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数信息</param>
        /// <param name="isSetConnectionStr">是否需要重置连接字符串</param>
        /// <returns>影响数</returns>
        public static int InsertUpdateOrDeleteSql<T>(string sql, IDbTransaction transaction = null, dynamic parms = null, bool isSetConnectionStr = true)
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertUpdateOrDeleteSql(sql, transaction, (object)parms);
                return result;
            }
        }

        /// <summary>
        ///     执行增加删除修改语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数信息</param>
        /// <param name="isSetConnectionStr">是否需要重置连接字符串</param>
        /// <returns>影响数</returns>
        public static bool InsertUpdateOrDeleteSqlBool<T>(string sql, IDbTransaction transaction = null, dynamic parms = null, bool isSetConnectionStr = true)
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertUpdateOrDeleteSql(sql, transaction, (object)parms) > 0;
                return result;
            }
        }

        #endregion

        #region 查询
        /// <summary>
        /// 执行Sql语句带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IEnumerable<T> SqlWithParams<T>(string sql, dynamic parms = null)
        {
            using (var db = CreateDbBase())
            {
                var result = db.SqlWithParams<T>(sql, (object)parms);
                return result;
            }
        }


        /// <summary>
        /// 动态类型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> SqlWithParams(string sql, dynamic parms = null)
        {
            using (var db = CreateDbBase())
            {
                var result = db.SqlWithParams(sql, (object)parms);
                return result;
            }
        }

        /// <summary>
        /// 返回dataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static DataTable SqlWithParamsToDataTable(string sql, dynamic parms = null)
        {
            using (var db = CreateDbBase())
            {
                var result = db.SqlWithParamsToDataTable(sql, (object)parms);
                return result;
            }
        }



        /// <summary>
        /// 执行语句返回bool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static bool SqlWithParamsBool<T>(string sql, dynamic parms = null)
        {
            using (var db = CreateDbBase())
            {
                var result = db.SqlWithParamsBool(sql, (object)parms);
                return result;
            }
        }

        /// <summary>
        /// 执行语句返回第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T SqlWithParamsSingle<T>(string sql, dynamic parms = null)
        {
            using (var db = CreateDbBase())
            {
                var result = db.SqlWithParamsSingle<T>(sql, (object)parms);
                return result;
            }
        }

        /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Count<T>(string sql = null)
        {
            using (var db = CreateDbBase())
            {
                var result = db.DbConnecttion.Query<T>(sql).Count();
                return result;
            }
        }


        /// <summary>
        /// 执行sql 获取list 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="querySql"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetListData<T>(string querySql)
        {
            var result = SqlWithParams<T>(querySql, null);
            return result;
        }


        /// <summary>
        /// 执行sql 获取动态类型list
        /// </summary>
        /// <param name="querySql"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> GetDynamicData(string querySql)
        {
            var sql = string.Format(@"select * from ({0}) seq ", querySql);
            var result = SqlWithParams(sql, null);
            return result;
        }

        /// <summary>
        /// 执行 sql 获取分页list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="querySql"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public static IEnumerable<T> PagingQuery<T>(string querySql, QueryModel queryParam)
        {
            using (var db = CreateDbBase())
            {
                var sql = querySql;
                var page = " ";
                if (!queryParam.IsReport)
                {
                    var currentPage = queryParam.PageIndex; //当前页号
                    var pageSize = queryParam.PageSize; //每页记录数
                    var lower = ((currentPage - 1) * pageSize); //记录起点
                    var upper = currentPage * pageSize; //记录终点
                    page = "  OFFSET " + lower + " ROWS FETCH NEXT " + upper + " ROWS ONLY ";
                }
                var where = @" where 1=1 ";
                queryParam.Items = queryParam.Items.Where(p => p.Value.ToString() != "").ToList();
                if (queryParam.Items.Count() > 0)
                {
                    where += SearchFilterHelper.ConvertFilters(queryParam.Items);
                }

                //排序字段 
                var orderString = "";
                if (queryParam.OrderList.Count() > 0)
                {
                    orderString = string.Format("{0}", SearchFilterHelper.ConvertOrderBy(queryParam.OrderList));
                }
                sql = sql.Replace("@orderBy", orderString)
                    .Replace("@where", where)
                    .Replace("@page", page);
                string countSql = querySql.Replace("@where", where).Replace("@orderBy", " ").Replace("@page", "");
                countSql = string.Format(@"select count(*) as DataCount from ({0}) seq ", countSql);
                int total = 0;
                var data = db.SqlWithParamsPage<T>(sql, countSql, out total, null);
                queryParam.Total = total;
                return data;
            }
        }


        public static IEnumerable<T> PagingQueryMult<T>(string querySql, string countSql, QueryModel queryParam, Type[] types, Func<object[], T> map, string splitOn = "Id") where T : class
        {
            using (var db = CreateDbBase())
            {
                var sql = querySql;
                var page = " ";
                if (!queryParam.IsReport)
                {
                    var currentPage = queryParam.PageIndex; //当前页号
                    var pageSize = queryParam.PageSize; //每页记录数
                    var lower = ((currentPage - 1) * pageSize); //记录起点
                    var upper = currentPage * pageSize; //记录终点
                    page = "  OFFSET " + lower + " ROWS FETCH NEXT " + upper + " ROWS ONLY ";
                }
                var whereM = @" where 1=1 ";
                var whereF = @" where 1=1 ";
                queryParam.Items = queryParam.Items.Where(p => p.Value.ToString() != "").ToList();
                if (queryParam.Items.Count() > 0)
                {
                    var whereMList = queryParam.Items.Where(p => p.Prefix.Contains("M")).ToList();
                    whereM += SearchFilterHelper.ConvertFilters(whereMList);
                    var whereFList = queryParam.Items.Where(p => p.Prefix.Contains("F")).ToList();
                    whereF += SearchFilterHelper.ConvertFilters(whereFList);
                }
                //排序字段 
                var orderString = "";
                if (queryParam.OrderList.Count() > 0)
                {
                    orderString = string.Format("{0}", SearchFilterHelper.ConvertOrderBy(queryParam.OrderList));
                }
                sql = sql.Replace("@orderBy", orderString)
                    .Replace("@whereM", whereM)
                    .Replace("@whereF", whereF)
                    .Replace("@page", page);
                if (!string.IsNullOrEmpty(countSql))
                    countSql = string.Format(@"select count(*) as DataCount from ({0}) seq ", countSql.Replace("@whereM", whereM).Replace("@orderBy", " ").Replace("@page", ""));
                int total = 0;
                var data = db.SqlWithParamsPageMult<T>(sql, countSql, out total, types, map, null, splitOn);
                queryParam.Total = total;
                return data;
            }
        }

        /// <summary>
        /// 获取 datatable数据
        /// </summary>
        /// <param name="querySql"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string querySql, QueryModel queryParam)
        {
            var sql = querySql;
            var where = @" where 1=1 ";
            queryParam.Items = queryParam.Items.Where(p => p.Value.ToString() != "").ToList();
            if (queryParam.Items.Count() > 0)
            {
                where += SearchFilterHelper.ConvertFilters(queryParam.Items);
            }
            sql = sql.Replace("@where", where);
            var data = SqlWithParamsToDataTable(sql);
            return data;
        }


        #endregion

        #region 存储过程

        /// <summary>
        ///     存储过程查询所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName">The procname.</param>
        /// <param name="parms">The parms.</param>
        /// <param name="isSetConnectionStr"></param>
        /// <returns></returns>
        public static IEnumerable<T> StoredProcWithParams<T>(string procName, dynamic parms,
            bool isSetConnectionStr = true)
        {
            using (var db = CreateDbBase())
            {
                var result = db.StoredProcWithParams<T>(procName, (object)parms);
                return result;
            }
        }

        /// <summary>
        /// 存储过程查询所有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <param name="isSetConnectionStr"></param>
        /// <returns></returns>
        public static IEnumerable<T> StoredProcWithParamsSync<T>(string procName, dynamic parms,
            bool isSetConnectionStr = true)
        {
            using (var db = CreateDbBase())
            {
                return db.StoredProcWithParams<T>(procName, (object)parms);
            }
        }

        /// <summary>
        /// 增删改查存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <param name="isSetConnectionStr"></param>
        /// <returns></returns>
        public static int InsertUpdateOrDeleteStoredProc<T>(string procName, IDbTransaction transaction = null, dynamic parms = null,
            bool isSetConnectionStr = true)
        {
            using (var db = CreateDbBase())
            {
                var result = db.InsertUpdateOrDeleteStoredProc(procName, transaction, (object)parms);
                return result;
            }
        }

        /// <summary>
        /// 返回存储过程第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="parms"></param>
        /// <param name="isSetConnectionStr"></param>
        /// <returns></returns>
        public static T StoredProcWithParamsSingle<T>(string procName, dynamic parms = null,
            bool isSetConnectionStr = true)
        {
            using (var db = CreateDbBase())
            {
                var result = db.StoredProcWithParamsSingle<T>(procName, (object)parms);
                return result;
            }
        }
        #endregion



        #endregion

    }
}
