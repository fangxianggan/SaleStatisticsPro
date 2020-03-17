
using EntitiesModels.Models;
using FXKJ.Infrastructure.Logic;
using WebApi.IBLL;
using FXKJ.Infrastructure.Entities.HttpResponse;
using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXKJ.Infrastructure.DataAccess;
using EntitiesModels.DtoModels;
using FXKJ.Infrastructure.Entities.Enum;
using FXKJ.Infrastructure.Core.Extensions;
using System.Linq;

namespace WebApi.BLL
{
    /// <summary>
    ///   业务访问——UserInfo
    /// </summary>
    public partial class UserInfoBLL : IUserInfoBLL
    {
        private readonly IEFRepository<UserInfo> _userInfoEFRepository;

        private readonly IEFRepository<Product> _productEFRepository;

        private readonly IEFRepository<SaleOrder> _saleOrderEFRepository;

        private readonly IEFRepository<PurchaseOrder> _purchaseOrderEFRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productEFRepository"></param>
        /// <param name="userInfoEFRepository"></param>
        /// <param name="logic"></param>
        public UserInfoBLL(IEFRepository<PurchaseOrder> purchaseOrderEFRepository,IEFRepository<SaleOrder> saleOrderEFRepository,IEFRepository<Product> productEFRepository, IEFRepository<UserInfo> userInfoEFRepository, ILogic<UserInfo> logic)
            : this(logic)
        {
            _userInfoEFRepository = userInfoEFRepository;
            _productEFRepository = productEFRepository;
            _saleOrderEFRepository = saleOrderEFRepository;
            _purchaseOrderEFRepository = purchaseOrderEFRepository;
        }

        /// <summary>
        /// 获取用户 selectlist
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<List<SelectViewModel>> GetSelectList()
        {
            HttpReponseModel<List<SelectViewModel>> httpReponse = new HttpReponseModel<List<SelectViewModel>>();
            List<SelectViewModel> selectList = AutoMapperExtension.MapToList<SelectViewModel>(_userInfoEFRepository.GetList(p => true));
            httpReponse.Data = selectList;
            return httpReponse;
        }


        /// <summary>
        /// 获取用户 list
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<List<UserInfo>> GetUserInfoList(string keyName)
        {
            HttpReponseModel<List<UserInfo>> httpReponse = new HttpReponseModel<List<UserInfo>>();
            List<UserInfo> selectList = new List<UserInfo>();
            if (!string.IsNullOrEmpty(keyName))
            {
                selectList= _userInfoEFRepository.GetList(p =>
p.PhoneNumber.Contains(keyName) ||
p.NickName.Contains(keyName) ||
p.ReceAddress.Contains(keyName) ||
p.UserName.Contains(keyName) ||
p.Remark.Contains(keyName)
);
            }
            else {
                selectList= _userInfoEFRepository.GetList(p => true);
            }

            httpReponse.Data = selectList;
            return httpReponse;
        }
        /// <summary>
        /// 判断可不可以删除
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<int> GetIsDeleteFlag(string phoneNumber)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            var list = _userInfoEFRepository.GetList(p => p.PhoneNumber == phoneNumber);
            if (list.Count > 0)
            {
                httpReponse.ResultSign = ResultSign.Warning;
                httpReponse.Data = list.Count;
                httpReponse.Message = "已购买了产品不可删除";
            }
            else
            {
                httpReponse.Data = 0;
                httpReponse.ResultSign = ResultSign.Successful;
                httpReponse.Message = "";
            }
            return httpReponse;
        }

        public HttpReponseModel<PanelGroupViewModel> GetPanelGroupViewModelData()
        {
            HttpReponseModel<PanelGroupViewModel> httpReponse = new HttpReponseModel<PanelGroupViewModel>();
            PanelGroupViewModel model = new PanelGroupViewModel();
            model.SalesAmount= _saleOrderEFRepository.GetList(p => true).Sum(p => p.AllSaleSumAmount);
            model.CostAmount = _purchaseOrderEFRepository.GetList(p => true).Sum(p => p.AllAmount);
            model.UserInfoCount = _userInfoEFRepository.GetList(p => true).Count();
            model.ProfitAmount = model.SalesAmount - model.CostAmount;
            httpReponse.Data = model;
            return httpReponse;
        }

    }
}

