﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="UserInfoBLL.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-03-15 20:51
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.Models;
using FXKJ.Infrastructure.Logic;
using WebApi.IRepository;
using WebApi.IBLL;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——UserInfo
    /// </summary>
    public partial class UserInfoBLL :IUserInfoBLL
    { 
       private readonly ILogic<UserInfo> _logic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logic"></param>
        public UserInfoBLL(ILogic<UserInfo> logic)
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
        public HttpReponseModel<UserInfo> GetModel(params object[] keyValues)
        {
            return  _logic.GetEntityLogic(keyValues);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HttpReponseModel<List<UserInfo>> GetPageList(QueryModel model)
        {
            return  _logic.GetPageListLogic(model);
        }


       /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public HttpReponseModel<List<UserInfo>> GetList(Expression<Func<UserInfo, bool>> whereLambda)
        {
            return  _logic.GetListLogic(whereLambda);
        }

        /// <summary>
        /// 添加修改 保存单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public HttpReponseModel<UserInfo> SaveData(UserInfo entity)
        {
            if (entity.ID == 0)
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
        public HttpReponseModel<List<UserInfo>> SaveData(List<UserInfo> entityList)
        {
            var entity = entityList.FindLast(p => true);
            if (entity.ID == 0)
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

