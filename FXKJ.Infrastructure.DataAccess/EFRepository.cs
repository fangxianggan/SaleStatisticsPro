using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EntitiesModels;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Core.Extensions;
using EntitiesModels.QueryModels;

namespace FXKJ.Infrastructure.DataAccess
{
    public class EFRepository<T> : BaseRepository, IEFRepository<T>
        where T : class, new()

    {
        public EFRepository()
        {

        }

        public virtual T Add(T model)
        {
            using (var dbContext = new MyContext())
            {
                model = _SetPropertiesAddData(model);
                dbContext.Set<T>().Add(model);
                dbContext.SaveChanges();
                return model;
            }
        }

        public virtual bool Add(T model, bool retType)
        {
            using (var dbContext = new MyContext())
            {
                model = _SetPropertiesAddData(model);
                dbContext.Set<T>().Add(model);
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }

        public virtual List<T> AddList(IEnumerable<T> list)
        {
            using (var dbContext = new MyContext())
            {
                foreach (var _model in list)
                {
                    dbContext.Set<T>().Add(_SetPropertiesAddData(_model));
                }
                dbContext.SaveChanges();
                return list.ToList();
            }
        }
        public virtual T Update(T model)
        {
            using (var dbContext = new MyContext())
            {

                model = _SetPropertiesUpdateData(model);
                dbContext.Entry(model).State = EntityState.Modified;
                dbContext.SaveChanges();
                return model;
            }
        }
        public virtual bool Update(T model, bool retType)
        {
            using (var dbContext = new MyContext())
            {
                model = _SetPropertiesUpdateData(model);
                dbContext.Entry(model).State = EntityState.Modified;
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }
        public virtual List<T> UpdateList(IEnumerable<T> list)
        {
            using (var dbContext = new MyContext())
            {
                foreach (var model in list)
                {
                    dbContext.Entry(_SetPropertiesUpdateData(model)).State = EntityState.Modified;
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
                return dbContext.SaveChanges();
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
                return dbContext.SaveChanges();
            }
        }

        public virtual T GetEntity(params object[] keyValues)
        {
            using (var dbContext = new MyContext())
            {
                var _model = dbContext.Set<T>().Find(keyValues);
                return _model;
            }
        }

        public virtual T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = new MyContext())
            {
                return dbContext.Set<T>().Where(whereLambda).FirstOrDefault();
            }
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = new MyContext())
            {

               
                var list = dbContext.Set<T>().Where(whereLambda);
                //权限过滤
                list = _GetFilterPermissionList(list);
                return list.ToList();
            }
        }

        public virtual List<T> GetPageList(QueryModel queryParam)
        {
            using (var dbContext = new MyContext())
            {
                IQueryable<T> QueryList = dbContext.Set<T>();
                //条件操作
                QueryList = Core.Extensions.QueryableExtensions.Where(QueryList, queryParam);
                //权限过滤
                QueryList = _GetFilterPermissionList(QueryList);
                //排序操作
                QueryList = _GetOrder(QueryList, queryParam.OrderList);
                queryParam.Total = QueryList.Count();
                return QueryList.Skip((queryParam.PageIndex - 1) * queryParam.PageSize).Take(queryParam.PageSize).ToList();
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

        /// <summary>
        /// 查询数据权限过滤
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private IQueryable<T> _GetFilterPermissionList(IQueryable<T> list)
        {
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            var perName = properties.Where(p => p.Name == "P_MerchantNo").FirstOrDefault();
            if (perName != null)
            {
                var currentMerchantInfo = FormAuthenticationExtension.CurrentAuth();
                if (currentMerchantInfo != null)
                {
                    if (!currentMerchantInfo.Roles.Contains("admin"))
                    {
                        string merchantNo = currentMerchantInfo.MerchantNo;
                        var expression = PredicateExtensions.BuildPropertyInExpression<T>("P_MerchantNo", new List<string>() { merchantNo });
                        list = list.Where(expression.Compile()).AsQueryable();
                    }
                }
            }
            return list;
        }


        private T _SetPropertiesAddData(T t)
        {
            var currentMerchantInfo = FormAuthenticationExtension.CurrentAuth();
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (var item in properties)
            {
                switch (item.Name)
                {
                    case "P_MerchantNo":
                        item.SetValue(t, currentMerchantInfo.MerchantNo);
                        break;
                    case "CreateTime":
                        item.SetValue(t, DateTime.Now);
                        break;
                    case "CreateUserCode":
                        item.SetValue(t, currentMerchantInfo.PhoneNumber);
                        break;
                    default: break;
                }
            }
            return t;
        }


        private T _SetPropertiesUpdateData(T t)
        {
            var currentMerchantInfo = FormAuthenticationExtension.CurrentAuth();
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (var item in properties)
            {
                switch (item.Name)
                {

                    case "UpdateTime":
                        item.SetValue(t, DateTime.Now);
                        break;
                    case "UpdateUserCode":
                        item.SetValue(t, currentMerchantInfo.PhoneNumber);
                        break;
                    default: break;
                }
            }

            return t;
        }
    }
}

