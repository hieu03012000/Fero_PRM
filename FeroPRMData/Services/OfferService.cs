using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using System;

namespace FeroPRMData.Services
{
    public partial interface IOfferService:IBaseService<Offer>
    {
    }
    public partial class OfferService:BaseService<Offer>,IOfferService
    {
        private readonly IMapper _mapper;
        private readonly ICastingRepository _castingRepository;
        private readonly IOfferRepository _offerRepository;
        public OfferService(IMapper mapper, IOfferRepository offerRepository, ICastingRepository castingRepository) :base(offerRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _castingRepository = castingRepository;
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
    }
}
