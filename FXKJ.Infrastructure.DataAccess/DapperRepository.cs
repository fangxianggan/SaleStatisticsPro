using EntitiesModels.QueryModels;
using FXKJ.Infrastructure.Core.Helper;
using FXKJ.Infrastructure.Core.ModelToSql;
using FXKJ.Infrastructure.Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FXKJ.Infrastructure.DataAccess
{
    /// <summary>
    ///     DapperRepository仓储,T代表实体信息,规范约束为T必须继承IEntityBase接口
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class DapperRepository<T> : BaseRepository,IDapperRepository<T> where T : class, new()
    {

        public DapperRepository()
        {
            SqlMapperUtil.ConnectionName = "MyStrConn";
        }
        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Add(T entity, bool retType)
        {
            throw new NotImplementedException();
        }

        public List<T> AddList(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public int Delete(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public virtual T GetEntity(params object[] keyValues)
        {
            return SqlMapperUtil.SingleOrDefault<T>(null,keyValues as object);
        }

        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            using (var db = new DbBase(SqlMapperUtil.ConnectionName))
            {
                SqlQuery sqlQuery = SqlQuery<T>.Builder(db);
                sqlQuery._Sql =new StringBuilder(LambdaToSqlHelper<T>.GetWhereByLambda(whereLambda, "sqlserver"));
                return db.SingleOrDefault<T>(sqlQuery);
            }
                  
        }

        public List<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public List<T> GetPageList(QueryModel queryParam)
        {
            throw new NotImplementedException();
        }

        public T Update(T current)
        {
            throw new NotImplementedException();
        }

        public bool Update(T current, bool retType)
        {
            throw new NotImplementedException();
        }

        public List<T> UpdateList(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }
    }
}
