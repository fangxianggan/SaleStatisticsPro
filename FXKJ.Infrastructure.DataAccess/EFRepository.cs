using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using FXKJ.Infrastructure.Core.Extensions;
using FXKJ.Infrastructure.Entities.QueryModel;


namespace FXKJ.Infrastructure.DataAccess
{
    public class EFRepository<T> : BaseRepository, IAsyncRepository<T> 
        where T : class,new()
        
    {
        private readonly DbContext _db;
        public EFRepository(DbContext db)
        {
            _db = db;
        }


        public virtual async Task<T> Add(T model)
        {
            using (var dbContext = _db)
            {
                dbContext.Set<T>().Add(model);
                await dbContext.SaveChangesAsync();
                return model;
            }
        }

        public virtual async Task<List<T>> AddList(IEnumerable<T> list)
        {
            using (var dbContext = _db)
            {
                foreach (var _model in list)
                {
                    dbContext.Set<T>().Add(_model);
                }
                await dbContext.SaveChangesAsync();
                return list.ToList();
            }
        }
        public virtual async Task<T> Update(T model)
        {
            using (var dbContext = _db)
            {
                dbContext.Entry(model).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return model;
            }
        }

        public virtual async Task<List<T>> UpdateList(IEnumerable<T> list)
        {
            using (var dbContext = _db)
            {
                foreach (var model in list)
                {
                    dbContext.Entry(model).State = EntityState.Modified;
                }
                await dbContext.SaveChangesAsync();
                return list.ToList();
            }
        }

        public virtual async Task<int> Delete(params object[] keyValues)
        {
            using (var dbContext = _db)
            {
                var _model = dbContext.Set<T>().Find(keyValues);
                dbContext.Set<T>().Remove(_model);
                return await dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<int> Delete(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = _db)
            {
                var _modelList = dbContext.Set<T>().Where(whereLambda);
                foreach (var _model in _modelList)
                {
                    dbContext.Set<T>().Remove(_model);
                }
                return await dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<T> GetEntity(params object[] keyValues)
        {
            using (var dbContext = _db)
            {
                var _model = dbContext.Set<T>().FindAsync(keyValues);
                return await _model;
            }
        }

        public virtual async Task<T> GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = _db)
            {
                return await dbContext.Set<T>().Where(whereLambda).FirstOrDefaultAsync();
            }
        }

        public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = _db)
            {
                return await dbContext.Set<T>().Where(whereLambda).ToListAsync();
            }
        }

        public virtual async Task<List<T>> GetPageList(QueryModel queryParam)
        {
            using (var dbContext = _db)
            {
                IQueryable<T> QueryList = dbContext.Set<T>();
                //条件操作
                QueryList = Core.Extensions.QueryableExtensions.Where(QueryList, queryParam);
                //排序操作
                QueryList = _GetOrder(QueryList, queryParam.OrderList);
                queryParam.Total = QueryList.Count();
                return await QueryList.Skip((queryParam.PageIndex - 1) * queryParam.PageSize).Take(queryParam.PageSize).ToListAsync();
            }
        }

        private IQueryable<T> _GetOrder(IQueryable<T> list, List<QueryOrder> orders)
        {
            if (orders.Count() > 0)
            {
                foreach (var item in orders)
                {
                    if (item.IsDesc)
                    {
                        list = PredicateExtensions.OrderBy(list, item.Field, false);
                    }
                    else
                    {
                        list = PredicateExtensions.OrderBy(list, item.Field, true);
                    }
                }
            }
            return list;
        }
    }
}

