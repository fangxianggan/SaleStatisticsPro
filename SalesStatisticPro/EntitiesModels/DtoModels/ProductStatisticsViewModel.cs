using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{

    /// <summary>
    /// 产品统计报表
    /// </summary>
    public partial class ProductStatisticsViewModel
    {
        /// <summary>
        /// 作为默认的排序
        /// </summary>
        /// 
        [DisplayName("ID")]
        public int ID { set; get; }
        /// <summary>
        /// 产品简码
        /// </summary>
        /// 
        [DisplayName("产品简码")]
        public string SimpleCode { set; get; }

        /// <summary>
        /// 产品编码
        /// </summary>
        /// 
        [DisplayName("产品编码")]
        public string ProductCode { set; get; }

        /// <summary>
        /// 产品名称
        /// </summary>
        ///  
        [DisplayName("产品名称")]
        public string ProductName { set; get; }

        /// <summary>
        /// 进货数量
        /// </summary>
        /// 
        [DisplayName("进货数量")]
        public int PurchaseCount { set; get; }

        /// <summary>
        /// 货钱
        /// </summary>
        /// 
        [DisplayName("进货金额")]
        public decimal PurchaseAmount { set; get; }

        /// <summary>
        /// 总运费
        /// </summary>
        /// 
        [DisplayName("进货运费")]
        public decimal PurchaseFreightAmount { set; get; }

        /// <summary>
        /// 进货理赔金额
        /// </summary>
        /// 
        [DisplayName("进货理赔金额")]
        public decimal PurchaseSettlementAmount { set; get; }

        /// <summary>
        /// 总的进货金额（货钱加运费）
        /// </summary>
        /// 
        [DisplayName("进货总金额")]
        public decimal AllPurchaseAmount { set; get; }



        /// <summary>
        /// 购买数量
        /// </summary>
        /// 
        [DisplayName("销售数量")]
        public int SaleCount { set; get; }

        /// <summary>
        /// 销售金额
        /// </summary>
        /// 
        [DisplayName("销售金额")]
        public decimal SaleAmount { set; get; }

        /// <summary>
        /// 销售的运费
        /// </summary>
        /// 
        [DisplayName("销售运费")]
        public decimal SaleFreightAmount { set; get; }

        /// <summary>
        /// 销售理赔金额
        /// </summary>
        /// 
        [DisplayName("销售理赔金额")]
        public decimal SaleSettlementAmount { set; get; }

        /// <summary>
        /// 销售总金额
        /// </summary>
        /// 
        [DisplayName("销售总金额")]
        public decimal AllSaleAmount { set; get; }


        /// <summary>
        /// 利润金额
        /// </summary>
        [DisplayName("利润金额")]
        public decimal ProfitAmount { set; get; }

        /// <summary>
        /// 库存量
        /// </summary>
        /// 
        [DisplayName("库存量")]
        public int Stock { set; get; }
    }
}
