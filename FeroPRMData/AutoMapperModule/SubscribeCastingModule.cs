using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.ViewModels;

namespace FeroPRMData.AutoMapperModule
{
    public static class SubscribeCastingModule
    {
        public static void ConfigSubscribeCastingModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<SubscribeCasting, SubscribeCastingViewModel>();
            mc.CreateMap<SubscribeCastingViewModel, SubscribeCasting>();
        }
    }
}
