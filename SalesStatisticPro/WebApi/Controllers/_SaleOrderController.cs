﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="_SaleOrderController.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-03-20 17:04
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.Models;
using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;
using System;
namespace WebApi.Controllers

{
   /// <summary>
    /// SaleOrder 控制器api 代码自动生成
    /// </summary>
    [RoutePrefix("dev-api/SaleOrder")]
    public partial class SaleOrderController : ApiController
    { 
        private readonly ISaleOrderBLL _saleOrderBLL;
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name="saleOrderBLL"></param>
        public SaleOrderController(ISaleOrderBLL saleOrderBLL)
        {
            _saleOrderBLL = saleOrderBLL;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        [Route("_GetPageList")]
        [HttpPost]
        public HttpReponseModel<List<SaleOrder>> GetPageList([FromBody] QueryModel query)
        {
            return  _saleOrderBLL.GetPageList(query);
        }

        /// <summary>
        /// 保存数据 当前数据
        /// </summary>
        /// <param name="saleOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("_SaveData")]
        public HttpReponseModel<SaleOrder> Post(SaleOrder saleOrder)
        {
            return  _saleOrderBLL.SaveData(saleOrder);
        }


        /// <summary>
        /// 保存数据 多条数据 add edit
        /// </summary>
        /// <param name="saleOrderList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("_SaveListData")]
        public HttpReponseModel<List<SaleOrder>> PostListData(List<SaleOrder> saleOrderList)
        {
            return  _saleOrderBLL.SaveData(saleOrderList);
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("_DelData")]
        public HttpReponseModel<int> Delete(Int32 id)
        {
            return  _saleOrderBLL.DelData(id);
        }



    }
}

