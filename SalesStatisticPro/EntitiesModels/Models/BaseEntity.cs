﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EntitiesModels.Models
{

    public interface IBaseEntity
    {
        int ID { get; }
    }

    [Serializable]
    public abstract class BaseEntity : IBaseEntity
    {
        
        public BaseEntity()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        [Key,DisplayName("序号")]
        public int ID { get; set; }

        [Required,DisplayName("添加时间")]
        public DateTime CreateTime { get; set; }

        [MaxLength(32),DisplayName("创建人")]
        public string CreateUserCode { get; set; }


        [Required,DisplayName("修改时间")]
        public DateTime UpdateTime{set;get;}

        [MaxLength(32),DisplayName("修改人")]
        public string UpdateUserCode { get; set; }

        [MaxLength(200),DisplayName("备注")]
        public string Remark { set; get; }

        [DisplayName("商户ID")]
        public Guid? MerchantId { set; get; }
    }
}
