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
            mc.CreateMap<ShowCasting, Casting>();
            mc.CreateMap<Offer, ShowOffer>();
            mc.CreateMap<Model, ListModelCasting>();
            mc.CreateMap<ListModelCasting, Model>();

            mc.CreateMap<Casting, GetNewCastingViewModel>()
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            mc.CreateMap<GetNewCastingViewModel, Casting>();

            mc.CreateMap<Casting, DetailCastingViewModel>()
                .ForMember(des => des.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            mc.CreateMap<DetailCastingViewModel, Casting>();

            mc.CreateMap<Casting, CreateCastingCallViewModel>();
            mc.CreateMap<CreateCastingCallViewModel, Casting>();

            mc.CreateMap<Casting, CastingViewModel>();
            mc.CreateMap<CastingViewModel, Casting>();

            mc.CreateMap<Casting, CastingModelSearchViewModel>();
            mc.CreateMap<CastingModelSearchViewModel, Casting>();

            mc.CreateMap<Casting, CastingModelGetViewModel>();
            mc.CreateMap<CastingModelGetViewModel, Casting>();

            mc.CreateMap<Casting, CastingImportViewModel>();
            mc.CreateMap<CastingImportViewModel, Casting>();
        }
    }
}
