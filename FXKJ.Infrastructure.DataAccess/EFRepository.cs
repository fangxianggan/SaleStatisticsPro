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
using EntitiesModels;
using FXKJ.Infrastructure.Core.Extensions;
using FXKJ.Infrastructure.Entities.QueryModel;


namespace FXKJ.Infrastructure.DataAccess
{
    public class EFRepository<T> : BaseRepository, IEFRepository<T>
        where T : class, new()

    {
        //private readonly DbContext _db;
        //public EFRepository(DbContext db)
        //{
        //    _db = db;
        //}


        public virtual T Add(T model)
        {
            using (var dbContext = new MyContext())
            {
                dbContext.Set<T>().Add(model);
                 dbContext.SaveChanges();
                return model;
            }
        }

        public virtual List<T> AddList(IEnumerable<T> list)
        {
            using (var dbContext = new MyContext())
            {
                foreach (var _model in list)
                {
                    dbContext.Set<T>().Add(_model);
                }
                 dbContext.SaveChanges();
                return list.ToList();
            }
        }
        public virtual T Update(T model)
        {
            using (var dbContext = new MyContext())
            {

                //反射更新时间字段
                Type type = model.GetType();
                foreach (var item in type.GetRuntimeProperties())
                {
                    if (item.Name == "UpdateTime")
                    {
                        item.SetValue(model, DateTime.Now);
                        break;
                    }
                }
                dbContext.Entry(model).State = EntityState.Modified;
                 dbContext.SaveChanges();
                return model;
            }
        }

        public virtual List<T> UpdateList(IEnumerable<T> list)
        {
            using (var dbContext = new MyContext())
            {
                foreach (var model in list)
                {
                    //反射更新时间字段
                    Type type = model.GetType();
                    foreach (var item in type.GetRuntimeProperties())
                    {
                        if (item.Name == "UpdateTime")
                        {
                            item.SetValue(model, DateTime.Now);
                            break;
                        }
                    }
                    dbContext.Entry(model).State = EntityState.Modified;
                }
                 dbContext.SaveChanges();
                return list.ToList();
            }
        }

        public virtual int Delete(params object[] keyValues)
        {
            using (var dbContext = new MyContext())
            {
                var _model = dbContext.Set<T>().Find(keyValues);
                dbContext.Set<T>().Remove(_model);
                return  dbContext.SaveChanges();
            }
        }

        public virtual int Delete(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = new MyContext())
            {
                var _modelList = dbContext.Set<T>().Where(whereLambda);
                foreach (var _model in _modelList)
                {
                    dbContext.Set<T>().Remove(_model);
                }
                return  dbContext.SaveChanges();
            }
        }

        public virtual T GetEntity(params object[] keyValues)
        {
            using (var dbContext = new MyContext())
            {
                var _model = dbContext.Set<T>().Find(keyValues);
                return  _model;
            }
        }

        public virtual T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = new MyContext())
            {
                return  dbContext.Set<T>().Where(whereLambda).FirstOrDefault();
            }
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = new MyContext())
            {
                return  dbContext.Set<T>().Where(whereLambda).ToList();
            }
        }

        public virtual List<T> GetPageList(QueryModel queryParam)
        {
            using (var dbContext = new MyContext())
            {
                IQueryable<T> QueryList = dbContext.Set<T>();
                //条件操作
                QueryList = Core.Extensions.QueryableExtensions.Where(QueryList, queryParam);
                //排序操作
                QueryList = _GetOrder(QueryList, queryParam.OrderList);
                queryParam.Total = QueryList.Count();
                return  QueryList.Skip((queryParam.PageIndex - 1) * queryParam.PageSize).Take(queryParam.PageSize).ToList();
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

