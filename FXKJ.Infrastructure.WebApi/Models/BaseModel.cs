using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXKJ.Infrastructure.WebApi.Models
{
   public partial class BaseModel
    {
        public BaseModel()
        {
            code = 20000;
            name = "Admin";
            introduction = "dasasas";
            avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif";

        }
        public int code { get; }

        public string name { get; }

        public string introduction { get; }

        public string avatar { get; }
    }
}
