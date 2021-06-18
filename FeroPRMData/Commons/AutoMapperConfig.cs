using AutoMapper;
using FeroPRMData.AutoMapperModule;
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
                mc.ConfigStyleModule();
                mc.ConfigImageModule();
                mc.ConfigApplyCastingModule();
                mc.ConfigSubscribeCastingModule();
                mc.ConfigCustomerModule();
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
