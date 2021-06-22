using AutoMapper;
using AutoMapper.QueryableExtensions;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface IModelService : IBaseService<Model>
    {
        Task<CreateModelAccountViewModel> CreateModelAccount(CreateModelAccountViewModel model);
        //Task<ModelDetailViewModel> GetModelById(string modelId);
        Task<IQueryable<ApplicantListViewModel>> GetApplicantList(int castingID);
        bool CheckModelGmail(string modelId, string gmail);
        bool CheckModelGmail(string gmail);
        Task<Model> GetModelByGmail(string gmail);
        Task<Model> GetModelsById(string modelId);
        Task<Model> CreateModel(Model model);
        CompleteModel GetCompleteModelsById(string modelId);
        CompleteModel GetCompleteModelByGmail(string gmail);

    }
    public partial class ModelService : BaseService<Model>, IModelService
    {
        private readonly IMapper _mapper;
        private readonly IApplyCastingRepository _applyCastingRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IModelStyleRepository _modelStyleRepository;
        private readonly IStyleRepository _styleRepository;
        public ModelService(IModelRepository modelRepository, IMapper mapper, IStyleRepository styleRepository,
            IApplyCastingRepository applyCastingRepository, IModelStyleRepository modelStyleRepository) : base(modelRepository)
        {
            _mapper = mapper;
            _applyCastingRepository = applyCastingRepository;
            _modelRepository = modelRepository;
            _modelStyleRepository = modelStyleRepository;
            _styleRepository = styleRepository;
        }
        private string GetModelId()
        {
            var model = Get().OrderByDescending(m => m.Id).FirstOrDefault();
            int num;
            if (model == null) num = 0;
            else num = int.Parse(model.Id.Substring(2));
            return "MD" + string.Format("{0 :D4}", ++num);
        }

        public async Task<CreateModelAccountViewModel> CreateModelAccount(CreateModelAccountViewModel model)
        {
            if (await FirstOrDefaultAsyn(m => m.Gmail == model.Gmail) != null)
                return null;
            var entity = _mapper.Map<Model>(model);
            entity.Id = GetModelId();
            await CreateAsyn(entity);
            return model;
        }

      /*  public async Task<ModelDetailViewModel> GetModelById(string modelId)
        {
            var model = await Get(x => x.Id == modelId && x.Status != 0).ProjectTo<ModelDetailViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return model;
        }
*/
        public async Task<IQueryable<ApplicantListViewModel>> GetApplicantList(int castingID)
        {
            if (await _applyCastingRepository.Get(ac => ac.CastingId == castingID).FirstOrDefaultAsync() == null)
                return null;
            var modelIdList = _applyCastingRepository.Get(ac => ac.CastingId == castingID).Select(ac => ac.ModelId);
            var modelList = Get(m => modelIdList.Contains(m.Id)).ProjectTo<ApplicantListViewModel>(_mapper.ConfigurationProvider);
            return modelList;
        }



        //ok
        public bool CheckModelGmail(string gmail)
        {
            var model = _modelRepository.FirstOrDefault(x => x.Gmail == gmail);
            if (model != null )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //ok
        public bool CheckModelGmail(string modelId, string gmail) 
        {
            var model = _modelRepository.FirstOrDefault(x => x.Id == modelId);
            if(model.Gmail == null || model.Gmail != gmail)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //ok
        public async Task<Model> GetModelByGmail(string gmail)
        {
            return await _modelRepository.FirstOrDefaultAsyn(x => x.Gmail == gmail);
        }

        //ok
        public async Task<Model> GetModelsById(string modelId)
        {
            return await _modelRepository.FirstOrDefaultAsyn(x => x.Id == modelId); 
        }

        //ok
        public async Task<Model> CreateModel(Model model)
        {
            if (await FirstOrDefaultAsyn(m => m.Gmail == model.Gmail) != null) return null;
            /*           var entity = _mapper.Map<Model>(model);
                       entity.Id = GetModelId();
                       await CreateAsyn(entity);*/
            model.Id = GetModelId();
            await _modelRepository.CreateAsyn(model);
            return model;
        }


        //ok
        public CompleteModel GetCompleteModelByGmail(string gmail)
        {
           var model =  _modelRepository.FirstOrDefault(x => x.Gmail == gmail);
            var styles = _modelStyleRepository.Get(x => x.ModelId == model.Id).ToList();
            List<int> li = new List<int>();
            List<string> ls = new List<string>();
            for (int i = 0; i < styles.Count; i++)
            {
                var style = _styleRepository.FirstOrDefault(x => x.Id == styles[i].StyleId);
                li.Add(style.Id);
                ls.Add(style.Name);
            }
            CompleteModel cm = new CompleteModel
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Avatar = model.Avatar,
                Bust = model.Bust,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Gmail = model.Gmail,
                Height = model.Height,
                Hip = model.Hip,
                Phone = model.Phone,
                SocialNetworkLink = model.SocialNetworkLink,
                Status = model.Status,
                Waist = model.Waist,
                Weight = model.Weight,
                StyleId = li,
                StyleName = ls
            };
            return cm;
        }

        //ok
        public CompleteModel GetCompleteModelsById(string modelId)
        {
            var styles = _modelStyleRepository.Get(x => x.ModelId == modelId).ToList();
            List<int> li = new List<int>();
            List<string> ls = new List<string>();
            for (int i = 0; i < styles.Count; i++)
            {
                var style = _styleRepository.FirstOrDefault(x => x.Id == styles[i].StyleId);
                li.Add(style.Id);
                ls.Add(style.Name);
            }
            var model = _modelRepository.FirstOrDefault(x => x.Id == modelId);
            CompleteModel cm = new CompleteModel
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Avatar = model.Avatar,
                Bust = model.Bust,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Gmail = model.Gmail,
                Height = model.Height,
                Hip = model.Hip,
                Phone = model.Phone,
                SocialNetworkLink = model.SocialNetworkLink,
                Status = model.Status,
                Waist = model.Waist,
                Weight = model.Weight,
                StyleId = li,
                StyleName = ls
            };
            return cm;
        }
    }
}
