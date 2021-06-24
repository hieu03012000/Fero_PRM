using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
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
        Task<bool> CheckCusGmail(string mail);
        Task<Customer> GetCustomerProfile(string mail);
        Task<Customer> CreateCustomer(Customer newCustomer);

    }
    public partial class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper) : base(customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }
        #region hdev
        private string GetCustomerId()
        {
            var customer = Get().OrderByDescending(m => m.Id).FirstOrDefault();
            int num;
            if (customer == null) num = 0;
            else num = int.Parse(customer.Id.Substring(2));
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

            return customer;
        }
        #endregion

        public async Task<Customer> GetCustomerProfile(string mail)
        {
            var customer = await Get(c => c.Gmail.Equals(mail)).FirstOrDefaultAsync();

            return customer;
        }

        public async Task<bool> CheckCusGmail(string mail)
        {
            var cus = await _customerRepository.FirstOrDefaultAsyn(x => x.Gmail == mail);
            if(cus != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Customer> CreateCustomer(Customer newCustomer)
        {
            var customer = await _customerRepository.FirstOrDefaultAsyn(x => x.Gmail == newCustomer.Gmail);
            if(customer != null)
            {
                return null;
            }
            else
            {
                await CreateAsyn(newCustomer);
                return newCustomer;
            }
        }

    }
}
