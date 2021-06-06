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
    public partial interface IModelService : IBaseService<Model>
    {
        Task<CreateModelAccountViewModel> CreateModelAccount(CreateModelAccountViewModel model);
        Task<ModelDetailViewModel> GetModelById(string modelId);
        Task<IQueryable<ApplicantListViewModel>> GetApplicantList(int castingID);
    }
    public partial class ModelService : BaseService<Model>, IModelService
    {
        private readonly IMapper _mapper;
        private readonly IApplyCastingRepository _applyCastingRepository;
        public ModelService(IModelRepository repository, IMapper mapper,
            IApplyCastingRepository applyCastingRepository) : base(repository)
        {
            _mapper = mapper;
            _applyCastingRepository = applyCastingRepository;
        }
        private string GetModelId()
        {
            var modelId = Get().OrderByDescending(m => m.Id).FirstOrDefault().Id;
            int num = int.Parse(modelId.Substring(2));
            return "MD" + string.Format("{0 :D4}", ++num);
        }

        public async Task<CreateModelAccountViewModel> CreateModelAccount(CreateModelAccountViewModel model)
        {
            if (await FirstOrDefaultAsyn(m => m.Gmail == model.Email) != null)
                return null;
            var entity = _mapper.Map<Model>(model);
            entity.Id = GetModelId();
            await CreateAsyn(entity);
            return model;
        }

        public async Task<ModelDetailViewModel> GetModelById(string modelId)
        {
            var model = await Get(x => x.Id == modelId && x.Status != 0).ProjectTo<ModelDetailViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return model;
        }

        public async Task<IQueryable<ApplicantListViewModel>> GetApplicantList(int castingID)
        {
            if (await _applyCastingRepository.Get(ac => ac.CastingId == castingID).FirstOrDefaultAsync() == null)
                return null;
            var modelIdList = _applyCastingRepository.Get(ac => ac.CastingId == castingID).Select(ac => ac.ModelId);
            var modelList = Get(m => modelIdList.Contains(m.Id)).ProjectTo<ApplicantListViewModel>(_mapper.ConfigurationProvider);
            return modelList;
        }
    }
}
