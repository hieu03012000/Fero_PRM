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
/*        Task<SubscribeCastingViewModel> SubscribeCastingCall(SubscribeCastingViewModel subCasting);
        Task<SubscribeCastingViewModel> CancelSubscribeCastingCall(SubscribeCastingViewModel subCasting);*/
        Task<List<SubscribeCasting>> GetSubscribeCastings(string modelId);
        Task<SubscribeCasting> DeleteSubscribeCasting(SubscribeCasting subscribeCasting);
    }
    public partial class SubscribeCastingService : BaseService<SubscribeCasting>, ISubscribeCastingService
    {
        private readonly IMapper _mapper;
        private readonly ISubscribeCastingRepository _subscribeCastingRepository;
        public SubscribeCastingService(ISubscribeCastingRepository subscribeCastingRepository, IMapper mapper) : base(subscribeCastingRepository)
        {
            _mapper = mapper;
            _subscribeCastingRepository = subscribeCastingRepository;
        }
/*        public async Task<SubscribeCastingViewModel> SubscribeCastingCall(SubscribeCastingViewModel subCasting)
        {
            var apply = await Get(ac => ac.CastingId == subCasting.CastingId && ac.ModelId == subCasting.ModelId)
                .FirstOrDefaultAsync();
            if (apply != null) return null;
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
        }*/

        public async Task<List<SubscribeCasting>> GetSubscribeCastings(string modelId)
        {
            var list = await _subscribeCastingRepository.Get(x => x.ModelId == modelId).ToListAsync();
            return list;
        }

        public async Task<SubscribeCasting> DeleteSubscribeCasting(SubscribeCasting subscribeCasting)
        {
            var subCasting = await _subscribeCastingRepository.FirstOrDefaultAsyn(x => x.CastingId == subscribeCasting.CastingId &&
                                                                                       x.ModelId == subscribeCasting.ModelId);
            if(subCasting == null)
            {
                return null;
            }
            else
            {
                await DeleteAsync(subCasting);
                return subCasting;
            }
        }
    }
}
