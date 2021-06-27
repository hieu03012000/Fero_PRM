using AutoMapper;
using AutoMapper.QueryableExtensions;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
//test /shdasdhakdasdhjasdjasdhaksjdf
namespace FeroPRMData.Services
{
    public partial interface IApplyCastingService : IBaseService<ApplyCasting>
    {
        /*        Task<ApplyCastingViewModel> ApplyCastingCall(ApplyCastingViewModel applyCasting);
                Task<ApplyCastingViewModel> CancelApplyCastingCall(ApplyCastingViewModel applyCasting);*/
        Task<ApplyCasting> ApplyCastingCalls(ApplyCasting applyCasting);
        Task<ApplyCasting> DeleteApplyCasting(ApplyCasting applyCasting);
        Task<bool> CheckApplyCasting(int id, string modelId);
    }
    public partial class ApplyCastingService : BaseService<ApplyCasting>, IApplyCastingService
    {
        private readonly IMapper _mapper;
        private readonly IApplyCastingRepository _applyCastingRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICastingRepository _castingRepository;
        public ApplyCastingService(IApplyCastingRepository applyCastingRepository, ICastingRepository castingRepository, IModelRepository modelRepository, IMapper mapper) : base(applyCastingRepository)
        {
            _mapper = mapper;
            _applyCastingRepository = applyCastingRepository;
            _modelRepository = modelRepository;
            _castingRepository = castingRepository;
        }

/*        public async Task<ApplyCastingViewModel> ApplyCastingCall(ApplyCastingViewModel applyCasting)
        {
            var apply = await Get(ac => ac.CastingId == applyCasting.CastingId && ac.ModelId == applyCasting.ModelId)
                .FirstOrDefaultAsync();
            if (apply != null) return null;
            var entity = _mapper.Map<ApplyCasting>(applyCasting);
            await _applyCastingRepository.CreateAsyn(entity);
            return applyCasting;
        }*/

/*        public async Task<ApplyCastingViewModel> CancelApplyCastingCall(ApplyCastingViewModel applyCasting)
        {
            var apply = await Get(ac => ac.CastingId == applyCasting.CastingId && ac.ModelId == applyCasting.ModelId)
                .ProjectTo<ApplyCastingViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            if (apply == null) return null;
            var entity = _mapper.Map<ApplyCasting>(applyCasting);
            await DeleteAsync(entity);
            return applyCasting;
        }*/

        public async Task<ApplyCasting> ApplyCastingCalls(ApplyCasting applyCasting)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == applyCasting.CastingId);
            var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == applyCasting.ModelId);
            if(model == null || casting == null)
            {
                return null;
            }
            else
            {
                applyCasting.Time = DateTime.Now;
                await CreateAsyn(applyCasting);
                return applyCasting;
            }
        }

        public async Task<ApplyCasting> DeleteApplyCasting(ApplyCasting applyCasting)
        {
            var apply = await _applyCastingRepository.Get(x => x.CastingId == applyCasting.CastingId &&
                                                               x.ModelId == applyCasting.ModelId).FirstOrDefaultAsync();
            if(apply == null)
            {
                return null;
            }
            else
            {
                await DeleteAsync(apply);
                return apply;
            }
        }

        public async Task<bool> CheckApplyCasting(int id, string modelId)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == id);
            var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == modelId);
            var apply = await _applyCastingRepository.FirstOrDefaultAsyn(x => x.CastingId == id && x.ModelId == modelId);
            if (model == null || casting == null || apply == null)
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
