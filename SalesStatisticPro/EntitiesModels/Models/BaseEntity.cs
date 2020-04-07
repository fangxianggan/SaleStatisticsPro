using EntitiesModels.DtoModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace EntitiesModels.Models
{

    public interface IBaseEntity
    {
        int ID { get; }
    }

    [Serializable]
    public abstract class BaseEntity : IBaseEntity
    {
        AuthInfoViewModel currentMerchantInfo = HttpContext.Current.Request.RequestContext.RouteData.Values["CurrentMerchantInfo"] as AuthInfoViewModel;
        public BaseEntity()
        {
            if (ID == 0)
            {
                CreateTime = DateTime.Now;
                CreateUserCode = currentMerchantInfo==null?"":currentMerchantInfo.PhoneNumber;
            }
            UpdateTime = DateTime.Now;
            UpdateUserCode = currentMerchantInfo == null ? "" : currentMerchantInfo.PhoneNumber;
        }

        [Key, DisplayName("序号")]
        public int ID { get; set; }

        [Required, DisplayName("添加时间")]
        public DateTime CreateTime { get; set; }

        [MaxLength(32), DisplayName("创建人")]
        public string CreateUserCode { get; set; }


        [Required, DisplayName("修改时间")]
        public DateTime UpdateTime { set; get; }

        [MaxLength(32), DisplayName("修改人")]
        public string UpdateUserCode { get; set; }

        [MaxLength(200), DisplayName("备注")]
        public string Remark { set; get; }


    }
}
