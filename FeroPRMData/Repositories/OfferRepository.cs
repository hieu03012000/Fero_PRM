using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IOfferRepository :IBaseRepository<Offer>
    {
    }
    public partial class OfferRepository :BaseRepository<Offer>, IOfferRepository
    {
         public OfferRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

