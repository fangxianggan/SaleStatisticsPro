using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Logic
{
    /// <summary>
    /// 业务逻辑基类:异步
    /// </summary>
    /// <typeparam name="T">实体信息</typeparam>
    public interface IAsyncLogic<T> where T : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        Task<HttpReponseModel<T>> AddLogic(T entity);

        /// <summary>
        /// SqlBulkCopy批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<HttpReponseModel<List<T>>> AddListLogic(IEnumerable<T> list);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        Task<HttpReponseModel<T>> UpdateLogic(T entity);


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        Task<HttpReponseModel<List<T>>> UpdateListLogic(IEnumerable<T> list);


        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<HttpReponseModel<int>> DeleteLogic(params object[] keyValues);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<HttpReponseModel<int>> DeleteLogic(Expression<Func<T, bool>> whereLambda);
        
      
        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<HttpReponseModel<T>> GetEntityLogic(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<HttpReponseModel<T>> GetEntityLogic(Expression<Func<T, bool>> whereLambda);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<HttpReponseModel<List<T>>> GetListLogic(Expression<Func<T, bool>> whereLambda);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        Task<HttpReponseModel<List<T>>> GetPageListLogic(QueryModel queryParam);
        

        
    }
}
