using FXKJ.Infrastructure.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.Core.Helper
{

    /// <summary>
    /// 递归数据
    /// </summary>
    public static class RecursiveHelper 
    {

        /// <summary>
        /// 递归 ztree
        /// </summary>
        /// <param name="alllist">数据里的所有节点</param>
        /// <param name="list">根节点</param>
        public static void GetTreeChilds<T,S>(List<T> alllist, ref List<S> list) where T : class
        {
            foreach (var model in list)
            {
                //主键id
                PropertyInfo pkProp = model.GetType().GetProperties().Where(p =>p.Name=="ID").FirstOrDefault();
                var propValue = Convert.ToInt32(pkProp.GetValue(model).ToString());
                //通过上级ID获取子级，然后添加到lstModel中  
                var expression=  PredicateExtensions.BuildPropertyInExpression<T>("ParentId", new List<int>() { propValue });
                List<T> wlstModel = alllist.Where(expression.Compile()).ToList();

                var lstModel= AutoMapperExtension.MapToList<S>(wlstModel);
                if (lstModel.Count > 0)
                {
                    //给children 赋值
                    PropertyInfo propertyInfo = model.GetType().GetProperty("Children"); //获取指定名称的属性  
                    propertyInfo.SetValue(model, lstModel);
                
                    //采用递归的形式  
                    GetTreeChilds(alllist, ref lstModel);
                }
            }
        }


        
    }
}
