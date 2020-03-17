using EntitiesModels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    /// <summary>
    /// dto 销售订单model
    /// </summary>
    /// 
    [NotMapped]
    public partial class SaleOrderViewModel : SaleOrder
    {


        public virtual List<SaleOrderInfoViewModel> SaleOrderInfoViewModels { set; get; }
    }
}
