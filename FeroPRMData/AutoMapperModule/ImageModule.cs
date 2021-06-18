using AutoMapper;
using FeroPRMData.ViewModels;
using FeroPRMData.Models;

namespace FeroPRMData.AutoMapperModule
{
    public static class ImageModule
    {
        public static void ConfigImageModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Image, CreateAccountImageViewModel>();
            mc.CreateMap<CreateAccountImageViewModel, Image>();

            mc.CreateMap<Image, ModelDetailImageViewModel>();
            mc.CreateMap<ModelDetailImageViewModel, Image>();
        }
    }
}
