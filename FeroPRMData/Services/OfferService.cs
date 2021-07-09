using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.ViewModels;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace FeroPRMData.Services
{
    public partial interface IOfferService:IBaseService<Offer>
    {
        Task<OfferViewModel> Add(OfferViewModel offer);
        Task<List<OfferViewModel>> GetList(string customerId);

        Task<OfferCustomerGetViewModel> GetByCustomer(int id, string customerId);
    }
    public partial class OfferService:BaseService<Offer>, IOfferService
    {
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;
        private readonly IModelOfferRepository _modelOfferRepository;
        private readonly IFavoriteModelRepository _favoriteModelRepository;
        private readonly INotificationService _notificationService;

        public OfferService(IMapper mapper, IOfferRepository offerRepository,
            IModelOfferRepository modelOfferRepository, IFavoriteModelRepository favoriteModelRepository,
            INotificationService notificationService):base(offerRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _modelOfferRepository = modelOfferRepository;
            _favoriteModelRepository = favoriteModelRepository;
            _notificationService = notificationService;
        }

        private int GetNewOfferId()
        {
            var offerId = _offerRepository.Get().OrderByDescending(x => x.Id).FirstOrDefault();
            return offerId.Id;
        }

        public async Task<OfferViewModel> Add(OfferViewModel viewModel)
        {
            var entity = _mapper.Map<Offer>(viewModel);
            entity.CreateTime = DateTime.UtcNow;
            await _offerRepository.CreateAsyn(entity);
            var offerId = GetNewOfferId();
            var favoriteModels = await _favoriteModelRepository
                .Get(x => x.CustomerId.Equals(viewModel.CustomerId))
                .ToListAsync();
            foreach (var favoriteModel in favoriteModels)
            {
                await _modelOfferRepository.CreateAsyn(new ModelOffer { 
                    ModelId = favoriteModel.ModelId, 
                    OfferId = offerId
                });
                await _favoriteModelRepository.DeleteAsync(favoriteModel);
                await _notificationService.ComposeReceiveOfferNotification(offerId, favoriteModel.ModelId, viewModel.CustomerId);
            }
            return viewModel;
        }

        public async Task<List<OfferViewModel>> GetList(string customerId)
        {
            var offers = await _offerRepository.Get(x => x.CustomerId.Equals(customerId))
                .ToListAsync();
            var result = new List<OfferViewModel>();
            foreach (var offer in offers)
            {
                var dto = _mapper.Map<OfferViewModel>(offer);
                result.Add(dto);
            }
            result.Reverse();
            return result;
        }

        public async Task<OfferCustomerGetViewModel> GetByCustomer(int id, string customerId)
        {
            var offer = await _offerRepository
                .FirstOrDefaultAsyn(x => x.Id == id && x.CustomerId.Equals(customerId));
            var modelOffers = await _modelOfferRepository.Get(x => x.OfferId == id)
                .ProjectTo<ModelOfferCustomerGetViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            OfferCustomerGetViewModel dto = _mapper.Map<OfferCustomerGetViewModel>(offer);
            dto.ModelOffers = modelOffers;
            return dto;
        }

    }
}
