using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;
using System;

namespace Fero.Data.AutoMapperModule
{
    public static class ApplyCastingModule
    {
        public static void ConfigApplyCastingModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<ApplyCasting, ApplyCastingViewModel>();
            mc.CreateMap<ApplyCastingViewModel, ApplyCasting>()
                .ForMember(des => des.Time, opt => opt.MapFrom(src => DateTime.Now.Ticks));
        }
    }
}
