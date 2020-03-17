using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 装运仓
    /// </summary>
    /// 
    [Table("TransferBin")]
    public partial class TransferBin:BaseEntity
    {
        /// <summary>
        /// 转运仓编码
        /// </summary>
        /// 
        [MaxLength(64),Required,DisplayName("转运仓编码")]
        public string TransferBinCode { set; get; }

        /// <summary>
        /// 转运仓名称
        /// </summary>
        [MaxLength(64),Required,DisplayName("转运仓名称")]
        public  string TransferBinName {set;get;}

    }
}
