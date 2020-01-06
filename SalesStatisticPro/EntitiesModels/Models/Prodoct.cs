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
    /// 产品
    /// </summary>
    [Table("Prodoct")]
    public partial class Prodoct:BaseEntity
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        [MaxLength(32)]
        public string ProductCode { set; get; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [MaxLength(50)]
        public string ProdoctName { set; get; }

       
    }
}
