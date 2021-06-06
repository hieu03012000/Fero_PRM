using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fero.Data.ViewModels;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface ISubscribeCastingService : IBaseService<SubscribeCasting>
    {
        Task<SubscribeCastingViewModel> SubscribeCastingCall(SubscribeCastingViewModel subCasting);
        Task<SubscribeCastingViewModel> CancelSubscribeCastingCall(SubscribeCastingViewModel subCasting);
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
    }
}
