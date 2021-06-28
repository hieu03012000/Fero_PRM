using AutoMapper;
using AutoMapper.QueryableExtensions;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface ISubscribeCastingService : IBaseService<SubscribeCasting>
    {
        Task<SubscribeCastingViewModel> SubscribeCastingCall(SubscribeCastingViewModel subCasting);
        Task<SubscribeCastingViewModel> CancelSubscribeCastingCall(SubscribeCastingViewModel subCasting);
        Task<List<ShowCasting>> GetSubscribeCastings(string modelId);
        Task<bool> CheckSubscribeCasting(int id, string modelId);

    }
    public partial class SubscribeCastingService : BaseService<SubscribeCasting>, ISubscribeCastingService
    {
        private readonly IMapper _mapper;
        private readonly ISubscribeCastingRepository _subscribeCastingRepository;
        private readonly ICastingRepository _castingRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICustomerRepository _customerRepository;

        public SubscribeCastingService(IMapper mapper, ISubscribeCastingRepository subscribeCastingRepository, ICastingRepository castingRepository, IModelRepository modelRepository, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _subscribeCastingRepository = subscribeCastingRepository;
            _castingRepository = castingRepository;
            _modelRepository = modelRepository;
            _customerRepository = customerRepository;
        }

        public async Task<SubscribeCastingViewModel> SubscribeCastingCall(SubscribeCastingViewModel subCasting)
        {
            var apply = await Get(ac => ac.CastingId == subCasting.CastingId && ac.ModelId == subCasting.ModelId)
                .FirstOrDefaultAsync();
            if (apply != null)
            {
                return null;
            }
            var entity = _mapper.Map<SubscribeCasting>(subCasting);
            await CreateAsyn(entity);
            return subCasting;
        }

        public async Task<SubscribeCastingViewModel> CancelSubscribeCastingCall(SubscribeCastingViewModel subCasting)
        {
            var apply = await Get(ac => ac.CastingId == subCasting.CastingId && ac.ModelId == subCasting.ModelId)
                .ProjectTo<SubscribeCastingViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            if (apply == null) return null;
            var entity = _mapper.Map<SubscribeCasting>(subCasting);
            await DeleteAsync(entity);
            return subCasting;
        }

        public async Task<List<ShowCasting>> GetSubscribeCastings(string modelId)
        {
            var subscribeCastings = await _subscribeCastingRepository.Get(x => x.ModelId == modelId).ToListAsync();
            List<ShowCasting> list = new List<ShowCasting>();
            foreach (var item in subscribeCastings)
            {
                var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == item.CastingId);
                if (casting.Status == 1)
                {
                    var dto = _mapper.Map<ShowCasting>(casting);
                    var customer = _customerRepository.FirstOrDefault(c => c.Id.Equals(casting.CustomerId));
                    dto.CustomerName = customer.Name;
                    list.Add(dto);
                }
            }
            return list;
        }

        public async Task<bool> CheckSubscribeCasting(int castingId, string modelId)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == castingId);
            var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == modelId);
            var sup = await _subscribeCastingRepository.FirstOrDefaultAsyn(x => x.CastingId == castingId && x.ModelId == modelId);
            if(model == null || casting == null || sup == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
