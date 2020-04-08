
using EntitiesModels.Models;
using FXKJ.Infrastructure.Logic;
using WebApi.IBLL;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXKJ.Infrastructure.DataAccess;
using EntitiesModels.DtoModels;
using EntitiesModels.Enum;
using FXKJ.Infrastructure.Core.Extensions;

namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——ExpressCompany
    /// </summary>
    public partial class ExpressCompanyBLL :IExpressCompanyBLL
    {
        private readonly IEFRepository<ExpressCompany> _expressCompanyEFRepository;

        private readonly IEFRepository<Product> _productEFRepository;

        private readonly IEFRepository<PurchaseOrderInfo> _purchaseOrderInfoEFRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productEFRepository"></param>
        /// <param name="expressCompanyEFRepository"></param>
        /// <param name="logic"></param>
        public ExpressCompanyBLL(IEFRepository<PurchaseOrderInfo> purchaseOrderInfoEFRepository,IEFRepository<Product> productEFRepository,IEFRepository<ExpressCompany> expressCompanyEFRepository,ILogic<ExpressCompany> logic)
            :this(logic)
        {
            _expressCompanyEFRepository = expressCompanyEFRepository;
            _productEFRepository = productEFRepository;
            _purchaseOrderInfoEFRepository = purchaseOrderInfoEFRepository;
        }

       

        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            HttpReponseModel<List<SelectViewModel>> httpReponse = new HttpReponseModel<List<SelectViewModel>>();
            httpReponse.Data = AutoMapperExtension.MapToList<SelectViewModel>(_expressCompanyEFRepository.GetList(p => true));
            return httpReponse;
        }


        /// <summary>
        /// 判断可不可以删除
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            var list = _purchaseOrderInfoEFRepository.GetList(p => p.ExpressCompanyCode == code||p.DomesticExpressCompanyCode==code||p.InternationExpressCompanyCode==code);
            if (list.Count > 0)
            {
                httpReponse.ResultSign = ResultSign.Warning;
                httpReponse.Data = list.Count;
                httpReponse.Message = "先解除产品关联再删除";
            }
            else
            {
                httpReponse.Data = 0;
                httpReponse.ResultSign = ResultSign.Successful;
                httpReponse.Message = "";
            }
            return httpReponse;
        }
    }
}

