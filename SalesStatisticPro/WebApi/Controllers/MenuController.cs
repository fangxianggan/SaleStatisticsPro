using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Web.Http;
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.WebApi.Filter;

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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMenuPageList")]
        public HttpReponseModel<List<MenuViewModel>> GetMenuPageList(QueryModel model)
        {
            return _menuBLL.GetMenuPageList(model);
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



        /// <summary>
        /// 设置角色菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [ApiDTC]
        [HttpPost]
        [Route("SetRoleMenuPermission")]
        public HttpReponseModel<bool> SetRoleMenuPermission(RoleMenuViewModel model)
        {
            return _menuBLL.SetRoleMenuPermission(model);
        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="roleCode"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("GetRoleMenuPermission")]
        public HttpReponseModel<int[]> GetRoleMenuPermission([FromBody]string roleCode)
        {
            return _menuBLL.GetRoleMenuPermission(roleCode);
        }

       /// <summary>
       /// 通过角色获取菜单权限
       /// </summary>
       /// <param name="roleCodes"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("GetMenuRouterList")]
        public HttpReponseModel<List<MenuRouterViewModel>> GetMenuRouterList([FromBody]string[] roleCodes)
        {
            return _menuBLL.GetMenuRouterList(roleCodes);
        }


        /// <summary>
        /// 删除条件
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<bool> GetIsDeleteFlag(int menuId)
        {
            return _menuBLL.GetIsDeleteFlag(menuId);
        }


    }
}

