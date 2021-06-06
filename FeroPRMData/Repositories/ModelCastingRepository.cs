using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IModelCastingRepository :IBaseRepository<ModelCasting>
    {
    }
    public partial class ModelCastingRepository :BaseRepository<ModelCasting>, IModelCastingRepository
    {
         public ModelCastingRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

