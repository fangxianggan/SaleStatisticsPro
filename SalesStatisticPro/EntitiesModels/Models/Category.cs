using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 分类
    /// </summary>
    /// 
    [Table("Category")]      
    public partial class Category: BaseEntity
    {
        /// <summary>
        ///分类编码
        /// </summary>
        /// 
        [MaxLength(64),Required,DisplayName("分类编码")]
        public string CategoryCode { set; get; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(32),Required,DisplayName("分类名称")]
        public  string CategoryName { set;get;}

    }
}
