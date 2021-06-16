using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;

namespace FeroPRMData.Services
{
    public partial interface IModelOfferService:IBaseService<ModelOffer>
    {
    }
    public partial class ModelOfferService:BaseService<ModelOffer>,IModelOfferService
    {
        public ModelOfferService(IModelOfferRepository repository):base(repository){}
    }
}
