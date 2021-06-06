using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IStyleRepository :IBaseRepository<Style>
    {
    }
    public partial class StyleRepository :BaseRepository<Style>, IStyleRepository
    {
         public StyleRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

