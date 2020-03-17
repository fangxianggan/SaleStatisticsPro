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
    [NotMapped]
    public partial class PurchaseOrderViewModel:PurchaseOrder
    {
       
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


        public virtual List<PurchaseOrderInfoViewModel>   PurchaseOrderInfoViewModels { set; get; }
    }
}
