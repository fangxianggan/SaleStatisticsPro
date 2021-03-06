﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesModels.Models
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("Menu")]
    public partial class Menu:BaseEntity
    {
        [DisplayName("菜单编码"), MaxLength(32)]
        public string MenuCode { set; get; }

        [DisplayName("菜单名称"), MaxLength(32)]
        public string MenuName { set; get; }

        [DisplayName("路径"), MaxLength(32)]
        public string Path { set; get; }

        [DisplayName("icon"), MaxLength(32)]
        public string Icon { set; get; }
        [DisplayName("父节点")]
        public int ParentId { set; get; }

        [DisplayName("是否掩藏")]
        public bool Hidden { set; get; }

        [DisplayName("排序")]
        public int Sort { set; get; }

        [DisplayName("无子节点")]
        public bool NoChildren { set; get; }
    }
}
