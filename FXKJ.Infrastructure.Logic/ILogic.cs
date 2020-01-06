using System.Collections.Generic;
using FXKJ.Infrastructure.Entities.HttpResponse;

namespace FXKJ.Infrastructure.Logic
{
    /// <summary>
    /// 业务逻辑基类
    /// </summary>
    /// <typeparam name="T">实体信息</typeparam>
    public interface ILogic<T> where T : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        HttpReponseModel Insert(T entity);

        /// <summary>
        /// Dapper批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        HttpReponseModel InsertMultipleDapper(IEnumerable<T> list);

        /// <summary>
        /// SqlBulkCopy批量新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        HttpReponseModel InsertMultiple(IEnumerable<T> list);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        HttpReponseModel Update(T current);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        HttpReponseModel Delete(T entity);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        HttpReponseModel Delete(object id);

        /// <summary>
        /// 删除匹配项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        HttpReponseModel DeleteBatch(string ids);

        /// <summary>
        /// 获取集合数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAllEnumerable();

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);
    }
}
