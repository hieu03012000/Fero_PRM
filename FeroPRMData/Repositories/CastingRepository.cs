using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface ICastingRepository :IBaseRepository<Casting>
    {
    }
    public partial class CastingRepository :BaseRepository<Casting>, ICastingRepository
    {
         public CastingRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

