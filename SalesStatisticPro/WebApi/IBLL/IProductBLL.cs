
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——Product
    /// </summary>
    public partial interface IProductBLL
    {

       

        /// <summary>
        /// 判断输入的简码存不存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="simpleCode"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetIsExist(int id,string simpleCode);


        /// <summary>
        /// 获取自定义的产品信息
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        HttpReponseModel<List<Product>> GetSelectProductDetial(string keyName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        HttpReponseModel<Product> GetProduct(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        HttpReponseModel<int> GetIsDeleteFlag(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HttpReponseModel<List<ProductViewModel>> GetProductViewModelPageList(QueryModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HttpReponseModel<List<ProductStatisticsViewModel>> GetProductStatisticsViewModelPageList(QueryModel model);

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        HttpReponseModel<FileStreamViewModel> GetProductStatisticsViewModelExportExcel(QueryModel model);

    }
}

