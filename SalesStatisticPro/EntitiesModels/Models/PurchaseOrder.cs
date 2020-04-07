using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 购买订单
    /// </summary>
    /// 
    [Table("PurchaseOrder")]
    public class PurchaseOrder : BaseEntityPermission
    {


        /// <summary>
        /// 购买订单号
        /// </summary>
        /// 
        [MaxLength(64), Required, DisplayName("购买订单号")]
        public string POrderNum { set; get; }


        /// <summary>
        /// 商户编码
        /// </summary>
        /// 
        [MaxLength(64), Required, DisplayName("商户编码")]
        public string BusinessCode { set; get; }

        /// <summary>
        /// 订单的标题
        /// </summary>
        /// 
        [MaxLength(100), Required, DisplayName("订单标题")]
        public string POrderTitle { set; get; }
        /// <summary>
        /// 购买的时间
        /// </summary>
        /// 
        [Required, DisplayName("购买时间")]
        public DateTime POrderCreateTime { set; get; }

        /// <summary>
        /// 美国单号
        /// </summary>
        [MaxLength(32), DisplayName("美国单号")]
        public string USANumber { set; get; }

        /// <summary>
        /// 转运仓code
        /// </summary>
        /// 
        [MaxLength(64), Required, DisplayName("转运仓编码")]
        public string TransferBinCode { set; get; }

        /// <summary>
        /// 订单总支付
        /// </summary>
        /// 
        [Required, DisplayName("订单总支出")]
        public decimal AllPurchaseAmount { set; get; }

        /// <summary>
        /// 国际总运费
        /// </summary>
        /// 
        [Required, DisplayName("国际总运费")]
        public decimal AllInternationFreightAmount { set; get; }


        /// <summary>
        /// 国内总运费
        /// </summary>
        /// 
        [Required, DisplayName("国内总运费")]
        public decimal AllDomesticFreightAmount { set; get; }


        /// <summary>
        /// 总理赔金额
        /// </summary>
        /// 
        [Required, DisplayName("总理赔金额")]
        public decimal AllPurchaseSettlementAmount { set; get; }

        /// <summary>
        /// 支付总支出
        /// </summary>
        /// 
        [Required, DisplayName("支付总支出")]
        public decimal AllAmount { set; get; }


        /// <summary>
        ///购买 状态
        /// </summary>
        /// 
        [MaxLength(8), Required, DisplayName("购买订单状态")]
        public string PurchaseOrderState { set; get; }

    }
}
