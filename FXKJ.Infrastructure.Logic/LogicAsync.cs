
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
    public abstract class AsyncLogic<T> : IAsyncLogic<T> where T: class, new()
    {
        private readonly IAsyncRepository<T> _repository;

        public AsyncLogic()
        {
           
        }
        public AsyncLogic(IAsyncRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository", "repository为空");
            }
            _repository = repository;
        }
        
        public async Task<HttpReponseModel<T>> AddLogic(T entity)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data= await _repository.Add(entity);
            return httpReponse;
        }

        public async Task<HttpReponseModel<List<T>>> AddListLogic(IEnumerable<T> list)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data = await _repository.AddList(list);
            return httpReponse;
        }

        public async Task<HttpReponseModel<T>> UpdateLogic(T entity)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data = await _repository.Update(entity);
            return httpReponse;
        }

        public async Task<HttpReponseModel<List<T>>> UpdateListLogic(IEnumerable<T> list)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data = await _repository.UpdateList(list);
            return httpReponse;
        }
        public async Task<HttpReponseModel<int>> DeleteLogic(params object[] keyValues)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            httpReponse.Data = await _repository.Delete(keyValues);
            return httpReponse;
        }

        public async Task<HttpReponseModel<int>> DeleteLogic(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            httpReponse.Data = await _repository.Delete(whereLambda);
            return httpReponse;
        }

        public async Task<HttpReponseModel<T>> GetEntityLogic(params object[] keyValues)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data = await _repository.GetEntity(keyValues);
            return httpReponse;
        }

        public async Task<HttpReponseModel<T>> GetEntityLogic(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseModel<T> httpReponse = new HttpReponseModel<T>();
            httpReponse.Data = await _repository.GetEntity(whereLambda);
            return httpReponse;
        }

        public async Task<HttpReponseModel<List<T>>> GetListLogic(Expression<Func<T, bool>> whereLambda)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data = await _repository.GetList(whereLambda);
            return httpReponse;
        }

        public async Task<HttpReponseModel<List<T>>> GetPageListLogic(QueryModel queryParam)
        {
            HttpReponseModel<List<T>> httpReponse = new HttpReponseModel<List<T>>();
            httpReponse.Data = await _repository.GetPageList(queryParam);
            return httpReponse;
        }

      

       

      
    }
}
