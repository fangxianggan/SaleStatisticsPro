using AutoMapper;
using EntitiesModels.DtoModels;
using EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.App_Start
{
    /// <summary>
    /// autoMapper 配置
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// 注册automapper配置
        /// </summary>
        public static void Register()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<BaseProFileEntity>();
            });
        }
    }


    /// <summary>
    /// 自动映射的配置
    /// </summary>
    public class BaseProFileEntity : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseProFileEntity()
        {
            //销售订单模型
            base.CreateMap<SaleOrder, SaleOrderViewModel>();
            base.CreateMap<SaleOrderViewModel, SaleOrder>();

            //销售订单 详情模型
            base.CreateMap<SaleOrderInfo, SaleOrderInfoViewModel>();
            base.CreateMap<SaleOrderInfoViewModel, SaleOrderInfo>();


            //进货订单模型
            base.CreateMap<PurchaseOrder, PurchaseOrderViewModel>();
            base.CreateMap<PurchaseOrderViewModel, PurchaseOrder>();

            //进货订单 详情模型
            base.CreateMap<PurchaseOrderInfo, PurchaseOrderInfoViewModel>();
            base.CreateMap<PurchaseOrderInfoViewModel, PurchaseOrderInfo>();

            // 模型 产品
            base.CreateMap<UserInfo, SelectViewModel>()
                .ForMember(dest => dest.Key, options => options.MapFrom(src => src.ID))
                 .ForMember(dest => dest.Label, options => options.MapFrom(src => src.NickName))
                 .ForMember(dest => dest.Value, options => options.MapFrom(src => src.PhoneNumber));


            // 模型 转运仓
            base.CreateMap<TransferBin, SelectViewModel>()
                .ForMember(dest => dest.Key, options => options.MapFrom(src => src.ID))
                 .ForMember(dest => dest.Label, options => options.MapFrom(src => src.TransferBinName))
                 .ForMember(dest => dest.Value, options => options.MapFrom(src => src.TransferBinCode));

            // 模型 分类
            base.CreateMap<Category, SelectViewModel>()
                .ForMember(dest => dest.Key, options => options.MapFrom(src => src.ID))
                 .ForMember(dest => dest.Label, options => options.MapFrom(src => src.CategoryName))
                 .ForMember(dest => dest.Value, options => options.MapFrom(src => src.CategoryCode));


            // 模型 供应商
            base.CreateMap<Business, SelectViewModel>()
                .ForMember(dest => dest.Key, options => options.MapFrom(src => src.ID))
                 .ForMember(dest => dest.Label, options => options.MapFrom(src => src.BusinessName))
                 .ForMember(dest => dest.Value, options => options.MapFrom(src => src.BusinessCode));

            // 模型 快递公司
            base.CreateMap<ExpressCompany, SelectViewModel>()
                .ForMember(dest => dest.Key, options => options.MapFrom(src => src.ID))
                 .ForMember(dest => dest.Label, options => options.MapFrom(src => src.ExpressCompanyName))
                 .ForMember(dest => dest.Value, options => options.MapFrom(src => src.ExpressCompanyCode));


            //产品
            base.CreateMap<Product, ProductViewModel>();

            //菜单
            base.CreateMap<Menu, TreeViewModel>()
                  .ForMember(dest => dest.ID, options => options.MapFrom(src => src.ID))
                   .ForMember(dest => dest.Label, options => options.MapFrom(src => src.MenuName));
                  
                


        }
    }
}