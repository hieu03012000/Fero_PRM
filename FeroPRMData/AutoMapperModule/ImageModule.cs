using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
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
