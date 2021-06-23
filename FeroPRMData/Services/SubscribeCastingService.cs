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
        Task<List<SubscribeCasting>> GetSubscribeCastings(string modelId);
    }
    public partial class SubscribeCastingService : BaseService<SubscribeCasting>, ISubscribeCastingService
    {
        private readonly IMapper _mapper;
        public SubscribeCastingService(ISubscribeCastingRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }
        public async Task<SubscribeCastingViewModel> SubscribeCastingCall(SubscribeCastingViewModel subCasting)
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
        }

        public async Task<List<SubscribeCasting>> GetSubscribeCastings(string modelId)
        {
            var list = await _repository.Get(x => x.ModelId == modelId).ToListAsync();
            return list;
        }
    }
}
