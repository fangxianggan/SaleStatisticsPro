using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Core.Util;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebApi.IBLL;

namespace WebApi.Controllers
{
    /// <summary>
    /// 统计报表
    /// </summary>
    /// 
    [RoutePrefix("dev-api/Statistics")]
    public class StatisticsController : ApiController
    {
        private readonly IProductBLL _productBLL;
        private readonly IUserInfoBLL _userInfoBLL;
        /// <summary>
        /// 
        /// </summary>
        public StatisticsController(IProductBLL productBLL, IUserInfoBLL userInfoBLL)
        {
            _productBLL = productBLL;
            _userInfoBLL = userInfoBLL;
        }



        /// <summary>
        /// 产品统计报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetProductViewModelPageList")]
        public HttpReponseModel<List<ProductStatisticsViewModel>> GetProductStatisticsViewModelPageList(QueryModel model)
        {
            return _productBLL.GetProductStatisticsViewModelPageList(model);
        }



        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("GetProductStatisticsViewModelExportExcel")]
        public HttpReponseModel<FileStreamViewModel> GetProductStatisticsViewModelExportExcel(QueryModel model)
        {
            return _productBLL.GetProductStatisticsViewModelExportExcel(model);
        }

        /// <summary>
        /// 首页 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPanelGroupViewModelData")]
        public HttpReponseModel<PanelGroupViewModel> GetPanelGroupViewModelData()
        {
            return _userInfoBLL.GetPanelGroupViewModelData();
        }

    }
}
