using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface IModelOfferService:IBaseService<ModelOffer>
    {
        Task<GetGeneralOfferViewModel> GetById(string modelId, int offerId);
        Task<List<GetGeneralOfferViewModel>> GetByModelId(string modelId);
    }
    public partial class ModelOfferService:BaseService<ModelOffer>,IModelOfferService
    {
        private readonly IMapper _mapper;
        private readonly IModelOfferRepository _modelOfferRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOfferRepository _offerRepository;

        public ModelOfferService(IMapper mapper, IModelOfferRepository modelOfferRepository, ICustomerRepository customerRepository, IOfferRepository offerRepository)
        {
            _mapper = mapper;
            _modelOfferRepository = modelOfferRepository;
            _customerRepository = customerRepository;
            _offerRepository = offerRepository;
        }

        public async Task<GetGeneralOfferViewModel> GetById(string modelId, int offerId)
        {
            var modelOffers = _modelOfferRepository.FirstOrDefault(x => x.ModelId.Equals(modelId) && x.OfferId == offerId);
            if (modelOffers == null)
            {
                return null;
            }
            var offer = await _offerRepository.FirstOrDefaultAsyn(x => x.Id == offerId);
            var customer = _customerRepository.FirstOrDefault(x => x.Id.Equals(offer.CustomerId));
            var target = _mapper.Map<GetGeneralOfferViewModel>(offer);
            target.ModelOfferStatus = modelOffers.Status;
            target.CustomerName = customer.Name;
            return target;
        }

        public async Task<List<GetGeneralOfferViewModel>> GetByModelId(string modelId)
        {
            var modelOffers = await _modelOfferRepository.Get(x => x.ModelId == modelId).ToListAsync();
            List<GetGeneralOfferViewModel> list = new List<GetGeneralOfferViewModel>();
            foreach (var item in modelOffers)
            {
                var offer = await _offerRepository.FirstOrDefaultAsyn(x => x.Id == item.OfferId);
                var customer = _customerRepository.FirstOrDefault(x => x.Id.Equals(offer.CustomerId));
                var target = _mapper.Map<GetGeneralOfferViewModel>(offer);
                target.ModelOfferStatus = item.Status;
                target.CustomerName = customer.Name;
                list.Add(target);
            }
            list.Sort((x, y) => DateTime.Compare((DateTime)y.Time, (DateTime)x.Time));
            return list;
        }

    }
}
