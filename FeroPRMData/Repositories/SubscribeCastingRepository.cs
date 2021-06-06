using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface ISubscribeCastingRepository :IBaseRepository<SubscribeCasting>
    {
    }
    public partial class SubscribeCastingRepository :BaseRepository<SubscribeCasting>, ISubscribeCastingRepository
    {
         public SubscribeCastingRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

