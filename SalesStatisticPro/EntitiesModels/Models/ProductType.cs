using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 产品型号
    /// </summary>
    [Table("ProductType")]
    public partial class ProductType:BaseEntity
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        /// 
        [MaxLength(32)]
        public string  ProdoctCode { set; get; }

        /// <summary>
        /// 型号编码
        /// </summary>
        /// 
        [MaxLength(32)]
        public string ProductTypeCode { set; get; }


        /// <summary>
        /// 型号名称
        /// </summary>
        [MaxLength(50)]
        public string ProdoctTypeName { set; get; }

      
    }
}
