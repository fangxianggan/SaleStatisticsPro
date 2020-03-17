﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="IMerchantRoleBLL.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-03-15 20:51
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——MerchantRole
    /// </summary>
    public partial interface IMerchantRoleBLL
    {

       

        /// <summary>
        ///获取分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HttpReponseModel<List<MerchantRole>> GetPageList(QueryModel model);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        HttpReponseModel<int> DelData(params object[] keyValues);

        /// <summary>
        /// 添加 修改 当条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        HttpReponseModel<MerchantRole> SaveData(MerchantRole entity);

        /// <summary>
        /// 添加 修改 多条数据
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        HttpReponseModel<List<MerchantRole>> SaveData(List<MerchantRole> entityList);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        HttpReponseModel<MerchantRole> GetModel(params object[] keyValues);

         /// <summary>
        /// 获取list数组
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        HttpReponseModel<List<MerchantRole>> GetList(Expression<Func<MerchantRole, bool>> whereLambda);
       
    }
}

