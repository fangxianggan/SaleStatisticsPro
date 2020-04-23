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
using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Log;
using EntitiesModels.Enum;
using FXKJ.Infrastructure.Core.Util;
using FXKJ.Infrastructure.Dapper;
using FXKJ.Infrastructure.Core.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
                //操作日志
                WriteDataLog(GetDataLog(model, OperateType.Add));
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
                //操作日志
                WriteDataLog(GetDataLog(model, OperateType.Add));
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }

        public virtual List<T> AddList(IEnumerable<T> list)
        {
            using (var dbContext = new MyContext())
            {
                List<T> qL = new List<T>();
                foreach (var _model in list)
                {
                    var t = _SetPropertiesAddData(_model);
                    qL.Add(t);
                    dbContext.Set<T>().Add(t);
                }
                //数据操作日志 
                WriteDataLog(GetDataLog(qL.AsQueryable(), OperateType.Add));
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
                //数据操作日志 
                WriteDataLog(GetDataLog(model, OperateType.Edit));
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
                //数据操作日志 
                WriteDataLog(GetDataLog(model, OperateType.Edit));
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }
        public virtual List<T> UpdateList(IEnumerable<T> list)
        {
            using (var dbContext = new MyContext())
            {
                IList<T> qL = new List<T>();
                foreach (var model in list)
                {
                    var t = _SetPropertiesUpdateData(model);
                    qL.Add(t);
                    dbContext.Entry(t).State = EntityState.Modified;
                }
                //数据操作日志 批量操作
                WriteDataLog(GetDataLog(qL.AsQueryable(), OperateType.Edit));
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
                //数据操作日志
                WriteDataLog(GetDataLog(_model, OperateType.Delete));
                return dbContext.SaveChanges();
            }
        }

        public virtual int Delete(Expression<Func<T, bool>> whereLambda)
        {
            using (var dbContext = new MyContext())
            {
                var _modelList = dbContext.Set<T>().Where(whereLambda);
                //数据操作日志 批量操作
                WriteDataLog(GetDataLog(_modelList, OperateType.Delete));
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
            if (currentMerchantInfo != null)
            {
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
            }
           
            return t;
        }
        private T _SetPropertiesUpdateData(T t)
        {
            var currentMerchantInfo = FormAuthenticationExtension.CurrentAuth();
            if (currentMerchantInfo != null)
            {
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
            }
          
            return t;
        }

        private DataLog GetDataLog(T model, OperateType operateType)
        {
               Type ts = model.GetType();
               var tableName = ts.GetCustomAttribute<TableAttribute>().Name;
                
                T t1 = null;//原来
                T t2 = null;//新的

                DataLog datalog = new DataLog
                {
                    OperateType = operateType.ToString(),
                    OperateTable = tableName
                };
                if (operateType == OperateType.Edit)
                {
                    #region 之前的数据
                   
                    var name = ts.GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault().Name;
                    var prikey = ts.GetProperty(name).GetValue(model, null);
                    t1 = GetEntity(prikey);
                    #endregion
                    t2 = model;
                    var ent = CompareModelUtil<T>.CompareModel(t1, t2);
                    datalog.OperationBefore = t1 == null ? "" : JsonUtil.JsonSerialize(ent.OldModelList);
                    datalog.OperationAfterData = t2 == null ? "" : JsonUtil.JsonSerialize(ent.NewModelList);
                }
                else if (operateType == OperateType.Delete)
                {
                    t1 = model;
                    datalog.OperationBefore = t1 == null ? "" : JsonUtil.JsonSerialize(t1);
                    datalog.OperationAfterData = t2 == null ? "" : JsonUtil.JsonSerialize(t2);
                }
                else
                {
                    t2 = model;
                    datalog.OperationBefore = t1 == null ? "" : JsonUtil.JsonSerialize(t1);
                    datalog.OperationAfterData = t2 == null ? "" : JsonUtil.JsonSerialize(t2);
                }
                return datalog;
            
        }

        private DataLog GetDataLog(IQueryable<T> list, OperateType operateType)
        {

            Type t = typeof(T);
            var tableName = t.GetCustomAttribute<TableAttribute>().Name;

            IQueryable<T> t1 = null;//原来
            IQueryable<T> t2 = null;//新的

            DataLog datalog = new DataLog
            {
                OperateType = operateType.ToString(),
                OperateTable = tableName
            };
            if (operateType == OperateType.Edit)
            {
                #region 之前的数据
                Dictionary<string, object> oldDic = new Dictionary<string, object>();
                Dictionary<string, object> newDic = new Dictionary<string, object>();
                t2 = list;
                foreach (var item in list)
                {
                    var ts = item.GetType();
                    var name = ts.GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault().Name;
                    var prikey = ts.GetProperty(name).GetValue(item, null);
                    var oldT = GetEntity(prikey);
                    #endregion
                    var ent = CompareModelUtil<T>.CompareModel(oldT, item);
                    oldDic.Add(prikey.ToString(), ent.OldModelList);
                    newDic.Add(prikey.ToString(), ent.NewModelList);
                }
               
                datalog.OperationBefore = t2 == null ? "" : JsonUtil.JsonSerialize(oldDic);
                datalog.OperationAfterData = t2 == null ? "" : JsonUtil.JsonSerialize(newDic);
            }
            else if (operateType == OperateType.Delete)
            {
                t1 = list;
                datalog.OperationBefore = t1 == null ? "" : JsonUtil.JsonSerialize(t1);
                datalog.OperationAfterData = t2 == null ? "" : JsonUtil.JsonSerialize(t2);
            }
            else
            {
                t2 = list;
                datalog.OperationBefore = t1 == null ? "" : JsonUtil.JsonSerialize(t1);
                datalog.OperationAfterData = t2 == null ? "" : JsonUtil.JsonSerialize(t2);
            }
            return datalog;

        }
        private void WriteDataLog(DataLog log)
        {
            DataLogHandler handler = new DataLogHandler(log.OperateType, log.OperateTable, log.OperationBefore, log.OperationAfterData);
            handler.WriteLog();
        }
    }
}

