﻿
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Logic
{
    /// <summary>
    /// 异步Logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  class Logic<T> : ILogic<T> where T: class, new()
    {
        private readonly IEFRepository<T> _repository;

        public Logic(IEFRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "repository为空");
            }
            _repository = repository;
        }
        
        public HttpReponseModel<T> AddLogic(T entity)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data=  _repository.Add(entity);
            return httpReponse;
        }

        public HttpReponseModel<List<T>> AddListLogic(IEnumerable<T> list)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data =  _repository.AddList(list);
            return httpReponse;
        }

        public HttpReponseModel<T> UpdateLogic(T entity)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data =  _repository.Update(entity);
            return httpReponse;
        }

        public HttpReponseModel<List<T>> UpdateListLogic(IEnumerable<T> list)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data =  _repository.UpdateList(list);
            return httpReponse;
        }
        public HttpReponseModel<int> DeleteLogic(params object[] keyValues)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            httpReponse.Data =  _repository.Delete(keyValues);
            return httpReponse;
        }

        public HttpReponseModel<int> DeleteLogic(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            httpReponse.Data =  _repository.Delete(whereLambda);
            return httpReponse;
        }

        public HttpReponseModel<T> GetEntityLogic(params object[] keyValues)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data =  _repository.GetEntity(keyValues);
            return httpReponse;
        }

        public HttpReponseModel<T> GetEntityLogic(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data =  _repository.GetEntity(whereLambda);
            return httpReponse;
        }

        public HttpReponseModel<List<T>> GetListLogic(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data =  _repository.GetList(whereLambda);
            
            return httpReponse;
        }

        public HttpReponseModel<List<T>> GetPageListLogic(QueryModel queryParam)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data =  _repository.GetPageList(queryParam);
            httpReponse.Total = queryParam.Total;
            httpReponse.PageIndex = queryParam.PageIndex;
            httpReponse.PageSize = queryParam.PageSize;
            httpReponse.RequestParams = queryParam;
            return httpReponse;
        }

      

       

      
    }
}
