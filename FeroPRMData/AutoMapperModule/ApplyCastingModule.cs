using AutoMapper;
using FeroPRMData.ViewModels;
using FeroPRMData.Models;
using System;

namespace FeroPRMData.AutoMapperModule
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
