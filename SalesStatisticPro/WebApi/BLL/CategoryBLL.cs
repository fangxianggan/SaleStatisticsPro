﻿
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
    ///   业务访问——Category
    /// </summary>
    public partial class CategoryBLL :ICategoryBLL
    {
        private readonly IEFRepository<Category> _categoryEFRepository;

        private readonly IEFRepository<Product> _productEFRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productEFRepository"></param>
        /// <param name="transferBinEFRepository"></param>
        /// <param name="logic"></param>
        public CategoryBLL(IEFRepository<Product> productEFRepository, IEFRepository<Category> transferBinEFRepository,ILogic<Category> logic)
            :this(logic)
        {
            _categoryEFRepository = transferBinEFRepository;
            _productEFRepository = productEFRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList()
        {
            HttpReponseModel<List<SelectViewModel>> httpReponse = new HttpReponseModel<List<SelectViewModel>>();
            httpReponse.Data = AutoMapperExtension.MapToList<SelectViewModel>(_categoryEFRepository.GetList(p => true));
            return httpReponse;
        }


        /// <summary>
        /// 判断可不可以删除
        /// </summary>
        /// <returns></returns>
        public HttpReponseModel<int> GetIsDeleteFlag(string code)
        {
            HttpReponseModel<int> httpReponse = new HttpReponseModel<int>();
            var list = _productEFRepository.GetList(p => p.CategoryCode == code);
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

