/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [ID]
      ,[POrderNum]
      ,[PProductCode]
      ,[PurchaseCount]
      ,[PurchasePrice]
      ,[PurchaseAmount]
      ,[CreateTime]
      ,[CreateUserCode]
      ,[UpdateTime]
      ,[UpdateUserCode]
      ,[Remark]
      ,[Amount]
      ,[PPOrderNum]
      ,[InternationNumber]
      ,[DomesticNumber]
      ,[InternationFreightAmount]
      ,[DomesticFreightAmount]
      ,[PurchaseSettlementAmount]
      ,[PurchaseOrderInfoState]
      ,[ExpressCompanyCode]
      ,[ExpressNumber]
      ,[ExpressFreightAmount]
      ,[InternationExpressCompanyCode]
      ,[DomesticExpressCompanyCode]
  FROM [SalesStatisticPro].[dbo].[PurchaseOrderInfo]


  ---从表从主表里更新数据 脚本
update a
set a.ExpressNumber=b.USANumber
from [dbo].[PurchaseOrderInfo]  a,[dbo].[PurchaseOrder]  b 
where a.POrderNum=b.POrderNum

 

 