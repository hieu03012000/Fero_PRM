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
        Task<GetGeneralOfferViewModel> GetByModel(int offerId, string modelId);
        Task<List<GetGeneralOfferViewModel>> GetListByModel(string modelId);
        Task<ModelOfferViewModel> Update(ModelOfferViewModel viewModel);
    }
    public partial class ModelOfferService:BaseService<ModelOffer>,IModelOfferService
    {
        private readonly IMapper _mapper;
        private readonly IModelOfferRepository _modelOfferRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly INotificationService _notificationService;

        public ModelOfferService(IMapper mapper, IModelOfferRepository modelOfferRepository,
            ICustomerRepository customerRepository, IOfferRepository offerRepository,
            INotificationService notificationService):base(modelOfferRepository)
        {
            _mapper = mapper;
            _modelOfferRepository = modelOfferRepository;
            _customerRepository = customerRepository;
            _offerRepository = offerRepository;
            _notificationService = notificationService;
        }

        public async Task<GetGeneralOfferViewModel> GetByModel(int offerId, string modelId)
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

        public async Task<List<GetGeneralOfferViewModel>> GetListByModel(string modelId)
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
            list.Sort((x, y) => DateTime.Compare((DateTime)y.CreateTime, (DateTime)x.CreateTime));
            return list;
        }

        public async Task<ModelOfferViewModel> Update(ModelOfferViewModel viewModel)
        {
            var entity = await _modelOfferRepository
                .FirstOrDefaultAsyn(x => x.ModelId.Equals(viewModel.ModelId) && x.OfferId == viewModel.OfferId);
            if (entity == null)
            {
                return null;
            }
            entity.Status = viewModel.Status;
            entity.Time = DateTime.Now;
            await _modelOfferRepository.UpdateAsync(entity);
            if (viewModel.Status == 1)
            {
                var offer = await _offerRepository.FirstOrDefaultAsyn(x => x.Id == viewModel.OfferId);
                await _notificationService.ComposeAcceptOfferNotification(viewModel.OfferId, viewModel.ModelId, offer.CustomerId);
            }
            return viewModel;
        }
    }
}
