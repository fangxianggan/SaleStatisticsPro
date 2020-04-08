
using EntitiesModels.Models;
using FXKJ.Infrastructure.Logic;
using WebApi.IBLL;
using EntitiesModels.HttpResponse;
using System.Collections.Generic;
using FXKJ.Infrastructure.DataAccess;
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Core.Extensions;
using EntitiesModels.Enum;

namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——TransferBin
    /// </summary>
    public partial class TransferBinBLL :ITransferBinBLL
    {
        private readonly IEFRepository<TransferBin> _transferBinEFRepository;

        private readonly IEFRepository<PurchaseOrder> _purchaseOrderEFRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseOrderEFRepository"></param>
        /// <param name="transferBinEFRepository"></param>
        /// <param name="logic"></param>
        public TransferBinBLL(IEFRepository<PurchaseOrder> purchaseOrderEFRepository ,IEFRepository<TransferBin> transferBinEFRepository,ILogic<TransferBin> logic)
            :this(logic)
        {
            _transferBinEFRepository = transferBinEFRepository;
            _purchaseOrderEFRepository = purchaseOrderEFRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            HttpReponseModel<List<SelectViewModel>> httpReponse = new HttpReponseModel<List<SelectViewModel>>();
            httpReponse.Data = AutoMapperExtension.MapToList<SelectViewModel>(_transferBinEFRepository.GetList(p => true));
            return httpReponse;
        }


        /// <summary>
        /// 判断可不可以删除
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            var list = _purchaseOrderEFRepository.GetList(p => p.TransferBinCode == code);
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

