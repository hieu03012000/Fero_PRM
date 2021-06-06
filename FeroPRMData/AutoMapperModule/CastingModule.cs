using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
{
    public static class CastingModule
    {
        public static void ConfigCastingModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Casting, GetAllCastingViewModel>()
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            mc.CreateMap<GetAllCastingViewModel, Casting>();

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
