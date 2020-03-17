
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——Business
    /// </summary>
    public partial interface IBusinessBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList();

        HttpReponseModel<int> GetIsDeleteFlag(string code);

    }
}

