﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="ISaleOrderRepository.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-02-21 16:20
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.DataAccess;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace WebApi.IRepository
{
    /// <summary>
    ///   数据访问层接口——SaleOrder
    /// </summary>
    public partial interface ISaleOrderRepository 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<SaleOrderViewModel> GetSaleOrderViewModelPageList(QueryModel model);

        DataTable GetSaleOrderViewModelDataTable(QueryModel model);
    }
}

