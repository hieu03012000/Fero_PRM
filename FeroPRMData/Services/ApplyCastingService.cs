using AutoMapper;
using AutoMapper.QueryableExtensions;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface IApplyCastingService : IBaseService<ApplyCasting>
    {
        Task<ApplyCastingViewModel> ApplyCastingCall(ApplyCastingViewModel applyCasting);
        Task<ApplyCastingViewModel> CancelApplyCastingCall(ApplyCastingViewModel applyCasting);
    }
    public partial class ApplyCastingService : BaseService<ApplyCasting>, IApplyCastingService
    {
        private readonly IMapper _mapper;
        public ApplyCastingService(IApplyCastingRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<ApplyCastingViewModel> ApplyCastingCall(ApplyCastingViewModel applyCasting)
        {
            var apply = await Get(ac => ac.CastingId == applyCasting.CastingId && ac.ModelId == applyCasting.ModelId)
                .FirstOrDefaultAsync();
            if (apply != null) return null;
            var entity = _mapper.Map<ApplyCasting>(applyCasting);
            await CreateAsyn(entity);
            return applyCasting;
        }
        public async Task<ApplyCastingViewModel> CancelApplyCastingCall(ApplyCastingViewModel applyCasting)
        {
            var apply = await Get(ac => ac.CastingId == applyCasting.CastingId && ac.ModelId == applyCasting.ModelId)
                .ProjectTo<ApplyCastingViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            if (apply == null) return null;
            var entity = _mapper.Map<ApplyCasting>(applyCasting);
            await DeleteAsync(entity);
            return applyCasting;
        }
    }
}
