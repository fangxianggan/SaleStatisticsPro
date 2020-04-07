﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="_UserInfoController.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-02-22 14:57
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using FXKJ.Infrastructure.WebApi.Filter;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;
namespace WebApi.Controllers

{
   /// <summary>
    /// UserInfo 控制器api 代码自动生成
    /// </summary>
  [ApiAuthorize]
    public partial class UserInfoController : ApiController
    {

       
        /// <summary>
        /// 构造器 依赖注入
        /// </summary>
        /// <param name=""></param>
        public UserInfoController()
        {
           
        }



        /// <summary>
        /// 获取select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectList")]
        public HttpReponseModel<List<SelectViewModel>> GetSelectList()
        {
            return _userInfoBLL.GetSelectList();
        }


        /// <summary>
        /// 获取select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserInfoList")]
        public HttpReponseModel<List<UserInfo>> GetUserInfoList(string keyName)
        {
            return _userInfoBLL.GetUserInfoList(keyName);
        }

        /// <summary>
        /// 判断该供应商能不能删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<int> GetIsDeleteFlag(string phoneNumber)
        {
            return _userInfoBLL.GetIsDeleteFlag(phoneNumber);
        }


    }
}

