
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Entities.HttpResponse;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——Category
    /// </summary>
    public partial interface ICategoryBLL
    {

       

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetIsDeleteFlag(string code);




    }
}

