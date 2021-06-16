using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;

namespace FeroPRMData.Services
{
    public partial interface IOfferService:IBaseService<Offer>
    {
    }
    public partial class OfferService:BaseService<Offer>,IOfferService
    {
        public OfferService(IOfferRepository repository):base(repository){}
    }
}
