using EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    [NotMapped]
    public partial class SaleOrderInfoViewModel : SaleOrderInfo
    {

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { set; get; }

        
        /// <summary>
        /// 产品简码
        /// </summary>
        public string SimpleCode { set; get; }


    }
}
