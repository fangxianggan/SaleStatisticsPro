using EntitiesModels.DtoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EntitiesModels.Models
{
    public class BaseEntityPermission : BaseEntity
    {
        AuthInfoViewModel currentMerchantInfo = HttpContext.Current.Request.RequestContext.RouteData.Values["CurrentMerchantInfo"] as AuthInfoViewModel;

        public BaseEntityPermission()
        {
            P_MerchantNo = currentMerchantInfo==null?"": currentMerchantInfo.MerchantNo;
        }
        [DisplayName("商户号")]
        public string P_MerchantNo { set; get; }
    }
}
