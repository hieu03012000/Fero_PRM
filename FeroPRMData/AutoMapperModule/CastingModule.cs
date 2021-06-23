using AutoMapper;
using FeroPRMData.ViewModels;
using FeroPRMData.Models;

namespace FeroPRMData.AutoMapperModule
{
    public static class CastingModule
    {
        public static void ConfigCastingModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Casting, ShowCasting>();
            mc.CreateMap<Offer, ShowOffer>();
            mc.CreateMap<Casting, NewCastingViewModel>()
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            mc.CreateMap<NewCastingViewModel, Casting>();

            mc.CreateMap<Casting, GetNewCastingViewModel>()
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            mc.CreateMap<GetNewCastingViewModel, Casting>();

            mc.CreateMap<Casting, DetailCastingViewModel>()
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            mc.CreateMap<DetailCastingViewModel, Casting>();

            mc.CreateMap<Casting, CreateCastingCallViewModel>();
            mc.CreateMap<CreateCastingCallViewModel, Casting>();

            mc.CreateMap<Casting, MakeOfferViewModel>();
            mc.CreateMap<MakeOfferViewModel, Casting>();

            mc.CreateMap<Casting, UpdateCastingViewModel>();
            mc.CreateMap<UpdateCastingViewModel, Casting>();
        }
    }
}
