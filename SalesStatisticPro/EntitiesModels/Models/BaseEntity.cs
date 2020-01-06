using System;
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

        [Key]
        public int ID { get; set; }

        public DateTime CreateTime { get; set; }

        [MaxLength(32)]
        public string CreateUserCode { get; set; }

        public DateTime UpdateTime { set; get; }

        [MaxLength(32)]
        public string UpdateUserCode { get; set; }

        [MaxLength(200)]
        public string Remark { set; get; }
    }
}
