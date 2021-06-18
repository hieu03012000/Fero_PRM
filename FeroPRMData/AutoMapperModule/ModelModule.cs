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

            mc.CreateMap<Model, ModelDetailViewModel>();
            mc.CreateMap<ModelDetailViewModel, Model>();

            mc.CreateMap<Model, ApplicantListViewModel>();
            mc.CreateMap<ApplicantListViewModel, Model>();

            


        }
    }
}
