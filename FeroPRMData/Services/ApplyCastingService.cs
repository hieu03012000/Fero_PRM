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
    public partial interface IApplyCastingService : IBaseService<ApplyCasting>
    {
        Task<ApplyCastingViewModel> ApplyCastingCall(ApplyCastingViewModel applyCasting);
        Task<ApplyCastingViewModel> CancelApplyCastingCall(ApplyCastingViewModel applyCasting);
        Task<bool> CheckApplyCasting(int castingId, string modelId);
        Task<List<GetGeneralModelViewModel>> GetApplyModel(int castingId);
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

        public async Task<ApplyCastingViewModel> ApplyCastingCall(ApplyCastingViewModel applyCasting)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == applyCasting.CastingId);
            var time = DateTime.UtcNow;
            if (time < casting.OpenTime || time > casting.CloseTime)
            {
                return null;
            }
            var entity = _mapper.Map<ApplyCasting>(applyCasting);
            entity.Time = time;
            await _applyCastingRepository.CreateAsyn(entity);
            return applyCasting;
        }

        public async Task<ApplyCastingViewModel> CancelApplyCastingCall(ApplyCastingViewModel applyCasting)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == applyCasting.CastingId);
            var time = DateTime.UtcNow;
            if (time < casting.OpenTime || time > casting.CloseTime)
            {
                return null;
            }
            var entity = _mapper.Map<ApplyCasting>(applyCasting);
            await _applyCastingRepository.DeleteAsync(entity);
            return applyCasting;
        }

        public async Task<bool> CheckApplyCasting(int castingId, string modelId)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == castingId);
            var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == modelId);
            var apply = await _applyCastingRepository.FirstOrDefaultAsyn(x => x.CastingId == castingId && x.ModelId == modelId);
            if (model == null || casting == null || apply == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<List<GetGeneralModelViewModel>> GetApplyModel(int castingId)
        {
            var listApplyCasting = await _applyCastingRepository.Get(x => x.CastingId == castingId).ToListAsync();
            List<GetGeneralModelViewModel> lm = new List<GetGeneralModelViewModel>();
            foreach (var item in listApplyCasting)
            {
                var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == item.ModelId);
                var des = _mapper.Map<GetGeneralModelViewModel>(model);
                lm.Add(des);
            }
            return lm;
        }
    }
}
