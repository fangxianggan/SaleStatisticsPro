﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="MerchantRoleBLL.cs">
//        Copyright(c)2013 GMFCN.All rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：yxd
//        生成时间：2020-03-20 17:04
// </copyright>
//------------------------------------------------------------------------------
using EntitiesModels.Models;
using EntitiesModels.Models.SysModels;
using FXKJ.Infrastructure.Logic;
using WebApi.IRepository;
using WebApi.IBLL;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using FXKJ.Infrastructure.DataAccess;
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Core.Extensions;

namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——MerchantRole
    /// </summary>
    public partial class MerchantRoleBLL : IMerchantRoleBLL
    {
        private readonly IEFRepository<MerchantRole> _merchantRoleEFRepository;
        private readonly IEFRepository<Role> _roleEFRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logic"></param>
        public MerchantRoleBLL(IEFRepository<Role> roleEFRepository,IEFRepository<MerchantRole> merchantRoleEFRepository, ILogic<MerchantRole> logic) : this(logic)
        {
            _merchantRoleEFRepository = merchantRoleEFRepository;
            _roleEFRepository = roleEFRepository;
        }

        public HttpReponseModel<bool> SetMerchantRolePermission(MerchantRoleViewModel model)
        {
            HttpReponseModel<bool> httpReponse = new HttpReponseModel<bool>();
            List<MerchantRole> list = new List<MerchantRole>();
            _merchantRoleEFRepository.Delete(p => p.MerchantNo == model.MerchantNo);
            foreach (var item in model.RoleCodes)
            {
                list.Add(new MerchantRole { RoleCode = item, MerchantNo = model.MerchantNo });
            }
            _merchantRoleEFRepository.AddList(list);
            return httpReponse;
        }

        public HttpReponseModel<string[]> GetMerchantRolePermission(string merchantNo)
        {
            HttpReponseModel<string[]> httpReponse = new HttpReponseModel<string[]>();
            httpReponse.Data = _merchantRoleEFRepository.GetList(p => p.MerchantNo == merchantNo).Select(p => p.RoleCode).ToArray();
            return httpReponse;
        }


        public HttpReponseModel<SetTransferViewModel> GetRoleTransferData(string merchantNo)
        {
            HttpReponseModel<SetTransferViewModel> httpReponse = new HttpReponseModel<SetTransferViewModel>();
            SetTransferViewModel viewModel = new SetTransferViewModel();
            var roleCodeList=  _merchantRoleEFRepository.GetList(p => p.MerchantNo == merchantNo).Select(p => p.RoleCode);
            viewModel.SetRoleList = roleCodeList.ToArray();
            viewModel.AllRoleList = AutoMapperExtension.MapTo<List<TransferViewModel>>(_roleEFRepository.GetList(p => true));
            httpReponse.Data = viewModel;
            return httpReponse;
        }
    }
}

