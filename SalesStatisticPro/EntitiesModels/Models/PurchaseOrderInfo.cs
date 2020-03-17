using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 购买订单信息
    /// </summary>
    /// 
    [Table("PurchaseOrderInfo")]
    public class PurchaseOrderInfo : BaseEntity
    {

        /// <summary>
        /// 购买订单号
        /// </summary>
        /// 
        [MaxLength(64), Required, DisplayName("订单号")]
        public string POrderNum { set; get; }


        /// <summary>
        /// 订单流水号
        /// </summary>
        /// 
        [MaxLength(64), Required, DisplayName("订单流水号")]
        public string PPOrderNum { set; get; }


        /// <summary>
        /// 国际快递
        /// </summary>
        /// 
        [MaxLength(64), DisplayName("国际快递")]
        public string ExpressCompanyCode { set; get; }

        /// <summary>
        /// 国际单号
        /// </summary>
        /// 
        [MaxLength(64), DisplayName("国际单号")]
        public string ExpressNumber { set; get; }

        /// <summary>
        /// 国际运费
        /// </summary>
        /// 
        [Required,DisplayName("国际运费")]
        public decimal ExpressFreightAmount { set; get; }


        /// <summary>
        /// 国际转运快递
        /// </summary>
        /// 
        [MaxLength(64), DisplayName("国际转运快递")]
        public string InternationExpressCompanyCode { set; get; }

        /// <summary>
        /// 国际转运单号
        /// </summary>
        /// 
        [MaxLength(64), DisplayName("国际转运单号")]
        public string InternationNumber { set; get; }

        /// <summary>
        /// 国际转运运费
        /// </summary>
        /// 
        [Required, DisplayName("国际转运运费")]
        public decimal InternationFreightAmount { set; get; }
        /// <summary>
        /// 国际快递
        /// </summary>
        [MaxLength(64), DisplayName("国内转运快递")]
        public string DomesticExpressCompanyCode { set; get; }
        /// <summary>
        /// 国内单号
        /// </summary>
        /// 
        [MaxLength(64), DisplayName("国内转运单号")]
        public string DomesticNumber { set; get; }

        /// <summary>
        /// 国内运费
        /// </summary>
        /// 
        [Required, DisplayName("国内运费")]
        public decimal DomesticFreightAmount { set; get; }

        /// <summary>
        /// 购买产品的code
        /// </summary>
        /// 
        [MaxLength(64), Required, DisplayName("产品编号")]
        public string PProductCode { set; get; }

        /// <summary>
        /// 购买数量
        /// </summary>
        [Required, DisplayName("购买数量")]
        public int PurchaseCount { set; get; }

        /// <summary>
        /// 购买价格
        /// </summary>
        /// 
        [Required, DisplayName("购买价格")]
        public decimal PurchasePrice { set; get; }

        /// <summary>
        /// 购买的金额
        /// </summary>
        /// 
        [Required, DisplayName("购买金额")]
        public decimal PurchaseAmount { set; get; }

        /// <summary>
        /// 支出金额（PurchaseAmount+InternationFreightAmount+DomesticFreightAmount-SettlementAmount）
        /// </summary>
        /// 
        [Required, DisplayName("支出金额")]
        public decimal Amount { set; get; }

        /// <summary>
        /// 理赔金额
        /// </summary>
        /// 
        [Required, DisplayName("理赔金额")]
        public decimal PurchaseSettlementAmount { set; get; }
        /// <summary>
        ///购买详情 状态
        /// </summary>
        /// 
        [MaxLength(8), Required, DisplayName("购买订单详情状态")]
        public string PurchaseOrderInfoState { set; get; }

    }
}
