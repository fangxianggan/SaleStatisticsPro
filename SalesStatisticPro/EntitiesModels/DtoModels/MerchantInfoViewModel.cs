using EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{
    /// <summary>
    /// 商户信息
    /// </summary>
    public class MerchantInfoViewModel
    {
        public string Name { set; get; }

        public string Introduction { set; get; }

        public string Avatar { set; get; }

        public List<string> Roles { set; get; }
    }
}
