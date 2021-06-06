using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IModelStyleRepository :IBaseRepository<ModelStyle>
    {
    }
    public partial class ModelStyleRepository :BaseRepository<ModelStyle>, IModelStyleRepository
    {
         public ModelStyleRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

