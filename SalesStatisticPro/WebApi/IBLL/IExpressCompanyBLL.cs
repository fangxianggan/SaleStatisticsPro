﻿
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using EntitiesModels.HttpResponse;
using EntitiesModels.QueryModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApi.IBLL
{
    /// <summary>
    ///   业务层接口——ExpressCompany
    /// </summary>
    public partial interface IExpressCompanyBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpReponseModel<List<SelectViewModel>> GetSelectViewModelList();

        HttpReponseModel<int> GetIsDeleteFlag(string code);

    }
}

