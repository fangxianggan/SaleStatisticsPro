
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
    public partial interface IUserInfoBLL
    {
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetIsDeleteFlag(string phoneNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpReponseModel<List<SelectViewModel>> GetSelectList();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpReponseModel<List<UserInfo>> GetUserInfoList(string keyName);

        HttpReponseModel<PanelGroupViewModel> GetPanelGroupViewModelData();

    }
}

