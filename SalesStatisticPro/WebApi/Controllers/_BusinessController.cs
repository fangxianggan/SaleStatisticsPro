﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="_BusinessController.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-04-08 11:41
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.Models;
using EntitiesModels.Models.SysModels;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.IBLL;
using System;
namespace WebApi.Controllers

{
   /// <summary>
    /// Business 控制器api 代码自动生成
    /// </summary>
    [RoutePrefix("dev-api/Business")]
    public partial class BusinessController : ApiController
    { 
        private readonly IBusinessBLL _businessBLL;
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name="businessBLL"></param>
        public BusinessController(IBusinessBLL businessBLL)
        {
            _businessBLL = businessBLL;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        [Route("_GetPageList")]
        [HttpPost]
        public HttpReponseModel<List<Business>> GetPageList([FromBody] QueryModel query)
        {
            return  _businessBLL.GetPageList(query);
        }

        /// <summary>
        /// 保存数据 当前数据
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("_SaveData")]
        public HttpReponseModel<Business> Post(Business business)
        {
            return  _businessBLL.SaveData(business);
        }


        /// <summary>
        /// 保存数据 多条数据 add edit
        /// </summary>
        /// <param name="businessList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("_SaveListData")]
        public HttpReponseModel<List<Business>> PostListData(List<Business> businessList)
        {
            return  _businessBLL.SaveData(businessList);
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
            return  _businessBLL.DelData(id);
        }



    }
}

