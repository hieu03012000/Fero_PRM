using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface ICustomerService : IBaseService<Customer>
    {
        #region hdev
        Task<CreateCustomerAccountViewModel> CreateCustomerAccount(CreateCustomerAccountViewModel customer);
        Task<Customer> CustomerLogin(string customer);
        #endregion
    }
    public partial class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }
        #region hdev
        private string GetCustomerId()
        {
            var customerId = Get().OrderByDescending(m => m.Id).FirstOrDefault().Id;
            int num = int.Parse(customerId.Substring(2));
            return "CM" + string.Format("{0 :D4}", ++num);
        }
        public async Task<CreateCustomerAccountViewModel> CreateCustomerAccount(CreateCustomerAccountViewModel customer)
        {
            if (await FirstOrDefaultAsyn(m => m.Gmail == customer.Gmail) != null)
                return null;
            var entity = _mapper.Map<Customer>(customer);
            entity.Id = GetCustomerId();
            await CreateAsyn(entity);
            return customer;
        }

        public async Task<Customer> CustomerLogin(string mail)
        {
            var customer = await Get(c => c.Gmail.Equals(mail)).FirstOrDefaultAsync();

            return null;
        }
        #endregion
    }
}
