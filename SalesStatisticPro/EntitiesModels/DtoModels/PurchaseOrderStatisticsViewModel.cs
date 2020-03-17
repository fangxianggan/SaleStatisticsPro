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
    /// 进货单统计dto
    /// </summary>
    [NotMapped]
    public partial class PurchaseOrderStatisticsViewModel
    {

        /// <summary>
        /// 订单号
        /// </summary>
        /// 
        [DisplayName("订单号")]
        public string POrderNum { set; get; }


        /// <summary>
        /// 订单的标题
        /// </summary>
        /// 
        [DisplayName("订单标题")]
        public string POrderTitle { set; get; }
        /// <summary>
        /// 购买的时间
        /// </summary>
        /// 
        [DisplayName("购买时间")]
        public DateTime POrderCreateTime { set; get; }

        /// <summary>
        /// 美国单号
        /// </summary>
        [DisplayName("美国单号")]
        public string USANumber { set; get; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        /// 
        [DisplayName("供应商名称")]
        public string BusinessName { set; get; }

        /// <summary>
        /// 装运仓名称
        /// </summary>
        /// 
        [DisplayName("装运仓名称")]
        public string TransferBinName { set; get; }

        /// <summary>
        /// 订单总支付
        /// </summary>
        /// 
        [DisplayName("订单总支出")]
        public decimal AllPurchaseAmount { set; get; }

        /// <summary>
        /// 国际总运费
        /// </summary>
        /// 
        [DisplayName("国际总运费")]
        public decimal AllInternationFreightAmount { set; get; }


        /// <summary>
        /// 国内总运费
        /// </summary>
        /// 
        [DisplayName("国内总运费")]
        public decimal AllDomesticFreightAmount { set; get; }


        /// <summary>
        /// 总理赔金额
        /// </summary>
        /// 
        [DisplayName("总理赔金额")]
        public decimal AllPurchaseSettlementAmount { set; get; }

        /// <summary>
        /// 支付总支出
        /// </summary>
        /// 
        [DisplayName("支付总支出")]
        public decimal AllAmount { set; get; }


        [DisplayName("状态")]
        public string PurchaseOrderState { set; get; }
        /// <summary>
        /// 订单流水号
        /// </summary>
        /// 
        [DisplayName("订单流水号")]
        public string PPOrderNum { set; get; }

        /// <summary>
        /// 国际单号
        /// </summary>
        /// 
        [DisplayName("国际单号")]
        public string InternationNumber { set; get; }

        /// <summary>
        /// 国内单号
        /// </summary>
        /// 
        [DisplayName("国内单号")]
        public string DomesticNumber { set; get; }


        [DisplayName("产品名称")]
        public string ProductName { set; get; }

        [DisplayName("产品简码")]
        public string SimpleCode { set; get; }

        [DisplayName("产品分类")]
        public string CategoryName { set; get; }

        /// <summary>
        /// 购买数量
        /// </summary>
        [DisplayName("购买数量")]
        public int PurchaseCount { set; get; }

        /// <summary>
        /// 购买价格
        /// </summary>
        /// 
        [DisplayName("购买价格")]
        public decimal PurchasePrice { set; get; }

        /// <summary>
        /// 购买的金额
        /// </summary>
        /// 
        [DisplayName("购买金额")]
        public decimal PurchaseAmount { set; get; }

        /// <summary>
        /// 国际运费
        /// </summary>
        /// 
        [DisplayName("国际运费")]
        public decimal InternationFreightAmount { set; get; }

        /// <summary>
        /// 国内运费
        /// </summary>
        /// 
        [DisplayName("国内运费")]
        public decimal DomesticFreightAmount { set; get; }

        /// <summary>
        /// 支出金额（PurchaseAmount+InternationFreightAmount+DomesticFreightAmount-SettlementAmount）
        /// </summary>
        /// 
        [DisplayName("支出金额")]
        public decimal Amount { set; get; }

        /// <summary>
        /// 理赔金额
        /// </summary>
        /// 
        [DisplayName("理赔金额")]
        public decimal PurchaseSettlementAmount { set; get; }
        /// <summary>
        ///购买详情 状态
        /// </summary>
        /// 
        [DisplayName("购买订单详情状态")]
        public string PurchaseOrderInfoState { set; get; }

    }
}
