﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="_MenuController.cs">
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
using EntitiesModels.DtoModels;

namespace WebApi.Controllers

{
   /// <summary>
    /// Menu 控制器api 代码自动生成
    /// </summary>
   
    public partial class MenuController : ApiController
    { 
       
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name="menuBLL"></param>
        public MenuController()
        {
          
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("GetTreeMenuList")]
        public HttpReponseModel<List<TreeViewModel>> GetTreeMenuList(int id)
        {
            return _menuBLL.GetTreeListView(id);
        }



    }
}

