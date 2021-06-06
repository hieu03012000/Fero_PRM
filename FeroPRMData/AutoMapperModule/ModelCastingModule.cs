using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
{
    public static class ModelCastingModule
    {
        public static void ConfigModelCastingModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<ModelCasting, AcceptCastingViewModel>();
            mc.CreateMap<AcceptCastingViewModel, ModelCasting>();

            mc.CreateMap<ModelCasting, MakeOfferModelCastingViewModel>();
            mc.CreateMap<MakeOfferModelCastingViewModel, ModelCasting>();

            
        }
    }
}
