using AutoMapper;
using FeroPRMData.ViewModels;
using FeroPRMData.Models;

namespace FeroPRMData.AutoMapperModule
{
    public static class StyleModule
    {
        public static void ConfigStyleModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Style, GetAllStyleViewModel>();
            mc.CreateMap<GetAllStyleViewModel, Style>();
        }
    }
}
