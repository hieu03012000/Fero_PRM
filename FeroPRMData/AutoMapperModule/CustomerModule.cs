using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;

namespace Fero.Data.AutoMapperModule
{
    public static class CustomerModule
    {
        public static void ConfigCustomerModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Customer, CreateCustomerAccountViewModel>();
            mc.CreateMap<CreateCustomerAccountViewModel, Customer>();
        }
    }
}
