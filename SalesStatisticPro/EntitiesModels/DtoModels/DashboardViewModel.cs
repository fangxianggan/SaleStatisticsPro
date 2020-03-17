using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
   
    public partial class PanelGroupViewModel
    {
        /// <summary>
        /// 用户人数
        /// </summary>
        public int  UserInfoCount { set; get; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal SalesAmount { set; get; }

        /// <summary>
        /// 成本金额
        /// </summary>
        public decimal CostAmount { set; get; }

        /// <summary>
        /// 利润金额
        /// </summary>
        public decimal ProfitAmount { set; get; }
    }


    public partial class ChartsViewModel
    {
        public string X { set; get; }

        public string Y_1 { set; get; }

        public string Y_2 { set; get; }

        public string Y_3 { set; get; }
    }
}
