using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fero.Data.ViewModels;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface ICastingService : IBaseService<Casting>
    {
        #region hdev
        Task<IQueryable<GetAllCastingViewModel>> FilterCasting(string name, decimal? min, decimal? max);
        Task<DetailCastingViewModel> GetCastingById(int castingId);
        Task<CreateCastingCallViewModel> CreateCasting(CreateCastingCallViewModel model);
        Task<UpdateCastingViewModel> UpdateCasting(UpdateCastingViewModel model);
        Task<int> PublicCasting(int castId, PublicCastingViewModel model);
        Task<int> StopCasting(int castId);
        Task<int> DeleteCasting(int castId);
        Task<int> MakeOffer(MakeOfferViewModel offer);
        #endregion
    }
    public partial class CastingService : BaseService<Casting>, ICastingService
    {
        private readonly IMapper _mapper;
        public CastingService(ICastingRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }
        #region hdev
        private async Task<decimal?> GetMaxSalary()
        {
            var maxSalary = await Get().OrderByDescending(c => c.Salary).FirstOrDefaultAsync();
            return maxSalary.Salary;
        }

        public async Task<IQueryable<GetAllCastingViewModel>> FilterCasting(string name, decimal? min, decimal? max)
        {
            if (min == null) min = 0;
            if (max == null) max = await GetMaxSalary();
            if (name == null) name = "";
            var castingList = Get(c => c.Salary > min && c.Salary < max && c.Name.Contains(name) && c.Status != 0 && c.Status != 6)
                .ProjectTo<GetAllCastingViewModel>(_mapper.ConfigurationProvider);
            return castingList;
        }

        public async Task<DetailCastingViewModel> GetCastingById(int castingId)
        {
            var casting = await Get(c => c.Id == castingId && c.Status != 4)
                .ProjectTo<DetailCastingViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return casting;
        }

        public async Task<CreateCastingCallViewModel> CreateCasting(CreateCastingCallViewModel model)
        {
            if (model.Status != 1 && model.Status != 0) return null;
            var entity = _mapper.Map<Casting>(model);
            await CreateAsyn(entity);
            return model;
        }

        public async Task<UpdateCastingViewModel> UpdateCasting(UpdateCastingViewModel model)
        {
            if (model.Status != 0) return null;
            if (await Get(c => c.Id == model.Id)
                .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() == null)
                return null;
            var entity = _mapper.Map<Casting>(model);
            await UpdateAsync(entity);
            return model;
        }

        public async Task<int> PublicCasting(int castId, PublicCastingViewModel model)
        {
            if (await Get(c => c.Id == castId)
                .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() == null)
                return -1;
            if (Get(c => c.Id == castId)
                .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault().Status != 0)
                return -1;
            var entity = await Get(c => c.Id == castId).FirstOrDefaultAsync();
            entity.Status = 1;
            entity.OpenTime = model.OpenTime;
            entity.CloseTime = model.CloseTime;
            await UpdateAsync(entity);
            return 0;
        }

        public async Task<int> StopCasting(int castId)
        {
            if (await Get(c => c.Id == castId)
                .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() == null)
                return -2;
            if (Get(c => c.Id == castId)
                .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault().Status != 2)
                return -1;
            var entity = await Get(c => c.Id == castId).FirstOrDefaultAsync();
            entity.Status = 4;
            await UpdateAsync(entity);
            return 0;
        }

        public async Task<int> DeleteCasting(int castId)
        {
            if (await Get(c => c.Id == castId)
                .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() == null)
                return -1;
            if (Get(c => c.Id == castId)
                .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault().Status == 2)
                return -1;
            var entity = await Get(c => c.Id == castId).FirstOrDefaultAsync();
            entity.Status = 5;
            await UpdateAsync(entity);
            return 0;
        }

        public async Task<int> MakeOffer(MakeOfferViewModel offer)
        {
            var entity = _mapper.Map<Casting>(offer);
            entity.Status = 6;
            await CreateAsyn(entity);
            return 0;
        }
        #endregion
    }
}
