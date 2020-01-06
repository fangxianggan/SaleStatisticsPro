
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.Entities.Enum;
using FXKJ.Infrastructure.Entities.HttpResponse;
using System;
using System.Collections.Generic;

namespace FXKJ.Infrastructure.Logic
{
    public abstract class Logic<T> : ILogic<T> where T : class, new()
    {
        protected Logic()
        {
        }

        protected Logic(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "repository not null");
            }
            Repository = repository;
        }

        private IRepository<T> Repository { get; set; }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        public HttpReponseModel Insert(T entity)
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                var resultNum = Repository.Insert(entity);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public HttpReponseModel InsertMultipleDapper(IEnumerable<T> list)
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                var resultNum = Repository.InsertMultipleDapper(list);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public HttpReponseModel InsertMultiple(IEnumerable<T> list)
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                var resultNum = Repository.InsertMultiple(list);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="current">更新实体</param>
        /// <returns></returns>
        public HttpReponseModel Update(T current)
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                var resultNum = Repository.Update(current);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public HttpReponseModel Delete(T entity)
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                var resultNum = Repository.Delete(entity);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public HttpReponseModel Delete(object id)
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                //获取需要删除的数据
                var t = GetById(id);
                if (t == null)
                {
                    operateStatus.ResultSign = ResultSign.Error;
                    operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, HttpReponseMessage.HaveDelete);
                    return operateStatus;
                }
                var resultNum = Repository.Delete(id);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     删除匹配项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public HttpReponseModel DeleteBatch(string ids)
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                var resultNum = Repository.DeleteBatch(ids);
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     删除匹配项
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel DeleteAll()
        {
            var operateStatus = new HttpReponseModel();
            try
            {
                var resultNum = Repository.DeleteAll();
                operateStatus.ResultSign = resultNum > 0 ? ResultSign.Successful : ResultSign.Error;
                operateStatus.Message = resultNum > 0 ? HttpReponseMessage.SuccessMsg : HttpReponseMessage.ErrorMsg;
            }
            catch (Exception exception)
            {
                operateStatus.Message = string.Format(HttpReponseMessage.ErrorMsg, exception.Message);
            }
            return operateStatus;
        }

        /// <summary>
        ///     获取集合数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllEnumerable()
        {
            return Repository.GetAllEnumerable();
        }

        /// <summary>
        ///     根据主键获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return Repository.GetById(id);
        }
    }
}
