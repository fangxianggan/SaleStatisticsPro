using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.Models
{
    public class BaseEntityGuid
    {

        public BaseEntityGuid()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
        [Key]
        [Required, DisplayName("ID")]
        public Guid ID { set; get; }


        [Required, DisplayName("添加时间")]
        public DateTime CreateTime { get; set; }


        [Required, DisplayName("修改时间")]
        public DateTime UpdateTime { set; get; }
    }
}
