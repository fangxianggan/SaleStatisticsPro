using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    /// <summary>
    /// 产品库存viewmodel
    /// </summary>
   public partial class ProductStockViewModel:ProductViewModel
    {

      /// <summary>
      /// 库存
      /// </summary>
        public int ProductStock { set; get; }

    }
}
