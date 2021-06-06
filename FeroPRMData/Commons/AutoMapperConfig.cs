using AutoMapper;
using Fero.Data.AutoMapperModule;
using Microsoft.Extensions.DependencyInjection;
namespace FeroPRMData.Commons
{
    public static class AutoMapperConfig
    {

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.ConfigModelModule();
                mc.ConfigCastingModule();
                mc.ConfigModelStyleModule();
                mc.ConfigImageModule();
                mc.ConfigApplyCastingModule();
                mc.ConfigSubscribeCastingModule();
                mc.ConfigModelCastingModule();
                mc.ConfigCustomerModule();
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
