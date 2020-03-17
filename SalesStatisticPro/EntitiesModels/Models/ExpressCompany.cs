using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 快递公司
    /// </summary>
    /// 
    [Table("ExpressCompany")]
    public partial class ExpressCompany : BaseEntity
    {
        /// <summary>
        /// 快递公司编码
        /// </summary>
        /// 
        [MaxLength(64),Required,DisplayName("快递公司编码")]
        public string ExpressCompanyCode { set; get; }

        /// <summary>
        /// 快递公司名称
        /// </summary>
        [MaxLength(64),Required,DisplayName("快递公司名称")]
        public  string ExpressCompanyName { set;get;}

    }
}
