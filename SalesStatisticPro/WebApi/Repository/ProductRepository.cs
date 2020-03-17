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
//        生成时间：2020-02-21 16:20
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.DataAccess;
using WebApi.IRepository;
using FXKJ.Infrastructure.Dapper;
using System.Configuration;
using System.Threading.Tasks;
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Data;

namespace WebApi.Repository
{
    /// <summary>
    ///   数据访问层——Product
    /// </summary>
    public partial class ProductRepository : IProductRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public IEnumerable<ProductViewModel> GetProductViewModelPageList(QueryModel model)
        {
            var sql = @"  SELECT 
b.*,a.CategoryName
from [dbo].[Product] AS b WITH (NOLOCK)
left join [dbo].[Category] AS a WITH (NOLOCK) 
on a.CategoryCode=b.CategoryCode
@where @orderBy @page";
            var list =  SqlMapperUtil.PagingQueryAsync<ProductViewModel>(sql, model);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<ProductStatisticsViewModel> GetProductStatisticsViewModelPageList(QueryModel model)
        {
            var sql = @"  WITH  p as(
SELECT 
a.PProductCode AS ProductCode,
SUM(a.PurchaseCount) AS PurchaseCount  ,
SUM(a.PurchaseAmount) AS PurchaseAmount ,
SUM(a.DomesticFreightAmount+a.InternationFreightAmount) AS PurchaseFreightAmount,
SUM(a.Amount) AS AllPurchaseAmount,
sum(a.PurchaseSettlementAmount) as PurchaseSettlementAmount
FROM [dbo].[PurchaseOrderInfo]  AS a WITH (NOLOCK)
GROUP BY a.PProductCode
),
s AS (
SELECT 
a.SProductCode AS ProductCode,
SUM(a.SaleCount) AS SaleCount,
SUM(a.SaleAmount) AS SaleAmount,
SUM(a.SaleFreightAmount) AS SaleFreightAmount,
SUM(a.SaleSumAmount) AS AllSaleAmount,
sum(a.SaleSettlementAmount) as SaleSettlementAmount
 FROM  [dbo].[SaleOrderInfo] AS a WITH (NOLOCK)
GROUP BY a.SProductCode
)
SELECT 
ROW_NUMBER() OVER (ORDER BY t.ID) AS ID,
p.ProductCode,
p.PurchaseCount,
p.PurchaseAmount,
p.PurchaseFreightAmount,
p.PurchaseSettlementAmount,
p.AllPurchaseAmount,
(CASE WHEN  s.SaleCount IS NULL THEN 0 ELSE s.SaleCount END) AS SaleCount ,
(CASE WHEN  s.SaleAmount IS NULL THEN 0 ELSE s.SaleAmount END) AS SaleAmount ,
(CASE WHEN  s.SaleFreightAmount IS NULL THEN 0 ELSE s.SaleFreightAmount END) AS SaleFreightAmount ,
(CASE WHEN  s.AllSaleAmount IS NULL THEN 0 ELSE s.AllSaleAmount END) AS AllSaleAmount ,
(CASE WHEN  s.SaleSettlementAmount IS NULL THEN 0 ELSE s.SaleSettlementAmount END) AS SaleSettlementAmount ,
t.SimpleCode,
t.ProductName,
(p.PurchaseCount - (CASE WHEN  s.SaleCount IS NULL THEN 0 ELSE s.SaleCount END )) AS Stock,
((CASE WHEN s.AllSaleAmount IS NULL THEN 0 ELSE s.AllSaleAmount END)- p.AllPurchaseAmount) AS ProfitAmount
FROM p LEFT JOIN s ON p.ProductCode=s.ProductCode
LEFT JOIN dbo.Product AS t ON  p.ProductCode=t.ProductCode
@where @orderBy @page";
            var list = SqlMapperUtil.PagingQueryAsync<ProductStatisticsViewModel>(sql, model);
            return list;
        }


        public DataTable GetProductStatisticsViewModelDataTable(QueryModel model)
        {
            var sql = @"  WITH  p as(
SELECT 
a.PProductCode AS ProductCode,
SUM(a.PurchaseCount) AS PurchaseCount  ,
SUM(a.PurchaseAmount) AS PurchaseAmount ,
SUM(a.DomesticFreightAmount+a.InternationFreightAmount) AS PurchaseFreightAmount,
SUM(a.Amount) AS AllPurchaseAmount,
sum(a.PurchaseSettlementAmount) as PurchaseSettlementAmount
FROM [dbo].[PurchaseOrderInfo]  AS a WITH (NOLOCK)
GROUP BY a.PProductCode

),
s AS (
SELECT 
a.SProductCode AS ProductCode,
SUM(a.SaleCount) AS SaleCount,
SUM(a.SaleAmount) AS SaleAmount,
SUM(a.SaleFreightAmount) AS SaleFreightAmount,
SUM(a.SaleSumAmount) AS AllSaleAmount,
sum(a.SaleSettlementAmount) as SaleSettlementAmount
 FROM  [dbo].[SaleOrderInfo] AS a WITH (NOLOCK)
GROUP BY a.SProductCode
)
SELECT 
ROW_NUMBER() OVER (ORDER BY t.ID) AS ID,
t.SimpleCode,
t.ProductName,
(p.PurchaseCount - (CASE WHEN  s.SaleCount IS NULL THEN 0 ELSE s.SaleCount END )) AS Stock,
((CASE WHEN s.AllSaleAmount IS NULL THEN 0 ELSE s.AllSaleAmount END)- p.AllPurchaseAmount) AS ProfitAmount,
p.PurchaseCount,
p.PurchaseAmount,
p.PurchaseFreightAmount,
p.PurchaseSettlementAmount,
p.AllPurchaseAmount,
(CASE WHEN  s.SaleCount IS NULL THEN 0 ELSE s.SaleCount END) AS SaleCount ,
(CASE WHEN  s.SaleAmount IS NULL THEN 0 ELSE s.SaleAmount END) AS SaleAmount ,
(CASE WHEN  s.SaleFreightAmount IS NULL THEN 0 ELSE s.SaleFreightAmount END) AS SaleFreightAmount ,
(CASE WHEN  s.AllSaleAmount IS NULL THEN 0 ELSE s.AllSaleAmount END) AS AllSaleAmount ,
(CASE WHEN  s.SaleSettlementAmount IS NULL THEN 0 ELSE s.SaleSettlementAmount END) AS SaleSettlementAmount 
FROM p LEFT JOIN s ON p.ProductCode=s.ProductCode
LEFT JOIN dbo.Product AS t ON  p.ProductCode=t.ProductCode
@where ";

            return SqlMapperUtil.GetDataTable(sql, model);

        }
    }
}
