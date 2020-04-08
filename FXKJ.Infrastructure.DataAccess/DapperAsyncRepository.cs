using EntitiesModels.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.DataAccess
{
    /// <summary>
    ///     DapperRepository仓储,T代表实体信息,规范约束为T必须继承IEntityBase接口
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class DapperAsyncRepository<T> : BaseRepository, IAsyncEFRepository<T> where T : class, new()
    {
        public Task<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(T entity, bool retType)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> AddList(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntity(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetList(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetPageList(QueryModel queryParam)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T current)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T current, bool retType)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> UpdateList(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }
    }
}
