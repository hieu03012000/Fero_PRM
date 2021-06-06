using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface ICustomerRepository :IBaseRepository<Customer>
    {
    }
    public partial class CustomerRepository :BaseRepository<Customer>, ICustomerRepository
    {
         public CustomerRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

