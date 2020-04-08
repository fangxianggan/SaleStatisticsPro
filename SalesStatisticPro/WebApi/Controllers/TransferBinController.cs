using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using FXKJ.Infrastructure.Logic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public partial class TransferBinController : ApiController
    {
      
        /// <summary>
        /// 
        /// </summary>
        public TransferBinController()
        {
            
        }

        /// <summary>
        /// 获取转运仓select数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSelectViewModelList")]
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            return  _transferBinBLL.GetSelectViewModelList();
        }


        /// <summary>
        /// 判断该品牌能不能删除
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetIsDeleteFlag")]
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            return _transferBinBLL.GetIsDeleteFlag(code);
        }

    }
}
