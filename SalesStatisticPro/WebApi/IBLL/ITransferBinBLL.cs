
using EntitiesModels.DtoModels;
using EntitiesModels.HttpResponse;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——TransferBin
    /// </summary>
    public partial interface ITransferBinBLL
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

