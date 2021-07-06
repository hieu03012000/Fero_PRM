using AutoMapper;
using FeroPRMData.ViewModels;
using FeroPRMData.Models;

namespace FeroPRMData.AutoMapperModule
{
    public static class OfferModule
    {
        public static void ConfigOfferModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Offer, OfferViewModel>();
            mc.CreateMap<OfferViewModel, Offer>();
            mc.CreateMap<Offer, OfferCustomerGetViewModel>();
            mc.CreateMap<OfferCustomerGetViewModel, Offer>();
        }
    }
}
