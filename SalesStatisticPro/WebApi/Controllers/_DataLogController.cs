﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="_DataLogController.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-04-22 13:13
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
using FXKJ.Infrastructure.Core.Attributes;
namespace WebApi.Controllers

{
   /// <summary>
    /// DataLog 控制器api 代码自动生成
    /// </summary>
    [RoutePrefix("dev-api/DataLog")]
    public partial class DataLogController : ApiController
    { 
        private readonly IDataLogBLL _dataLogBLL;
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name="dataLogBLL"></param>
        public DataLogController(IDataLogBLL dataLogBLL)
        {
            _dataLogBLL = dataLogBLL;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// 
        [Route("_GetPageList")]
        [HttpPost]
        [ActionRecord(Describe = "获取分页数据")] 
        public HttpReponseModel<List<DataLog>> GetPageList([FromBody] QueryModel query)
        {
            return  _dataLogBLL.GetPageList(query);
        }

        /// <summary>
        /// 保存数据 当前数据
        /// </summary>
        /// <param name="dataLog"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("_SaveData")]
        [ActionRecord(Describe = "保存数据")] 
        public HttpReponseModel<DataLog> Post(DataLog dataLog)
        {
            return  _dataLogBLL.SaveData(dataLog);
        }


        /// <summary>
        /// 保存数据 多条数据 add edit
        /// </summary>
        /// <param name="dataLogList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("_SaveListData")]
        [ActionRecord(Describe = "保存数据 多条数据 add edit")] 
        public HttpReponseModel<List<DataLog>> PostListData(List<DataLog> dataLogList)
        {
            return  _dataLogBLL.SaveData(dataLogList);
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("_DelData")]
        [ActionRecord(Describe = "删除数据")]
        public HttpReponseModel<int> Delete(Guid id)
        {
            return  _dataLogBLL.DelData(id);
        }



    }
}

