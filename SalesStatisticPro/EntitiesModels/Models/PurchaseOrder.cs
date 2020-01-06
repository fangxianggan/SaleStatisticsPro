using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 购买订单
    /// </summary>
    /// 
    [Table("PurchaseOrder")]
    public class PurchaseOrder:BaseEntity
    {
       

        /// <summary>
        /// 购买订单号
        /// </summary>
        /// 
        [MaxLength(32)]
        public string POrderNum { set; get; }


        /// <summary>
        /// 订单的标题
        /// </summary>
        /// 
        [MaxLength(100)]
        public string POrderTitle { set; get; }
        /// <summary>
        /// 购买的时间
        /// </summary>
         public DateTime POrderCreateTime { set; get; }

        /// <summary>
        /// 购买的那个商家
        /// </summary>
        /// 
        [MaxLength(32)]
        public string BusinessCode { set; get; }

        /// <summary>
        /// 美国单号
        /// </summary>
        [MaxLength(32)]
        public string USANumber { set; get; }

        /// <summary>
        /// 转运仓code
        /// </summary>
        /// 
        [MaxLength(32)]
        public string  TransferBinCode { set; get; }

    }
}
