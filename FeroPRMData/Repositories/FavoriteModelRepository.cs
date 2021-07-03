using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IFavoriteModelRepository : IBaseRepository<FavoriteModel>
    {
    }
    public partial class FavoriteModelRepository : BaseRepository<FavoriteModel>, IFavoriteModelRepository
    {
         public FavoriteModelRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

