using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 销售订单
    /// </summary>
    /// 
    [Table("SaleOrder")]
    public partial class SaleOrder : BaseEntity
    {

        /// <summary>
        /// 销售订单号
        /// </summary>
        /// 
        [MaxLength(64),Required,DisplayName("下单号")]
        public string SOrderNum { set; get; }

        /// <summary>
        /// 售出的时间
        /// </summary>
        /// 
        [Required,DisplayName("销售时间")]
        public DateTime SOrderCreateTime { set; get; }


        /// <summary>
        /// 购买用户的手机号
        /// </summary>
        /// 
        [MaxLength(32),Required, DisplayName("客户手机号")]
        public string PhoneNumber { set; get; }


        /// <summary>
        /// 总共消费的金额
        /// </summary>
        /// 
       [Required,DisplayName("销售订单总金额")]
        public decimal AllSaleAmount { set; get; }


        /// <summary>
        /// 消费的运费
        /// </summary>
        /// 
        [Required,DisplayName("销售运费总金额")]
        public decimal AllSaleFreightAmount { set; get; }

        /// <summary>
        /// 消费总的金额
        /// </summary>
        /// 
        [Required,DisplayName("消费总金额")]
        public decimal AllSaleSumAmount { set; get; }



        /// <summary>
        /// 总理赔金额
        /// </summary>
        /// 
        [Required, DisplayName("总理赔金额")]
        public decimal AllSaleSettlementAmount { set; get; }
        /// <summary>
        /// 销售订单状态
        /// </summary>
        /// 
        [MaxLength(8), Required, DisplayName("销售订单状态")]
        public string SaleOrderState { set; get; }
    }
}
