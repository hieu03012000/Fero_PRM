using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
{
    public static class ModelStyleModule
    {
        public static void ConfigModelStyleModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<ModelStyle, CreateAccountModelStyleViewModel>();
            mc.CreateMap<CreateAccountModelStyleViewModel, ModelStyle>();

            mc.CreateMap<ModelStyle, ModelDetailModelStyleViewModel>()
                 .ForMember(des => des.StyleName, opt => opt.MapFrom(src => src.Style.Name));
            mc.CreateMap<ModelDetailModelStyleViewModel, ModelStyle>();
        }
    }
}
