using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FeroPRMData.Models;
using FeroPRMData.Services;
using FeroPRMData.Repositories;

namespace FeroPRMData.Commons
{
    public static class DependencyInjectionResolverGen
    {
        public static void InitializerDI(this IServiceCollection services)
        {
            services.AddScoped<DbContext, Fero_PRMContext>();
        
            services.AddScoped<IApplyCastingService, ApplyCastingService>();
            services.AddScoped<IApplyCastingRepository, ApplyCastingRepository>();
        
            services.AddScoped<ICastingService, CastingService>();
            services.AddScoped<ICastingRepository, CastingRepository>();
        
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageRepository, ImageRepository>();
        
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IModelRepository, ModelRepository>();
        
            services.AddScoped<IModelOfferService, ModelOfferService>();
            services.AddScoped<IModelOfferRepository, ModelOfferRepository>();
        
            services.AddScoped<IModelStyleService, ModelStyleService>();
            services.AddScoped<IModelStyleRepository, ModelStyleRepository>();
        
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
        
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IOfferRepository, OfferRepository>();
        
            services.AddScoped<IStyleService, StyleService>();
            services.AddScoped<IStyleRepository, StyleRepository>();
        
            services.AddScoped<ISubscribeCastingService, SubscribeCastingService>();
            services.AddScoped<ISubscribeCastingRepository, SubscribeCastingRepository>();

            services.AddScoped<IFavoriteModelService, FavoriteModelService>();
            services.AddScoped<IFavoriteModelRepository, FavoriteModelRepository>();
        }
    }
}
