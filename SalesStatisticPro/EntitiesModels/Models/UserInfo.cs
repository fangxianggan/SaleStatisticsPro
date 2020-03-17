using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{

    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("UserInfo")]
    public partial class UserInfo : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(64),DisplayName("客户名称")]
        public string UserName { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(64),DisplayName("客户昵称")]
        public string NickName { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public int Gender { set; get; }


        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(32),Required,DisplayName("手机号")]
        public string PhoneNumber { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(200),DisplayName("寄货地址")]
        public string ReceAddress { set; get; }

    }
}
