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

namespace FeroPRMData.Services
{
    public partial interface IOfferService:IBaseService<Offer>
    {
        Task<List<Offer>> GetOffer();
        Task<List<Offer>> GetOfferById(string customerId);
        Task<OfferWithListModel> GetOfferWithListModel(int offerId);
        Task<ShowOffer> CreateOffers(CreateOffer createOffer);
        Task<ModelOffer> UpdateModelOffer(ShowModelOffer updateModelOffer);
    }
    public partial class OfferService:BaseService<Offer>, IOfferService
    {
        private readonly IMapper _mapper;
        private readonly ICastingRepository _castingRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IModelOfferRepository _modelOfferRepository;
        private readonly IModelRepository _modelRepository;

        public OfferService(IMapper mapper, IOfferRepository offerRepository, IModelRepository modelRepository, IModelOfferRepository modelOfferRepository, ICastingRepository castingRepository) :base(offerRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _castingRepository = castingRepository;
            _modelOfferRepository = modelOfferRepository;
            _modelRepository = modelRepository;
        }

        public async void CreateOffer(string customerId, Offer offer)
        {
            offer.CustomerId = customerId;
            await _offerRepository.CreateAsyn(offer);
        }

        public async void CreateOffer(Offer offer)
        {
            await _offerRepository.CreateAsyn(offer);
        }

        public int GetNewOfferId()
        {
            var offerId = _offerRepository.Get().OrderByDescending(x => x.Id).FirstOrDefault();
            Console.WriteLine(offerId.Id);
  
            return offerId.Id;
        }

        public async Task<ShowOffer> CreateOffers(CreateOffer createOffer)
        {
            var showOffer = _mapper.Map<ShowOffer>(createOffer.Offer);
            showOffer.Time = DateTime.Now;
            await _offerRepository.CreateAsyn(createOffer.Offer);
            var offerId = GetNewOfferId();
            foreach (var item in createOffer.ListModelId)
            {
                ModelOffer mo = new ModelOffer
                {
                    ModelId = item,
                    Time = DateTime.Now,
                    OfferId = offerId
                };
                await _modelOfferRepository.CreateAsyn(mo);
            }
            return showOffer;
        }

        public void Offer(string cusId, string des, double salary, bool monopolistic, DateTime monoTime)
        {
            if (monopolistic == false)
            {
                Offer offer = new Offer
                {
                    CustomerId = cusId,
                    Description = des,
                    Salary = salary
                };
                CreateOffer(offer);
            }
            else
            {
                Offer offer = new Offer
                {
                    CustomerId = cusId,
                    Description = des,
                    Salary = salary,
                    Time = monoTime
                };
                CreateOffer(offer);
            }
        }

        public async Task<List<Offer>> GetOfferById(string customerId)
        {
            var listOffer = await _offerRepository.Get(x => x.CustomerId == customerId).ToListAsync();
            return listOffer;
        }

        public async Task<List<Offer>> GetOffer()
        {
            var listOffer = await _offerRepository.Get().ToListAsync();
            listOffer.Sort((x, y) => DateTime.Compare((DateTime)x.Time, (DateTime)y.Time));
            var newList = listOffer.Skip(Math.Max(0, listOffer.Count() - 10)).ToList();
            return newList;
        }

        public async Task<OfferWithListModel> GetOfferWithListModel(int offerId)
        {
            var offer = await _offerRepository.FirstOrDefaultAsyn(x => x.Id == offerId);
            var listModelId = await _modelOfferRepository.Get(x => x.OfferId == offerId).ToListAsync();
            List<ModelForOffer> lm = new List<ModelForOffer>();
            foreach (var item in listModelId)
            {
                var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == item.ModelId);
                var modelForOffer = _mapper.Map<ModelForOffer>(model);
                modelForOffer.OfferStatus = item.Status;
                lm.Add(modelForOffer);
            }
            OfferWithListModel ow = _mapper.Map<OfferWithListModel>(offer);
            ow.Model = lm;
            return ow;
        }

        public async Task<ModelOffer> UpdateModelOffer(ShowModelOffer updateModelOffer)
        {

            var modelOffer = await _modelOfferRepository.FirstOrDefaultAsyn(x => x.ModelId == updateModelOffer.ModelId && x.OfferId == updateModelOffer.OfferId);
            if (modelOffer == null)
            {
                return null;
            }
            else
            {
                updateModelOffer.Time = DateTime.Now;
                modelOffer = _mapper.Map(updateModelOffer, modelOffer);
                await _modelOfferRepository.UpdateAsync(modelOffer);
                return modelOffer;
            }
        }
    }
}
