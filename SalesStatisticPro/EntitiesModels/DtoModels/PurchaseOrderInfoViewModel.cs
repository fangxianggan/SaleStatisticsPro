using EntitiesModels.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.DtoModels
{
    [NotMapped]
    public partial class PurchaseOrderInfoViewModel : PurchaseOrderInfo
    {
        [DisplayName("产品编码")]
        public string ProductCode { set; get; }

        [DisplayName("产品名称")]
        public string ProductName { set; get; }

        [DisplayName("产品简码")]
        public string SimpleCode { set; get; }

        /// <summary>
        /// 国际快递
        /// </summary>
        /// 
        [ DisplayName("国际快递")]
        public string ExpressCompanyName { set; get; }
        /// <summary>
        /// 国际转运快递
        /// </summary>
        /// 
        [DisplayName("国际转运快递")]
        public string InternationExpressCompanyName { set; get; }
        /// <summary>
        /// 国内转运快递
        /// </summary>
        /// 
        [DisplayName("国内转运快递")]
        public string DomesticExpressCompanyName { set; get; }

    }
}
