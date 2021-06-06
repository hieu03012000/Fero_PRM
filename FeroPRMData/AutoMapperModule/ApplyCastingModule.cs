using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
{
    public static class ApplyCastingModule
    {
        public static void ConfigApplyCastingModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<ApplyCasting, ApplyCastingViewModel>();
            mc.CreateMap<ApplyCastingViewModel, ApplyCasting>();
        }
    }
}
