using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 销售订单信息
    /// </summary>
    /// 
    [Table("SaleOrderInfo")]
    public partial class SaleOrderInfo:BaseEntity
    {
        

        /// <summary>
        /// 销售订单Id
        /// </summary>
        /// 
        [MaxLength(32)]
        public string SOrderNum { set; get; }

        /// <summary>
        /// 进货单号
        /// </summary>
        /// 
        [MaxLength(32)]
        public string POrderNum { set; get; }

        /// <summary>
        /// 卖出的商家code
        /// </summary>
        /// 
        [MaxLength(32)]
        public string  SBusinessCode { set; get; }
        /// <summary>
        /// 销售产品的code
        /// </summary>
        /// 
        [MaxLength(32)]
        public string SProductCode { set; get; }

        /// <summary>
        /// 销售产品系列的code
        /// </summary>
        /// 
        [MaxLength(32)]
        public string SProductTypeCode { set; get; }


        /// <summary>
        /// 销售数量
        /// </summary>
        public int SaleCount { set; get; }

        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal SalePrice { set; get; }

        /// <summary>
        /// 购买的金额
        /// </summary>
        public decimal SaleAmount { set; get; }

        


    }
}
