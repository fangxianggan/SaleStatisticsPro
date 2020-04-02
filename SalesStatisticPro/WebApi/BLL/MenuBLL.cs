﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//       如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="MenuBLL.cs">
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
using FXKJ.Infrastructure.Core.Helper;
using FXKJ.Infrastructure.Core.Extensions;

namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——Menu
    /// </summary>
    public partial class MenuBLL : IMenuBLL
    {

        private readonly IEFRepository<Menu> _menuEFRepository;

        private readonly IEFRepository<RoleMenu> _roleMenuEFRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logic"></param>
        public MenuBLL(IEFRepository<RoleMenu> roleMenuEFRepository, IEFRepository<Menu> menuEFRepository, ILogic<Menu> logic) : this(logic)
        {
            _menuEFRepository = menuEFRepository;
            _roleMenuEFRepository = roleMenuEFRepository;
        }

        public HttpReponseModel<List<TreeViewModel>> GetTreeListView(int id)
        {
            HttpReponseModel<List<TreeViewModel>> httpReponse = new HttpReponseModel<List<TreeViewModel>>();
            List<TreeViewModel> trees = new List<TreeViewModel>();
            List<Menu> menus = _menuEFRepository.GetList(p => true);

            TreeViewModel treeView = new TreeViewModel();
            if (id == 0)
            {
                treeView.ID = 0;
                treeView.Label = "根节点";
                treeView.Children = new List<TreeViewModel>();
                trees.Add(treeView);
                RecursiveHelper.GetTreeChilds<Menu, TreeViewModel>(menus, ref trees);
            }
            else
            {


            }
            httpReponse.Data = trees;
            return httpReponse;
        }

        public HttpReponseModel<List<MenuViewModel>> GetMenuPageList(QueryModel model)
        {
            HttpReponseModel<List<MenuViewModel>> httpReponse = new HttpReponseModel<List<MenuViewModel>>();
            List<MenuViewModel> trees = new List<MenuViewModel>();
            List<Menu> menus = _menuEFRepository.GetList(p => true);
            MenuViewModel treeView = new MenuViewModel();
            treeView.ID = 0;
            treeView.Children = new List<MenuViewModel>();
            trees.Add(treeView);
            RecursiveHelper.GetTreeChilds<Menu, MenuViewModel>(menus, ref trees);
            httpReponse.Data = trees;
            return httpReponse;
        }

        public HttpReponseModel<bool> SetRoleMenuPermission(RoleMenuViewModel model)
        {
            HttpReponseModel<bool> httpReponse = new HttpReponseModel<bool>();
            List<RoleMenu> list = new List<RoleMenu>();
            _roleMenuEFRepository.Delete(p => p.RoleCode == model.RoleCode);
            foreach (var item in model.MenuIds)
            {
                RoleMenu roleMenu = new RoleMenu();
                roleMenu.RoleCode = model.RoleCode;
                roleMenu.MenuId = item;
                list.Add(roleMenu);
            }
            _roleMenuEFRepository.AddList(list);
            return httpReponse;
        }


        public HttpReponseModel<int[]> GetRoleMenuPermission(string roleCode)
        {
            HttpReponseModel<int[]> httpReponse = new HttpReponseModel<int[]>();
            httpReponse.Data = _roleMenuEFRepository.GetList(p => p.RoleCode == roleCode).Select(s => s.MenuId).ToArray();
            return httpReponse;
        }

        public HttpReponseModel<List<MenuRouterViewModel>> GetMenuRouterList(string[] roleCodes)
        {
            HttpReponseModel<List<MenuRouterViewModel>> httpReponse = new HttpReponseModel<List<MenuRouterViewModel>>();
            List<int> menusId = _roleMenuEFRepository.GetList(p => roleCodes.Contains(p.RoleCode)).Select(p => p.MenuId).Distinct().ToList();
            List<Menu> menus = _menuEFRepository.GetList(p => menusId.Contains(p.ID)).ToList();
            List<MenuViewModel> trees = new List<MenuViewModel>();
            MenuViewModel treeView = new MenuViewModel();
            treeView.ID = 0;
            treeView.Children = new List<MenuViewModel>();
            trees.Add(treeView);
            RecursiveHelper.GetTreeChilds<Menu, MenuViewModel>(menus, ref trees);
            httpReponse.Data = AutoMapperExtension.MapTo<List<MenuRouterViewModel>>(trees[0].Children);
            return httpReponse;
        }



    }
}

