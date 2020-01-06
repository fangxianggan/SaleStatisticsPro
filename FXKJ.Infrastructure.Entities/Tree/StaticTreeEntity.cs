using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Entities.Tree
{
    public static class StaticTreeEntity
    {


        /// <summary>
        /// 递归 ztree
        /// </summary>
        /// <param name="alllist">数据里的所有节点</param>
        /// <param name="list">根节点</param>
        public static void GetTreeChilds(List<TreeEntity> alllist, ref List<TreeEntity> list)
        {
            foreach (var model in list)
            {
                //通过上级ID获取子级，然后添加到lstModel中  
                List<TreeEntity> lstModel = alllist.Where(p => p.pId.ToString() == model.id.ToString()).ToList();
                if (lstModel.Count > 0)
                {
                    model.children = lstModel;
                    model.open = true;//张开
                    //采用递归的形式  
                    GetTreeChilds(alllist, ref lstModel);
                }
            }
        }


        /// <summary>
        /// 递归 ztree
        /// </summary>
        /// <param name="alllist">数据里的所有节点</param>
        /// <param name="list">根节点</param>
        public static void GetTreeMenuChilds(List<Define_Sys_Menu> alllist, ref List<Define_Sys_Menu> list)
        {
            foreach (var model in list)
            {
                //通过上级ID获取子级，然后添加到lstModel中  
                List<Define_Sys_Menu> lstModel = alllist.Where(p => p.MenuParentId.ToString() == model.MenuId.ToString()).ToList();
                if (lstModel.Count > 0)
                {
                    model.Children = lstModel;
                    //  model.open = true;//张开
                    //采用递归的形式  
                    GetTreeMenuChilds(alllist, ref lstModel);
                }
            }
        }
    }
}
