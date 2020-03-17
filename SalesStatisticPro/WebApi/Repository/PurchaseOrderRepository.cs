﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="PurchaseOrderRepository.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-02-17 16:30
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels;
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Dapper;
using FXKJ.Infrastructure.DataAccess;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WebApi.IRepository;
namespace WebApi.Repository
{
    /// <summary>
    ///   数据访问层——PurchaseOrder
    /// </summary>
    public partial class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<PurchaseOrderViewModel> GetPurchaseOrderViewModelPageList(QueryModel model)
        {

            var sql = @" select * from (SELECT distinct  a.*,BusinessName,TransferBinName ,a.POrderNum as Code FROM [dbo].[PurchaseOrder] AS a WITH(NOLOCK) 
LEFT JOIN [dbo].[Business] AS b WITH (NOLOCK) ON a.BusinessCode=b.BusinessCode
left join [dbo].[TransferBin] as c with (nolock) on a.TransferBinCode=c.TransferBinCode
left join [dbo].[PurchaseOrderInfo] AS d WITH(NOLOCK)  on a.POrderNum=d.POrderNum) as cc
@where @orderBy @page";
            var list = SqlMapperUtil.PagingQueryAsync<PurchaseOrderViewModel>(sql, model);
            return list;

        }

        /// <summary>
        /// 库存统计
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductStockViewModel> GetProductStockViewModelList(string keyName,string productCode)
        {
            string where = "  where  ss.ProductStock>0  ";
            if (!string.IsNullOrEmpty(keyName)) {

                where += @" and ( ProductName like '%" + keyName + "%' OR  SimpleCode like '%" + keyName + "%' ) ";
               // where += @"  OR  BusinessName like '%" + keyName + "%' OR BrandName like '%" + keyName + "%' ";
               // where += @" OR CategoryName like '%" + keyName + "%' OR SpecsName like '5" + keyName + "%' ";
               // where += @" OR UnitName like '%" + keyName + "%' OR ProductColor like '%" + keyName + "%' ";
              //  where += @" OR ProductTypeName like '%" + keyName + "%' ) ";
            }
            if (!string.IsNullOrEmpty(productCode))
            {
                where += @" and  ProductCode = '" + productCode + "' ";
            }
            var sql = @" WITH  p as(
SELECT 
a.PProductCode AS ProductCode,
SUM(a.PurchaseCount) AS PurchaseCount  
FROM [dbo].[PurchaseOrderInfo]  AS a WITH (NOLOCK)
GROUP BY a.PProductCode
),
s AS (
SELECT 
a.SProductCode AS ProductCode,
SUM(a.SaleCount) AS SaleCount
 FROM  [dbo].[SaleOrderInfo] AS a WITH (NOLOCK)
GROUP BY a.SProductCode
),
ss AS (
SELECT 
p.ProductCode,
t.SimpleCode,
t.ProductName,
(p.PurchaseCount - (CASE WHEN  s.SaleCount IS NULL THEN 0 ELSE s.SaleCount END )) AS ProductStock
FROM p LEFT JOIN s ON p.ProductCode=s.ProductCode
LEFT JOIN dbo.Product AS t ON  p.ProductCode=t.ProductCode

)
SELECT * FROM ss 
" + where+" ";
            var list = SqlMapperUtil.SqlWithParams<ProductStockViewModel>(sql);
            return list;
        }



        public DataTable GetPurchaseOrderViewModelDataTable(QueryModel model)
        {
            var sql = @" 
select * from (SELECT distinct 
a.POrderNum ,
a.POrderTitle,
a.POrderCreateTime,
a.USANumber,
b.BusinessCode,
b.BusinessName,
c.TransferBinCode,
c.TransferBinName,
a.AllPurchaseAmount,
a.AllInternationFreightAmount,
a.AllDomesticFreightAmount,
a.AllPurchaseSettlementAmount,
a.AllAmount,
(case when a.PurchaseOrderState='FF01' then '未完成' else '已锁定' end) as PurchaseOrderState
FROM [dbo].[PurchaseOrder] AS a WITH(NOLOCK) 
LEFT JOIN [dbo].[Business] AS b WITH (NOLOCK) ON a.BusinessCode=b.BusinessCode
left join [dbo].[TransferBin] as c with (nolock) on a.TransferBinCode=c.TransferBinCode
left join [dbo].[PurchaseOrderInfo] AS d WITH(NOLOCK)  on a.POrderNum=d.POrderNum) as cc
@where  ";
            return SqlMapperUtil.GetDataTable(sql, model);
        }
    }
}

