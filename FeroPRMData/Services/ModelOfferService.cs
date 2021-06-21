using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using System;
using System.Linq;

namespace FeroPRMData.Services
{
    public partial interface IModelOfferService:IBaseService<ModelOffer>
    {
    }
    public partial class ModelOfferService:BaseService<ModelOffer>,IModelOfferService
    {
        private readonly IMapper _mapper;
        private readonly IModelOfferRepository _modelOfferRepository;
        private readonly ICustomerRepository _customerRepository;
        public ModelOfferService(IModelOfferRepository modelOfferRepository, IMapper mapper) :base(modelOfferRepository)
        {
            _mapper = mapper;
            _modelOfferRepository = modelOfferRepository;
        }

        public async void CreateModelOffer(string modelId, Offer offer)
        {
            ModelOffer modelOffer = new ModelOffer
            {
                //0 chua accept
                Status = 0,
                OfferId = offer.Id,
                Time = DateTime.Now,
                ModelId = modelId
            };
            await _modelOfferRepository.CreateAsyn(modelOffer);
        }

        public void GetModelOffer(string modelId)
        {
            _modelOfferRepository.Get(x => x.ModelId == modelId).ToList();
        }

/*        public void GetModelOfferCus(string cusId)
        {
            var cus = _customerRepository.FirstOrDefault(x => x.Id == cusId);
            cus.Offer
            _modelOfferRepository.Get(x => x.ModelId == modelId).ToList();
        }*/
    }
}
