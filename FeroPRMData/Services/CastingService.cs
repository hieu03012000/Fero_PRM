using AutoMapper;
using AutoMapper.QueryableExtensions;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface ICastingService : IBaseService<Casting>
    {
        Task<IQueryable<CastingModelSearchViewModel>> GetNew();
        Task<IQueryable<CastingModelSearchViewModel>> Search(string name, double? min, double? max);
        Task<CastingModelGetViewModel> GetByModel(int id, string modelId);
        Task<List<CastingViewModel>> GetList(string customerId);
        Task<List<int>> GetIdList(string customerId);
        Task<CastingViewModel> GetByCustomer(int id, string customerId);
        Task<List<CastingImportViewModel>> GetImportList(string customerId);
        Task<CastingViewModel> Add(CastingViewModel viewModel);
        Task<CastingViewModel> Update(CastingViewModel viewModel);
        Task<CastingViewModel> Delete(int id, string customerId);
        Task<CastingViewModel> Stop(int id, string customerId);
        DateTime GetRecentSchedule();
    }
    public partial class CastingService : BaseService<Casting>, ICastingService
    {
        private readonly IMapper _mapper;
        private readonly ICastingRepository _castingRepository;
        private readonly IApplyCastingRepository _applyCastingRepository;
        private readonly ISubscribeCastingRepository _subscribeCastingRepository;

        public CastingService(IMapper mapper, ICastingRepository castingRepository,
            IApplyCastingRepository applyCastingRepository,
            ISubscribeCastingRepository subscribeCastingRepository) : base(castingRepository)
        {
            _mapper = mapper;
            _castingRepository = castingRepository;
            _applyCastingRepository = applyCastingRepository;
            _subscribeCastingRepository = subscribeCastingRepository;
        }

        public async Task<IQueryable<CastingModelSearchViewModel>> GetNew()
        {
            if (await Get().FirstOrDefaultAsync() == null)
            {
                return null;
            }
            var castingList = Get(c => c.Status == 1)
                .OrderByDescending(c => c.CreateTime)
                .Take(10)
                .ProjectTo<CastingModelSearchViewModel>(_mapper.ConfigurationProvider);
            return castingList;
        }

        private async Task<double?> GetMaxSalary()
        {
            var maxSalary = await Get().OrderByDescending(c => c.Salary).FirstOrDefaultAsync();
            return maxSalary.Salary;
        }

        private async Task<int> GetLastCastingId()
        {
            var lastCasting = await Get().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return lastCasting.Id;
        }

        public async Task<IQueryable<CastingModelSearchViewModel>> Search(string name, double? min, double? max)
        {
            if (min == null)
            {
                min = 0;
            }
            if (max == null)
            {
                max = await GetMaxSalary();
            }
            if (name == null)
            {
                name = "";
            }
            var castingList = Get(c => c.Salary >= min && c.Salary <= max && c.Name.Contains(name) && c.Status == 1)
                .OrderByDescending(c => c.CreateTime)
                .ProjectTo<CastingModelSearchViewModel>(_mapper.ConfigurationProvider);
            return castingList;
        }

        public async Task<CastingModelGetViewModel> GetByModel(int id, string modelId)
        {
            var casting = await _castingRepository.Get(c => c.Id == id && c.Status == 1)
                .ProjectTo<CastingModelGetViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (casting == null)
            {
                return null;
            }
            var subscribe = await _subscribeCastingRepository
                .FirstOrDefaultAsyn(x => x.CastingId == id && x.ModelId.Equals(modelId));
            var apply = await _applyCastingRepository
                .FirstOrDefaultAsyn(x => x.CastingId == id && x.ModelId.Equals(modelId));
            casting.IsSubscribe = subscribe != null;
            casting.IsApply = apply != null;
            return casting;
        }

        public async Task<List<CastingViewModel>> GetList(string customerId)
        {
            var listCasting = await _castingRepository
                .Get(x => x.CustomerId == customerId && x.Status != 2)
                .ProjectTo<CastingViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            listCasting.Reverse();
            return listCasting;
        }

        public async Task<CastingViewModel> GetByCustomer(int id, string customerId)
        {
            var casting = await _castingRepository
                .Get(c => c.Id == id && c.Status != 2 && c.CustomerId.Equals(customerId))
                .ProjectTo<CastingViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return casting;
        }

        public async Task<List<CastingImportViewModel>> GetImportList(string customerId)
        {
            var listCasting = await _castingRepository
                .Get(x => x.CustomerId == customerId && x.Status != 2)
                .ProjectTo<CastingImportViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return listCasting;
        }

        public async Task<CastingViewModel> Add(CastingViewModel viewModel)
        {
            if (viewModel.Status != 1 && viewModel.Status != 0)
            {
                return null;
            }
            if (viewModel.OpenTime != null
                && viewModel.CloseTime != null)
            {
                var openTime = (DateTime)viewModel.OpenTime;
                viewModel.OpenTime = openTime
                    .AddSeconds(-openTime.Second)
                    .AddMilliseconds(-openTime.Millisecond);
                var closeTime = (DateTime)viewModel.CloseTime;
                viewModel.CloseTime = closeTime
                    .AddSeconds(-closeTime.Second)
                    .AddMilliseconds(-closeTime.Millisecond);
            }
            var entity = _mapper.Map<Casting>(viewModel);
            entity.CreateTime = DateTime.UtcNow;
            await CreateAsyn(entity);
            viewModel.Id = await GetLastCastingId();
            return viewModel;
        }

        public async Task<CastingViewModel> Update(CastingViewModel viewModel)
        {
            var entity = await Get(c => c.Id == viewModel.Id && c.CustomerId.Equals(viewModel.CustomerId))
                .FirstOrDefaultAsync();
            if (entity == null || entity.Status == 2)
            {
                return null;
            }
            // Update draft casting
            if (viewModel.Name != null
                && viewModel.Description != null
                && viewModel.Salary != null)
            {
                entity.Name = viewModel.Name;
                entity.Description = viewModel.Description;
                entity.MonopolisticTime = viewModel.MonopolisticTime;
                entity.Salary = viewModel.Salary;
            }
            else if (viewModel.OpenTime != null
                && viewModel.CloseTime != null
                && viewModel.Status != null)
            {
                var openTime = (DateTime)viewModel.OpenTime;
                openTime = openTime
                    .AddSeconds(-openTime.Second)
                    .AddMilliseconds(-openTime.Millisecond);
                var closeTime = (DateTime)viewModel.CloseTime;
                closeTime = closeTime
                    .AddSeconds(-closeTime.Second)
                    .AddMilliseconds(-closeTime.Millisecond);
                // Publish casting
                if (!entity.OpenTime.HasValue)
                {
                    entity.OpenTime = openTime;
                    entity.CloseTime = closeTime;
                    entity.Status = viewModel.Status;
                }
                else
                {
                    // Update publish casting
                    var current = DateTime.UtcNow.AddMinutes(5);
                    if (DateTime.Compare((DateTime)entity.OpenTime, current) > 0)
                    {
                        entity.OpenTime = openTime;
                        entity.CloseTime = closeTime;
                        entity.Status = viewModel.Status;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
            await UpdateAsync(entity);
            return _mapper.Map<CastingViewModel>(entity);
        }

        public async Task<CastingViewModel> Stop(int id, string customerId)
        {
            var entity = await Get(c => c.Id == id && c.CustomerId.Equals(customerId))
                .FirstOrDefaultAsync();
            if (entity == null || entity.Status == 2)
            {
                return null;
            }
            if (entity.CloseTime.HasValue
                && DateTime.Compare((DateTime)entity.CloseTime, DateTime.UtcNow) > 0)
            {
                entity.CloseTime = DateTime.UtcNow;
            }
            else
            {
                return null;
            }
            await UpdateAsync(entity);
            return _mapper.Map<CastingViewModel>(entity);
        }

        public async Task<CastingViewModel> Delete(int id, string customerId)
        {
            var entity = await Get(c => c.Id == id && c.CustomerId.Equals(customerId))
                .FirstOrDefaultAsync();
            if (entity == null || entity.Status == 2)
            {
                return null;
            }
            if (entity.OpenTime.HasValue && entity.CloseTime.HasValue)
            {
                var current = DateTime.UtcNow;
                if (DateTime.Compare((DateTime)entity.OpenTime, current) <= 0
                    && DateTime.Compare((DateTime)entity.CloseTime, current) >= 0)
                {
                    return null;
                }
            }
            entity.Status = 2;
            await UpdateAsync(entity);
            return _mapper.Map<CastingViewModel>(entity);
        }

        public DateTime GetRecentSchedule()
        {
            var current = DateTime.UtcNow;
            current = current.AddSeconds(-current.Second).AddMilliseconds(-current.Millisecond);
            var castings = _castingRepository
                .Get(x => x.OpenTime != null
                    && x.CloseTime != null
                    && ((DateTime.Compare((DateTime)x.OpenTime, current) > 0)
                        || (DateTime.Compare((DateTime)x.CloseTime, current) > 0)
                    )
                ).OrderBy(x => x.OpenTime)
                .ToList();
            if (castings == null || castings.Count == 0)
            {
                return DateTime.UtcNow;
            }
            var recentOpenTime = (DateTime)castings[0].OpenTime;
            castings.Sort((x, y) => DateTime.Compare((DateTime)x.CloseTime, (DateTime)y.CloseTime));
            var recentCloseTime = (DateTime)castings[0].CloseTime;
            return DateTime.Compare(recentOpenTime, recentCloseTime) <= 0
                ? recentOpenTime
                : recentCloseTime;
        }

        public async Task<List<int>> GetIdList(string customerId)
        {
            var listCasting = await _castingRepository
                .Get(x => x.CustomerId == customerId && x.Status == 1)
                .ToListAsync();
            var ids = new List<int>();
            foreach (var casting in listCasting)
            {
                ids.Add(casting.Id);
            }
            return ids;
        }
    }
}
