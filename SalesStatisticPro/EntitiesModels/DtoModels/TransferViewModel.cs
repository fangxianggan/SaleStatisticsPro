using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesModels.DtoModels
{

    public partial class SetTransferViewModel
    {
        public string[] SetRoleList { set; get; }

        public List<TransferViewModel> AllRoleList { set; get; }
    }
    public class TransferViewModel
    {
        public string Key { set; get; }

        public string Label { set; get; }

    }
}
