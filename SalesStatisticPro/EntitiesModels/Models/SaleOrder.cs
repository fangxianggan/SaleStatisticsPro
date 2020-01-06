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
    /// 销售订单
    /// </summary>
    /// 
    [Table("SaleOrder")]
    public partial class SaleOrder:BaseEntity
    {
       
        /// <summary>
        /// 销售订单号
        /// </summary>
        /// 
        [MaxLength(32)]
        public string SOrderNum { set; get; }

        /// <summary>
        /// 售出的时间
        /// </summary>
        public DateTime SOrderCreateTime { set; get; }

        /// <summary>
        /// 购买的用户 姓名
        /// </summary>
        ///  
        [MaxLength(32)]
        public string UserPurchase { set; get; }

        /// <summary>
        /// 购买用户的手机号
        /// </summary>
        /// 
        [MaxLength(32)]
        public string UserPurchasePhone { set; get; }

        /// <summary>
        /// 0 男 1 女
        /// </summary>
        public int UserPurchaseGender { set; get; }

        /// <summary>
        /// 购买用户的地址
        /// </summary>
        /// 
        [MaxLength(100)]
        public string UserPurchaseAddress { set; get; }

        /// <summary>
        /// 购买的总量
        /// </summary>
        public int AllPurchaseCount { set; get; }
        
        /// <summary>
        /// 总共消费的金额
        /// </summary>
        public decimal AllPurchaseAmount { set; get; }
    }
}
