
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
    ///   业务访问——Business
    /// </summary>
    public partial class BusinessBLL :IBusinessBLL
    {
        private readonly IEFRepository<Business> _businessEFRepository;

        private readonly IEFRepository<Product> _productEFRepository;

        private readonly IEFRepository<PurchaseOrder> _purchaseOrderEFRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productEFRepository"></param>
        /// <param name="businessEFRepository"></param>
        /// <param name="logic"></param>
        public BusinessBLL(IEFRepository<PurchaseOrder> purchaseOrderEFRepository,IEFRepository<Product> productEFRepository,IEFRepository<Business> businessEFRepository,ILogic<Business> logic)
            :this(logic)
        {
            _businessEFRepository = businessEFRepository;
            _productEFRepository = productEFRepository;
            _purchaseOrderEFRepository = purchaseOrderEFRepository;
        }

       

        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            HttpReponseModel<List<SelectViewModel>> httpReponse = new HttpReponseModel<List<SelectViewModel>>();
            httpReponse.Data = AutoMapperExtension.MapToList<SelectViewModel>(_businessEFRepository.GetList(p => true));
            return httpReponse;
        }


        /// <summary>
        /// 判断可不可以删除
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            var list = _purchaseOrderEFRepository.GetList(p => p.BusinessCode == code);
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

