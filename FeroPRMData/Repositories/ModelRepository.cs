using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IModelRepository :IBaseRepository<Model>
    {
    }
    public partial class ModelRepository :BaseRepository<Model>, IModelRepository
    {
         public ModelRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

