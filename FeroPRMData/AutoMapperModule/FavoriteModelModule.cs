using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.ViewModels;

namespace FeroPRMData.AutoMapperModule
{
    public static class FavoriteModelModule
    {
        public static void ConfigFavoriteModelModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<FavoriteModel, FavoriteModelViewModel>();
            mc.CreateMap<FavoriteModelViewModel, FavoriteModel>();
        }
    }
}
