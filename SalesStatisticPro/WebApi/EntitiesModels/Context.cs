﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// 
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-04-22 13:13
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.Models;
using EntitiesModels.Models.SysModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EntitiesModels
{
    public class MyContext : DbContext
    {
  
      public MyContext() : base("name=MyStrConn")
        {
                   
        }
        
                /// <summary>
       ///   Business
      /// </summary>
       public DbSet<Business> Business { get; set; }

               /// <summary>
       ///   SqlLog
      /// </summary>
       public DbSet<SqlLog> SqlLog { get; set; }

               /// <summary>
       ///   Menu
      /// </summary>
       public DbSet<Menu> Menu { get; set; }

               /// <summary>
       ///   MerchantRole
      /// </summary>
       public DbSet<MerchantRole> MerchantRole { get; set; }

               /// <summary>
       ///   MerchantInfo
      /// </summary>
       public DbSet<MerchantInfo> MerchantInfo { get; set; }

               /// <summary>
       ///   Product
      /// </summary>
       public DbSet<Product> Product { get; set; }

               /// <summary>
       ///   PurchaseOrder
      /// </summary>
       public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

               /// <summary>
       ///   PurchaseOrderInfo
      /// </summary>
       public DbSet<PurchaseOrderInfo> PurchaseOrderInfo { get; set; }

               /// <summary>
       ///   Role
      /// </summary>
       public DbSet<Role> Role { get; set; }

               /// <summary>
       ///   RoleMenu
      /// </summary>
       public DbSet<RoleMenu> RoleMenu { get; set; }

               /// <summary>
       ///   SaleOrder
      /// </summary>
       public DbSet<SaleOrder> SaleOrder { get; set; }

               /// <summary>
       ///   SaleOrderInfo
      /// </summary>
       public DbSet<SaleOrderInfo> SaleOrderInfo { get; set; }

               /// <summary>
       ///   SysConfig
      /// </summary>
       public DbSet<SysConfig> SysConfig { get; set; }

               /// <summary>
       ///   Category
      /// </summary>
       public DbSet<Category> Category { get; set; }

               /// <summary>
       ///   ExpressCompany
      /// </summary>
       public DbSet<ExpressCompany> ExpressCompany { get; set; }

               /// <summary>
       ///   TransferBin
      /// </summary>
       public DbSet<TransferBin> TransferBin { get; set; }

               /// <summary>
       ///   UserInfo
      /// </summary>
       public DbSet<UserInfo> UserInfo { get; set; }

               /// <summary>
       ///   DataLog
      /// </summary>
       public DbSet<DataLog> DataLog { get; set; }

               /// <summary>
       ///   ExceptionLog
      /// </summary>
       public DbSet<ExceptionLog> ExceptionLog { get; set; }

               /// <summary>
       ///   LoginLog
      /// </summary>
       public DbSet<LoginLog> LoginLog { get; set; }

               /// <summary>
       ///   OperateLog
      /// </summary>
       public DbSet<OperateLog> OperateLog { get; set; }

          }
}

