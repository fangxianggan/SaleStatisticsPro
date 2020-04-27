using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table("Product")]
    public partial class Product : BaseEntityPermission
    {

        /// <summary>
        /// 产品编码
        /// </summary>
        [MaxLength(64),Required,DisplayName("产品编码")]
        public string ProductCode { set; get; }

        ///// <summary>
        ///// 商户编码code
        ///// </summary>
        //[MaxLength(64)]
        //public string BusinessCode { set; get; }


        ///// <summary>
        ///// 品牌编码code
        ///// </summary>
        //[MaxLength(64)]
        //public string BrandCode { set; get; }

        /// <summary>
        /// 分类编码code
        /// </summary>
        [MaxLength(64)]
        public string CategoryCode { set; get; }

        ///// <summary>
        ///// 型号名称
        ///// </summary>
        //[MaxLength(50)]
        //public string ProductTypeName { set; get; }

        /// <summary>
        /// 产品简码
        /// </summary>
        [MaxLength(64),Required, DisplayName("产品简码")]
        public string SimpleCode { set; get; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [MaxLength(64),Required,DisplayName("产品名称")]
        public string ProductName { set; get; }


        ///// <summary>
        ///// 规格编码code
        ///// </summary>
        //[MaxLength(64)]
        //public string SpecsCode { set; get; }


        ///// <summary>
        ///// 颜色
        ///// </summary>
        //[MaxLength(50)]
        //public string ProductColor { set; get; }


        ///// <summary>
        ///// 单位编码
        ///// </summary>
        //[MaxLength(64)]
        //public string UnitCode { set; get; }

        

    }
}
