using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 商家
    /// </summary>
    [Table("Business")]
    public partial class Business: BaseEntity
    {
        /// <summary>
        /// 商户编码code
        /// </summary>
        [MaxLength(64),Required,DisplayName("商户编码")]
        public string BusinessCode { set; get; }


        /// <summary>
        /// 商户名称
        /// </summary>
        [MaxLength(50),Required,DisplayName("商户名称")]
        public string BusinessName { set; get; }


    }
}
