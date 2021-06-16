using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IModelOfferRepository :IBaseRepository<ModelOffer>
    {
    }
    public partial class ModelOfferRepository :BaseRepository<ModelOffer>, IModelOfferRepository
    {
         public ModelOfferRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

