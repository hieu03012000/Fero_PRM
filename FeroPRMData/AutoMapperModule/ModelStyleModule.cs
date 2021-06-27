using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.ViewModels;

namespace FeroPRMData.AutoMapperModule
{
    public static class ModelStyleModule
    {
        public static void ConfigModelStyleModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<ModelStyle, ModelStyleGeneral>();
            mc.CreateMap<ModelStyleGeneral, ModelStyle>();
            mc.CreateMap<ModelStyle, CreateAccountModelStyleViewModel>();
            mc.CreateMap<CreateAccountModelStyleViewModel, ModelStyle>();
            mc.CreateMap<ModelStyle, ModelDetailModelStyleViewModel>()
                 .ForMember(des => des.StyleName, opt => opt.MapFrom(src => src.Style.Name));
            mc.CreateMap<ModelDetailModelStyleViewModel, ModelStyle>();
        }
    }
}
