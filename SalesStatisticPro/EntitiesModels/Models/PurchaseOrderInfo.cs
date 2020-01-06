using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 购买订单信息
    /// </summary>
    /// 
    [Table("PurchaseOrderInfo")]
    public class PurchaseOrderInfo:BaseEntity
    {
       

        /// <summary>
        /// 购买订单号
        /// </summary>
        /// 
        [MaxLength(32)]
        public string POrderNum { set; get; }

        /// <summary>
        /// 转运单号
        /// </summary>
        /// 
        [MaxLength(32)]
        public string TrackingNumber { set; get; }

        /// <summary>
        /// 购买产品的code
        /// </summary>
        /// 
        [MaxLength(32)]
        public string PProductCode { set; get; }

        /// <summary>
        /// 购买产品系列的code
        /// </summary>
        /// 
        [MaxLength(32)]
        public string  PProductTypeCode { set; get; }


        /// <summary>
        /// 购买数量
        /// </summary>
        public int PurchaseCount { set; get; }

        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal PurchasePrice { set; get; }

        /// <summary>
        /// 购买的金额
        /// </summary>
        public decimal PurchaseAmount { set; get; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockCount { set; get; }

        /// <summary>
        /// 盈利金额
        /// </summary>
        public decimal ProfitAmount { set; get; }

        

    }
}
