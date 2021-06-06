using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IApplyCastingRepository :IBaseRepository<ApplyCasting>
    {
    }
    public partial class ApplyCastingRepository :BaseRepository<ApplyCasting>, IApplyCastingRepository
    {
         public ApplyCastingRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

