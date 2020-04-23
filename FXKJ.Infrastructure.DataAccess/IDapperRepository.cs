using EntitiesModels.QueryModels;
using FXKJ.Infrastructure.Dapper;
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
   public  interface IDapperRepository<T> where T : class
    {
         T Add(T entity);


        bool Add(T entity, bool retType);


        List<T> AddList(IEnumerable<T> list);


        int Delete(params object[] keyValues);


        int Delete(Expression<Func<T, bool>> whereLambda);


         T GetEntity(params object[] keyValues);


        T GetEntity(Expression<Func<T, bool>> whereLambda);


        List<T> GetList(Expression<Func<T, bool>> whereLambda);


        List<T> GetPageList(QueryModel queryParam);


        T Update(T current);


        bool Update(T current, bool retType);


        List<T> UpdateList(IEnumerable<T> list);
        
    }
}
