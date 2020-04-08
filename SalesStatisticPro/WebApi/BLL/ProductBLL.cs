
using EntitiesModels.Models;
using FXKJ.Infrastructure.Logic;
using WebApi.IBLL;
using EntitiesModels.DtoModels;
using EntitiesModels.HttpResponse;
using FXKJ.Infrastructure.DataAccess;
using System.Collections.Generic;
using System;
using EntitiesModels.Enum;
using EntitiesModels.QueryModels;
using WebApi.IRepository;
using System.Data;
using FXKJ.Infrastructure.Core.Helper;
using System.IO;
using System.Web;
using System.Net.Http;
using FXKJ.Infrastructure.Core.Util;
using System.Net;
using System.Net.Http.Headers;

namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——Product
    /// </summary>
    public partial class ProductBLL : IProductBLL
    {
        /// <summary>
        /// 构造函数依赖注入
        /// </summary>
        private readonly IEFRepository<Product> _productEFRepository;

        private readonly IEFRepository<SaleOrderInfo> _saleOrderInfoEFRepository;

        private readonly IEFRepository<PurchaseOrderInfo> _purchaseOrderInfoEFRepository;

        private readonly IProductRepository _productRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="purchaseOrderInfoEFRepository"></param>
        /// <param name="saleOrderInfoEFRepository"></param>
        /// <param name="productEFRepository"></param>
        /// <param name="logic"></param>
        public ProductBLL(IProductRepository productRepository,IEFRepository<PurchaseOrderInfo> purchaseOrderInfoEFRepository, IEFRepository<SaleOrderInfo> saleOrderInfoEFRepository, IEFRepository<Product> productEFRepository, ILogic<Product> logic)
            : this(logic)
        {
            _productEFRepository = productEFRepository;
            _saleOrderInfoEFRepository = saleOrderInfoEFRepository;
            _purchaseOrderInfoEFRepository = purchaseOrderInfoEFRepository;
            _productRepository = productRepository;

        }

        

        /// <summary>
        /// 验证输入的简码是不是已经存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="simpleCode"></param>
        /// <returns></returns>
        public HttpReponseModel<int> GetIsExist(int id, string simpleCode)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            var list = _productEFRepository.GetList(p => p.SimpleCode == simpleCode);
            int count = list.Count;
            if (id > 0)
            {
                var ent = list.Find(p => p.ID == id);
                if (ent != null)
                {
                    count = 0;
                    httpReponse.Message = "";
                    httpReponse.ResultSign = ResultSign.Successful;
                }
                else
                {
                    httpReponse.Message = "已重复";
                    httpReponse.ResultSign = ResultSign.Warning;
                    count = list.Count;
                }
            }
            else {
                if (count > 0)
                {
                    httpReponse.Message = "已重复";
                    httpReponse.ResultSign = ResultSign.Warning;
                }
            }
            httpReponse.Data = count;
            return httpReponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public HttpReponseModel<List<Product>> GetSelectProductDetial(string keyName)
        {
            HttpReponseModel<List<Product>> httpReponse = new HttpReponseModel<List<Product>>();
            List<Product> list = new List<Product>();
            if (!String.IsNullOrEmpty(keyName))
            {
                list = _productEFRepository.GetList(p =>
             p.SimpleCode.Contains(keyName)
           || p.ProductName.Contains(keyName)
           || p.Remark.Contains(keyName)
            );
            }
            else
            {
                list = _productEFRepository.GetList(p =>
                            true
                            );
            }
            httpReponse.Data = list;
            return httpReponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public HttpReponseModel<Product> GetProduct(string code)
        {
            HttpReponseModel<Product> httpReponse = new HttpReponseModel<Product>();

            httpReponse.Data = _productEFRepository.GetEntity(p => p.ProductCode == code);
            return httpReponse;
        }

        /// <summary>
        /// 判断可不可以删除
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            var soilist = _saleOrderInfoEFRepository.GetList(p => p.SProductCode == code);
            var poiList = _purchaseOrderInfoEFRepository.GetList(p => p.PProductCode == code);
            if (soilist.Count > 0 && poiList.Count > 0)
            {
                httpReponse.ResultSign = ResultSign.Warning;
                httpReponse.Data = poiList.Count;
                httpReponse.Message = "产品已销售不可删除";
            }
            else if (soilist.Count > 0 && poiList.Count == 0)
            {
                httpReponse.ResultSign = ResultSign.Warning;
                httpReponse.Data = soilist
.Count;
                httpReponse.Message = "产品已入库不可删除";
            }
            else if (soilist.Count == 0 && poiList.Count > 0)
            {

                httpReponse.ResultSign = ResultSign.Warning;
                httpReponse.Data = poiList.Count;
                httpReponse.Message = "产品已销售不可删除";

            }
            else
            {
                httpReponse.Data = 0;
                httpReponse.ResultSign = ResultSign.Successful;
                httpReponse.Message = "";
            }
            return httpReponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HttpReponseModel<List<ProductViewModel>> GetProductViewModelPageList(QueryModel model)
        {
            HttpReponseModel<List<ProductViewModel>> httpReponse = new HttpReponseModel<List<ProductViewModel>>();
            httpReponse.Data= _productRepository.GetProductViewModelPageList(model) as List<ProductViewModel>;
            httpReponse.Total = model.Total;
            return httpReponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HttpReponseModel<List<ProductStatisticsViewModel>> GetProductStatisticsViewModelPageList(QueryModel model)
        {
            HttpReponseModel<List<ProductStatisticsViewModel>> httpReponse = new HttpReponseModel<List<ProductStatisticsViewModel>>();
            httpReponse.Data = _productRepository.GetProductStatisticsViewModelPageList(model) as List<ProductStatisticsViewModel>;
            httpReponse.Total = model.Total;
            return httpReponse;
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HttpReponseModel<FileStreamViewModel> GetProductStatisticsViewModelExportExcel(QueryModel model)
        {
            HttpReponseModel<FileStreamViewModel> httpReponse = new HttpReponseModel<FileStreamViewModel>(); 
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            DataTable dt = _productRepository.GetProductStatisticsViewModelDataTable(model);
            dt.TableName = "产品统计报表";
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dt);

            //列名称显示中文
            ProductStatisticsViewModel t = new ProductStatisticsViewModel();
            var dic=  DisplayNameUtil.DisplayNameModel(t);
            ExportHelper.SetColumnNameDisplayName(true,dic);


            var stream = ExportHelper.ExportData(dataSet);
            var  fileInfo=Convert.ToBase64String(stream.ToArray());
            FileStreamViewModel fileStream = new FileStreamViewModel();
            fileStream.Content = JsonUtil.JsonSerialize(fileInfo);
            fileStream.FileName = fileName;
            httpReponse.Data = fileStream;
            return httpReponse;
        }
    }
}

