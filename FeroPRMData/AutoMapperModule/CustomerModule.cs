using AutoMapper;
using FeroPRMData.ViewModels;
using FeroPRMData.Models;

namespace FeroPRMData.AutoMapperModule
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
