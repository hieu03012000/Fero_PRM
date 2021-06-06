using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
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
