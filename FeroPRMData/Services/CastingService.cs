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
    //aall
    public partial interface ICastingService : IBaseService<Casting>
    {
        #region hdev
        Task<IQueryable<NewCastingViewModel>> NewCasting();
        //Task<IQueryable<GetAllCastingViewModel>> FilterCasting(string name, decimal? min, decimal? max);
        //Task<DetailCastingViewModel> GetCastingById(int castingId);
        //Task<CreateCastingCallViewModel> CreateCasting(CreateCastingCallViewModel model);
        //Task<UpdateCastingViewModel> UpdateCasting(UpdateCastingViewModel model);
        //Task<int> PublicCasting(int castId, PublicCastingViewModel model);
        //Task<int> StopCasting(int castId);
        //Task<int> DeleteCasting(int castId);
        //Task<int> MakeOffer(MakeOfferViewModel offer);
        #endregion
        Task<List<Casting>> GetListCasting(string cusId);
        Task<Casting> GetCastingByCusId(string customerId);
        Task<Casting> GetCastingById(int castingId);
        Task<List<Casting>> GetListCasting();
        Task<List<Casting>> SearchListCasting(string search, double? min, double? max);
    }
    public partial class CastingService : BaseService<Casting>, ICastingService
    {
        private readonly IMapper _mapper;
        private readonly ICastingRepository _castingRepository;
        private readonly ICustomerRepository _customerRepository;

        public CastingService(ICastingRepository castingRepository, ICustomerRepository customerRepository, IMapper mapper) : base(castingRepository)
        {
            _mapper = mapper;
            _castingRepository = castingRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IQueryable<NewCastingViewModel>> NewCasting()
        {
            if (await Get().FirstOrDefaultAsync() == null) return null;
            var castingList = Get().OrderByDescending(c => c.CreateTime).Take(10)
                .ProjectTo<NewCastingViewModel>(_mapper.ConfigurationProvider);
            return castingList;
        }


        //#region hdev
        //private async Task<decimal?> GetMaxSalary()
        //{
        //    var maxSalary = await Get().OrderByDescending(c => c.Salary).FirstOrDefaultAsync();
        //    return maxSalary.Salary;
        //}

        //public async Task<IQueryable<GetAllCastingViewModel>> FilterCasting(string name, decimal? min, decimal? max)
        //{
        //    if (min == null) min = 0;
        //    if (max == null) max = await GetMaxSalary();
        //    if (name == null) name = "";
        //    var castingList = Get(c => c.Salary >= min && c.Salary <= max && c.Name.Contains(name) && c.Status != 0 && c.Status != 4)
        //        .ProjectTo<GetAllCastingViewModel>(_mapper.ConfigurationProvider);
        //    return castingList;
        //}

        //public async Task<DetailCastingViewModel> GetCastingById(int castingId)
        //{
        //    var casting = await Get(c => c.Id == castingId && c.Status != 4)
        //        .ProjectTo<DetailCastingViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        //    return casting;
        //}

        //public async Task<CreateCastingCallViewModel> CreateCasting(CreateCastingCallViewModel model)
        //{
        //    if (model.Status != 1 && model.Status != 0) return null;
        //    var entity = _mapper.Map<Casting>(model);
        //    await CreateAsyn(entity);
        //    return model;
        //}

        //public async Task<UpdateCastingViewModel> UpdateCasting(UpdateCastingViewModel model)
        //{
        //    if (model.Status != 0) return null;
        //    if (await Get(c => c.Id == model.Id)
        //        .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefaultAsync() == null)
        //        return null;
        //    var entity = _mapper.Map<Casting>(model);
        //    await UpdateAsync(entity);
        //    return model;
        //}

        //public async Task<int> PublicCasting(int castId, PublicCastingViewModel model)
        //{
        //    if (await Get(c => c.Id == castId)
        //        .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefaultAsync() == null)
        //        return -1;
        //    if (Get(c => c.Id == castId)
        //        .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefault().Status != 0)
        //        return -1;
        //    var entity = await Get(c => c.Id == castId).FirstOrDefaultAsync();
        //    entity.Status = 1;
        //    entity.OpenTime = model.OpenTime;
        //    entity.CloseTime = model.CloseTime;
        //    await UpdateAsync(entity);
        //    return 0;
        //}

        //public async Task<int> StopCasting(int castId)
        //{
        //    if (await Get(c => c.Id == castId)
        //        .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefaultAsync() == null)
        //        return -2;
        //    if (Get(c => c.Id == castId)
        //        .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefault().Status != 2)
        //        return -1;
        //    var entity = await Get(c => c.Id == castId).FirstOrDefaultAsync();
        //    entity.Status = 3;
        //    await UpdateAsync(entity);
        //    return 0;
        //}

        //public async Task<int> DeleteCasting(int castId)
        //{
        //    if (await Get(c => c.Id == castId)
        //        .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefaultAsync() == null)
        //        return -1;
        //    if (Get(c => c.Id == castId)
        //        .ProjectTo<UpdateCastingViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefault().Status == 2)
        //        return -1;
        //    var entity = await Get(c => c.Id == castId).FirstOrDefaultAsync();
        //    entity.Status = 4;
        //    await UpdateAsync(entity);
        //    return 0;
        //}

        //public async Task<int> MakeOffer(MakeOfferViewModel offer)
        //{
        //    var entity = _mapper.Map<Casting>(offer);
        //    entity.Status = 6;
        //    await CreateAsyn(entity);
        //    return 0;
        //}
        //#endregion

        //Tao casting vs form casting
        public async void CreateCasting(Casting casting)
        {
            await _castingRepository.CreateAsyn(casting);
        }

        //them create at 
        public async void CreateCasting(string customerId, Casting casting)
        {
            casting.CustomerId = customerId;
            await _castingRepository.CreateAsyn(casting);
        }

        public async Task<Casting> GetCastingById(int castingId)
        {
            return await _castingRepository.FirstOrDefaultAsyn(x => x.Id == castingId);
        }

        public async Task<Casting> GetCastingByCusId(string customerId)
        {
            return await _castingRepository.FirstOrDefaultAsyn(x => x.CustomerId == customerId);
        }

        public async Task<List<Casting>> GetListCasting(string cusId)
        {
            var listCasting = await _castingRepository.Get(x => x.CustomerId == cusId).ToListAsync();
            listCasting.Sort((x, y) => DateTime.Compare((DateTime)x.CreateTime, (DateTime)y.CreateTime));
            var newList = listCasting.Skip(Math.Max(0, listCasting.Count() - 10)).ToList();
            return newList;
        }

        public async Task<List<Casting>> GetListCasting()
        {
            var listCasting = await _castingRepository.Get().ToListAsync();
            listCasting.Sort((x, y) => DateTime.Compare((DateTime)x.CreateTime, (DateTime)y.CreateTime));
            foreach (var item in listCasting)
            {
                if(item.Status != 1)
                {
                    listCasting.Remove(item);
                }
            }
            if (listCasting.Count > 10)
            {
                var newList = listCasting.Skip(Math.Max(0, listCasting.Count() - 10)).ToList();
                return newList;
            }
            else{
                return listCasting;
            }
            
        }

        public double GetMaxSalary()
        {
            var casting =  _castingRepository.Get().OrderByDescending(x => x.Salary).FirstOrDefault();
            return (double)casting.Salary;
            
        }
        
        //nguoi mau search casting theo substr casting name, min salary, max salary ( casting - status  published, opened, and closed)
        public async Task<List<Casting>> SearchListCasting(string search, double? min, double? max)
        {
            if(min == null)
            {
                min = 0;
            }
            if(max == null)
            {
                max = GetMaxSalary();
            }
            var listCasting = await _castingRepository.Get(x => x.Name.Contains(search) && x.Salary >= min && x.Salary <= max  && x.Status == 1).ToListAsync();
            return listCasting;
        }
    }
}
