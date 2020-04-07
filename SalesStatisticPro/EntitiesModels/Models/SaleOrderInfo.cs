using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 销售订单信息
    /// </summary>
    /// 
    [Table("SaleOrderInfo")]
    public partial class SaleOrderInfo: BaseEntityPermission
    {

        /// <summary>
        /// 销售流水号
        /// </summary>
        /// 
        [MaxLength(64),Required,DisplayName("销售流水号")]
        public string SOrderNum { set; get; }

        /// <summary>
        /// 销售编号
        /// </summary>
        /// 
        [MaxLength(64),Required,DisplayName("销售编号")]
        public string SSOrderNum { set; get; }

       
        /// <summary>
        /// 销售产品的code
        /// </summary>
        /// 
        [MaxLength(64),Required,DisplayName("产品编号")]
        public string SProductCode { set; get; }

 
        /// <summary>
        /// 销售数量
        /// </summary>
        /// 
        [Required,DisplayName("销售数量")]
        public int SaleCount { set; get; }

        /// <summary>
        /// 购买价格
        /// </summary>
        /// 
         [Required,DisplayName("销售价格")]
        public decimal SalePrice { set; get; }

        /// <summary>
        /// 购买的金额
        /// </summary>
        /// 
        [Required,DisplayName("销售金额")]
        public decimal SaleAmount { set; get; }


        /// <summary>
        /// 快递公司
        /// </summary>
        /// 
        [MaxLength(64), DisplayName("快递公司")]
        public string ExpressCompanyCode { set; get; }

        /// <summary>
        /// 快递单号
        /// </summary>
        /// 
        [MaxLength(64), DisplayName("快递单号")]
        public string ExpressNumber { set; get; }

        /// <summary>
        /// 购买时运费
        /// </summary>
        /// 
        [Required,DisplayName("销售运费")]
        public decimal SaleFreightAmount { set; get; }


        /// <summary>
        /// 每个订单支付的金额  包括运费
        /// </summary>
        /// 
        [Required,DisplayName("销售总收入")]
        public decimal SaleSumAmount { set; get; }


        /// <summary>
        /// 理赔金额
        /// </summary>
        /// 
        [Required, DisplayName("理赔金额")]
        public decimal SaleSettlementAmount { set; get; }

        /// <summary>
        /// 销售订单状态
        /// </summary>
        /// 
        [MaxLength(8), Required, DisplayName("销售订单详情状态")]
        public string SaleOrderInfoState { set; get; }
    }
}
