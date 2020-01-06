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
        [MaxLength(32)]
        public string TransferBinCode { set; get; }

        /// <summary>
        /// 转运仓名称
        /// </summary>
        [MaxLength(64)]
        public  string TransferBinName {set;get;}

    }
}
