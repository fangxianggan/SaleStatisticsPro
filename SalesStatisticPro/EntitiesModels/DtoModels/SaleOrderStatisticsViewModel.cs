using EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{

    /// <summary>
    /// 销售单统计dto
    /// </summary>
    [NotMapped]
    public partial class SaleOrderStatisticsViewModel
    {

      
        [DisplayName("订单号")]
        public string SOrderNum { set; get; }

        [DisplayName("昵称")]
        public string NickName { set; get; }

        [DisplayName("手机号")]
        public string PhoneNumber { set; get; }
       
        [DisplayName("销售时间")]
        public DateTime SOrderCreateTime { set; get; }

        [DisplayName("订单总支出")]
        public decimal AllSaleAmount { set; get; }

        
        [DisplayName("国内总运费")]
        public decimal AllSaleFreightAmount { set; get; }


        
        [DisplayName("总理赔金额")]
        public decimal AllSaleSettlementAmount { set; get; }

        
        [DisplayName("销售总收入")]
        public decimal AllSaleSumAmount { set; get; }


        [DisplayName("状态")]
        public string SaleOrderState { set; get; }
       
       
        [DisplayName("国内单号")]
        public string ExpressNumber { set; get; }


        [DisplayName("产品名称")]
        public string ProductName { set; get; }

        [DisplayName("产品简码")]
        public string SimpleCode { set; get; }

        [DisplayName("产品分类")]
        public string CategoryName { set; get; }

       
        [DisplayName("购买数量")]
        public int SaleCount { set; get; }

      
        [DisplayName("购买价格")]
        public decimal SalePrice { set; get; }

        
        [DisplayName("购买金额")]
        public decimal SaleAmount { set; get; }

       
        [DisplayName("国内运费")]
        public decimal SaleFreightAmount { set; get; }

        [DisplayName("销售金额")]
        public decimal SaleSumAmount { set; get; }

        /// <summary>
        /// 理赔金额
        /// </summary>
        /// 
        [DisplayName("理赔金额")]
        public decimal SaleSettlementAmount { set; get; }
        /// <summary>
        ///购买详情 状态
        /// </summary>
        /// 
        [DisplayName("购买订单详情状态")]
        public string SaleOrderInfoState { set; get; }

    }
}
