using EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    public partial class RequestSaleOrderModel
    {
        public SaleOrder saleOrder { set; get; }

        public List<SaleOrderInfo> saleOrderInfos { set; get; }
    }
}
