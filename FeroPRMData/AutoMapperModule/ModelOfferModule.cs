using AutoMapper;
using FeroPRMData.ViewModels;
using FeroPRMData.Models;

namespace FeroPRMData.AutoMapperModule
{
    public static class ModelOfferModule
    {
        public static void ConfigModelOfferModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<ModelOffer, ModelOfferViewModel>();
            mc.CreateMap<ModelOfferViewModel, ModelOffer>();

            mc.CreateMap<ModelOffer, ModelOfferCustomerGetViewModel>();
            mc.CreateMap<ModelOfferCustomerGetViewModel, ModelOffer>();
        }
    }
}
