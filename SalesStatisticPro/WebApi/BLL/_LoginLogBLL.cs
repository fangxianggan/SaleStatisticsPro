﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="LoginLogBLL.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-04-08 11:41
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.Models;
using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Logic;
using WebApi.IRepository;
using WebApi.IBLL;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using EntitiesModels.Enum;
namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——LoginLog
    /// </summary>
    public partial class LoginLogBLL :ILoginLogBLL
    { 
       private readonly ILogic<LoginLog> _logic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logic"></param>
        public LoginLogBLL(ILogic<LoginLog> logic)
        {
           _logic = logic;
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public HttpReponseModel<int> DelData(params object[] keyValues)
        {
           return  _logic.DeleteLogic(keyValues);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public HttpReponseModel<LoginLog> GetModel(params object[] keyValues)
        {
          return  _logic.GetEntityLogic(keyValues);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HttpReponseModel<List<LoginLog>> GetPageList(QueryModel model)
        {
          return  _logic.GetPageListLogic(model);
        }


       /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public HttpReponseModel<List<LoginLog>> GetList(Expression<Func<LoginLog, bool>> whereLambda)
        {
           return  _logic.GetListLogic(whereLambda);
        }

        /// <summary>
        /// 添加修改 保存单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public HttpReponseModel<LoginLog> SaveData(LoginLog entity)
        {
          
       
       if (entity.LoginLogId.ToString()=="")
       {
        return  _logic.AddLogic(entity);
       }
      else
       {
         return  _logic.UpdateLogic(entity);
       }
                 }

        /// <summary>
        /// 保存数据 多条 新增或是修改
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public HttpReponseModel<List<LoginLog>> SaveData(List<LoginLog> entityList)
        {

 
        if (entityList.FirstOrDefault().LoginLogId.ToString()=="")
{
     return  _logic.AddListLogic(entityList);
}
else
{
    return  _logic.UpdateListLogic(entityList);
}
           
        }


    }
}

