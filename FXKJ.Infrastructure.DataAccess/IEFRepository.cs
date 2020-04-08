using EntitiesModels.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FXKJ.Infrastructure.DataAccess
{
    public interface IEFRepository<T> where T : class 
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        T Add(T entity);

        bool Add(T entity,bool retType);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<T> AddList(IEnumerable<T> list);

       

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        T Update(T current);

        bool Update(T current,bool retType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<T> UpdateList(IEnumerable<T> list);
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        int Delete(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> whereLambda);


        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T GetEntity(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        T GetEntity(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> whereLambda);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        List<T> GetPageList(QueryModel queryParam);


    }
}
