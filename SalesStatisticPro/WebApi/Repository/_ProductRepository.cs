﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="ProductRepository.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-04-08 11:41
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels;
using EntitiesModels.Models;
using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.DataAccess;
using WebApi.IRepository;
using FXKJ.Infrastructure.Dapper;
using System.Configuration;
namespace WebApi.Repository
{
    /// <summary>
    ///   数据访问层——Product
    /// </summary>
    public partial class ProductRepository : IProductRepository
    { 
          private readonly string connString = "MyStrConn";
         /// <summary>
        ///   数据——
       /// </summary>
         public ProductRepository()
         {
           SqlMapperUtil.ConnectionName=connString;
         }
    }
}

