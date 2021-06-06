using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
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
