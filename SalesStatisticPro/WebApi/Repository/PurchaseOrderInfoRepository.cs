﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="PurchaseOrderInfoRepository.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-02-17 16:30
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Dapper;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Data;
using WebApi.IRepository;
using FXKJ.Infrastructure.Auth;
using FXKJ.Infrastructure.Auth.Auth;

namespace WebApi.Repository
{
    /// <summary>
    ///   数据访问层——PurchaseOrderInfo
    /// </summary>
    public partial class PurchaseOrderInfoRepository : IPurchaseOrderInfoRepository
    {

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pOrderNum"></param>
        /// <returns></returns>
        public IEnumerable<PurchaseOrderInfoViewModel> GetPurchaseOrderInfoViewModelList(string pOrderNum)
        {
            var sql = @" select * from ( SELECT a.*,b.ProductName,b.SimpleCode ,b.ProductCode FROM  [dbo].[PurchaseOrderInfo] AS a WITH (NOLOCK) 
 LEFT JOIN [dbo].[Product] AS  b WITH (NOLOCK) ON a.PProductCode=b.ProductCode

 WHERE a.POrderNum='" + pOrderNum + "') as a "+ permissionWhere + " ";
            var list = SqlMapperUtil.GetListData<PurchaseOrderInfoViewModel>(sql);
            return list;

        }

        public DataTable GetPurchaseOrderInfoViewModelDataTable(QueryModel model)
        {
            var sql = @" select * from ( SELECT  
a.POrderNum ,
a.POrderTitle,
a.POrderCreateTime,
b.BusinessCode,
b.BusinessName,
c.TransferBinCode,
c.TransferBinName,
a.AllPurchaseAmount,
a.AllInternationFreightAmount,
a.AllDomesticFreightAmount,
a.AllPurchaseSettlementAmount,
a.AllAmount,
(case when a.PurchaseOrderState='FF01' then '未完成' else '已锁定' end) as PurchaseOrderState,
d.PPOrderNum,
d.ExpressNumber,
d.InternationNumber,
d.DomesticNumber,
e.ProductName,
e.SimpleCode,
f.CategoryName,
d.PurchaseCount,
d.PurchasePrice,
d.PurchaseAmount,
d.InternationFreightAmount,
d.DomesticFreightAmount,
d.Amount,
d.PurchaseSettlementAmount,
case when d.PurchaseOrderInfoState='SS01' then '未发货'  
when  d.PurchaseOrderInfoState='SS02' then '已发货'
 when d.PurchaseOrderInfoState='SS03' then '已签收'
 else  '已丢失' end as PurchaseOrderInfoState
 FROM [dbo].[PurchaseOrder] AS a WITH(NOLOCK) 
LEFT JOIN [dbo].[Business] AS b WITH (NOLOCK) ON a.BusinessCode=b.BusinessCode
left join [dbo].[TransferBin] as c with (nolock) on a.TransferBinCode=c.TransferBinCode
left join [dbo].[PurchaseOrderInfo] AS d WITH(NOLOCK)  on a.POrderNum=d.POrderNum
left join [dbo].[Product] AS e WITH (NOLOCK) ON d.PProductCode=e.ProductCode
left join [dbo].[Category] AS f WITH (NOLOCK) ON f.CategoryCode=e.CategoryCode
"+ permissionWhere + " ) as cc @where  ";
            return SqlMapperUtil.GetDataTable(sql, model);
        }
    }
}

