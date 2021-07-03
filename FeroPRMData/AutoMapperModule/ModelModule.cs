using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.ViewModels;

namespace FeroPRMData.AutoMapperModule
{
    public static class ModelModule
    {
        public static void ConfigModelModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Model, CreateModelAccountViewModel>();
            mc.CreateMap<CreateModelAccountViewModel, Model>()
                .ForMember(des => des.Status, opt => opt.MapFrom(src => 1));
            mc.CreateMap<Model, ApplicantListViewModel>();
            mc.CreateMap<ApplicantListViewModel, Model>();
            mc.CreateMap<GetGeneralOfferViewModel, Offer>();
            mc.CreateMap<Offer, GetGeneralOfferViewModel>();
            mc.CreateMap<Model, Offer>();
            mc.CreateMap<Offer, GetGeneralOfferViewModel>();

            mc.CreateMap<Model, GetModelViewModel>();
            mc.CreateMap<GetModelViewModel, Model>();
            mc.CreateMap<Model, GetGeneralModelViewModel>();
            mc.CreateMap<GetGeneralModelViewModel, Model>();
        }
    }
}
