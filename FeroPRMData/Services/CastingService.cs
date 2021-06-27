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
        Task<DetailCastingViewModel> GetCastingById(int castingId);
        Task<List<SearchCastingViewModel>> GetListCasting();
        Task<List<SearchCastingViewModel>> SearchListCasting(string search, double? min, double? max);
        Task<Casting> CreateCasting(Casting casting);
        Task<Casting> CreateCasting(string customerId, Casting casting);
        Task<Casting> DeleteCasting(int castingId);
        Task<List<ListModelCasting>> GetModelsByCastingId(int castingId);
        Task<Casting> UpdateCasting(int id, ShowCasting updateCasting);
    }
    public partial class CastingService : BaseService<Casting>, ICastingService
    {
        private readonly IMapper _mapper;
        private readonly ICastingRepository _castingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IApplyCastingRepository _applyCastingRepository;
        private readonly IModelRepository _modelRepository;

        public CastingService(ICastingRepository castingRepository, ICustomerRepository customerRepository, IModelRepository modelRepository, IApplyCastingRepository applyCastingRepository, IMapper mapper) : base(castingRepository)
        {
            _mapper = mapper;
            _castingRepository = castingRepository;
            _customerRepository = customerRepository;
            _applyCastingRepository = applyCastingRepository;
            _modelRepository = modelRepository;
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

        public async Task<DetailCastingViewModel> GetCastingById(int castingId)
        {
            var casting = await Get(c => c.Id == castingId && c.Status != 2)
                .ProjectTo<DetailCastingViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return casting;
        }

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
        public async Task<Casting> CreateCasting(Casting casting)
        {
            var cus = await _customerRepository.FirstOrDefaultAsyn(x => x.Id == casting.CustomerId);
            if(cus == null)
            {
                return null;
            }
            casting.CreateTime = DateTime.Now;
            await _castingRepository.CreateAsyn(casting);
            return casting;
        }

        //them create at 
        public async Task<Casting> CreateCasting(string customerId, Casting casting)
        {
            var cus = await _customerRepository.FirstOrDefaultAsyn(x => x.Id == customerId);
            if (cus == null)
            {
                return null;
            }
            casting.CustomerId = customerId;
            casting.CreateTime = DateTime.Now;
            await _castingRepository.CreateAsyn(casting);
            return casting;
        }

        public async Task<Casting> GetCastingByCusId(string customerId)
        {
            return await _castingRepository.FirstOrDefaultAsyn(x => x.CustomerId == customerId);
        }

        public async Task<List<Casting>> GetListCasting(string cusId)
        {
            var listCasting = await _castingRepository.Get(x => x.CustomerId == cusId && (x.Status == 0 || x.Status == 1)).ToListAsync();
            listCasting.Sort((x, y) => DateTime.Compare((DateTime)x.CreateTime, (DateTime)y.CreateTime));
            var newList = listCasting.Skip(Math.Max(0, listCasting.Count() - 10)).ToList();
            return newList;
        }

        public async Task<List<SearchCastingViewModel>> GetListCasting()
        {
            var listCasting = await _castingRepository.Get().ToListAsync();
            listCasting.Sort((x, y) => DateTime.Compare((DateTime)y.CreateTime, (DateTime)x.CreateTime));
            var result = new List<SearchCastingViewModel>();
            for (int i = 0; i < Math.Min(listCasting.Count, 10); i++)
            {
                var casting = listCasting[i];
                if (casting.Status != 1)
                {
                    listCasting.RemoveAt(i);
                    i--;
                } else
                {
                    var customer = _customerRepository.FirstOrDefault(c => c.Id.Equals(listCasting[i].CustomerId));
                    result.Add(new SearchCastingViewModel
                    {
                        Id = casting.Id,
                        Name = casting.Name,
                        Description = casting.Description,
                        Salary = casting.Salary,
                        OpenTime = casting.OpenTime,
                        CloseTime = casting.CloseTime,
                        CustomerName = customer.Name
                    });
                }
            }
            return result;

        }

        private double GetMaxSalary()
        {
            var casting =  _castingRepository.Get().OrderByDescending(x => x.Salary).FirstOrDefault();
            return (double)casting.Salary;
            
        }
        
        //nguoi mau search casting theo substr casting name, min salary, max salary ( casting - status  published, opened, and closed)
        public async Task<List<SearchCastingViewModel>> SearchListCasting(string search, double? min, double? max)
        {
            if(min == null)
            {
                min = 0;
            }
            if(max == null)
            {
                max = GetMaxSalary();
            }
            if(search == null)
            {
                search = "";
            }
            var listCasting = await _castingRepository.Get(x => x.Name.Contains(search) && x.Salary >= min && x.Salary <= max  && x.Status == 1).ToListAsync();
            listCasting.Sort((x, y) => DateTime.Compare((DateTime)y.CreateTime, (DateTime)x.CreateTime));
            var result = new List<SearchCastingViewModel>();
            foreach (var casting in listCasting)
            {
                var customer = _customerRepository.FirstOrDefault(c => c.Id.Equals(casting.CustomerId));
                result.Add(new SearchCastingViewModel {
                    Id = casting.Id,
                    Name = casting.Name,
                    Description = casting.Description,
                    Salary = casting.Salary,
                    OpenTime = casting.OpenTime,
                    CloseTime = casting.CloseTime,
                    CustomerName = customer.Name
                });
            }
            return result;
        }

        public async Task<Casting> DeleteCasting(int castingId)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == castingId);
            casting.Status = 2;
            await UpdateAsync(casting);
            return casting;
        }

        public async Task<List<ListModelCasting>> GetModelsByCastingId(int castingId)
        {
            var listApplyCasting = await _applyCastingRepository.Get(x => x.CastingId == castingId).ToListAsync();
            List<ListModelCasting> lm = new List<ListModelCasting>();
            foreach (var item in listApplyCasting)
            {
                var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == item.ModelId);
                var des = _mapper.Map<ListModelCasting>(model);
                lm.Add(des);
            }
            return lm;
        }

        public async Task<Casting> UpdateCasting(int castingId, ShowCasting updateCasting)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == castingId);
            if (casting == null)
            {
                return null;
            }
            else
            {
                updateCasting.Id = castingId;
                updateCasting.CustomerId = casting.CustomerId;
                casting = _mapper.Map(updateCasting, casting);
                await UpdateAsync(casting);
                return casting;
            }
        }

        //ta dao
        /*public async Task<Casting> UpdateCasting(int castingId, Casting updateCasting)
        {
            var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == castingId);
            if (updateCasting.Description == null)
            {
                Description = 
            }
            if (updateCasting.Name == null)
            {
                updateCasting.Name = casting.Name
            }
            if (updateCasting.Description == null)
            {
                Description =
            }
            if (updateCasting.Description == null)
            {
                Description =
            }
            if (updateCasting.Description == null)
            {
                Description =
            }
            if (updateCasting.Description == null)
            {
                Description =
            }
            if (updateCasting.Description == null)
            {
                Description =
            }
            Casting cast = new Casting
            {

                
            }
        }*/
    }
}
