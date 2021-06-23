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
        Task<List<ShowCasting>> GetCastingsModelById(string modelId);
        Task<List<ShowOffer>> GetOffersModelById(string modelId);

/*        CompleteModel GetCompleteModelsById(string modelId);
        GetModelViewModel GetCompleteModelByGmail(string gmail);
*/
    }
    public partial class ModelService : BaseService<Model>, IModelService
    {
        private readonly IMapper _mapper;
        private readonly IApplyCastingRepository _applyCastingRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IModelStyleRepository _modelStyleRepository;
        private readonly IStyleRepository _styleRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ISubscribeCastingRepository _subscribeCastingRepository;
        private readonly ICastingRepository _castingRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IModelOfferRepository _modelOfferRepository;
        public ModelService(IModelRepository modelRepository, IMapper mapper, ICastingRepository castingRepository, IModelOfferRepository modelOfferRepository, IOfferRepository offerRepository,ISubscribeCastingRepository subscribeCastingRepository, IStyleRepository styleRepository, IImageRepository imageRepository,
        IApplyCastingRepository applyCastingRepository, IModelStyleRepository modelStyleRepository) : base(modelRepository)
        {
            _mapper = mapper;
            _applyCastingRepository = applyCastingRepository;
            _modelRepository = modelRepository;
            _modelStyleRepository = modelStyleRepository;
            _styleRepository = styleRepository;
            _imageRepository = imageRepository;
            _subscribeCastingRepository = subscribeCastingRepository;
            _castingRepository = castingRepository;
            _offerRepository = offerRepository;
            _modelOfferRepository = modelOfferRepository;
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

        public async Task<List<ShowOffer>> GetOffersModelById(string modelId)
        {
            var listModelOffer = await _modelOfferRepository.Get(x => x.ModelId == modelId).ToListAsync();
            List<ShowOffer> lc = new List<ShowOffer>();
            foreach (var item in listModelOffer)
            {
                var offer = await _offerRepository.FirstOrDefaultAsyn(x => x.Id == item.OfferId);
                var des = _mapper.Map<ShowOffer>(offer);
                des.OfferStatus = item.Status;
                lc.Add(des);
            }
            return lc;
        }

        public async Task<List<ShowCasting>> GetCastingsModelById(string modelId)
        {
            var listSupCasting = await _subscribeCastingRepository.Get(x => x.ModelId == modelId).ToListAsync();
            List<ShowCasting> lc = new List<ShowCasting>();
            foreach (var item in listSupCasting)
            {
                var casting = await _castingRepository.FirstOrDefaultAsyn(x => x.Id == item.CastingId);
                var des = _mapper.Map<ShowCasting>(casting);
                lc.Add(des);
            }
            return lc;
        }

        public ShowCasting CopyAToB(Casting a)
        {
            ShowCasting b = new ShowCasting();
            // copy fields
            var typeOfA = a.GetType();
            var typeOfB = b.GetType();
            foreach (var fieldOfA in typeOfA.GetFields())
            {
                var fieldOfB = typeOfB.GetField(fieldOfA.Name);
                fieldOfB.SetValue(b, fieldOfA.GetValue(a));
            }
            // copy properties
            foreach (var propertyOfA in typeOfA.GetProperties())
            {
                var propertyOfB = typeOfB.GetProperty(propertyOfA.Name);
                propertyOfB.SetValue(b, propertyOfA.GetValue(a));
            }

            return b;
        }

        //ok
        public GetModelViewModel GetCompleteModelByGmail(string gmail)
        {
            var model =  _modelRepository.FirstOrDefault(x => x.Gmail == gmail);
            if (model == null)
            {
                return null;
            }
            GetModelViewModel dto = new GetModelViewModel {
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
                Weight = model.Weight
            };
            var styles = _modelStyleRepository.Get(x => x.ModelId == model.Id).ToList();
            for (int i = 0; i < styles.Count; i++)
            {
                var style = _styleRepository.FirstOrDefault(x => x.Id == styles[i].StyleId);
                dto.Styles.Add(new GetModelStyleViewModel { Id = style.Id, Name = style.Name });
            }
            var images = _imageRepository.Get(x => x.ModelId == model.Id).ToList();
            for (int i = 0; i < images.Count; i++)
            {
                var image = _imageRepository.FirstOrDefault(x => x.Id == images[i].Id);
                dto.Images.Add(new GetModelImageViewModel { Id = image.Id, Link = image.Link });
            }
            return dto;
        }

        //ok
        /*public CompleteModel GetCompleteModelsById(string modelId)
        {
            return null;
            //var model = _modelRepository.FirstOrDefault(x => x.Id == modelId);
            //var styles = _modelStyleRepository.Get(x => x.ModelId == modelId).ToList();
            //List<(int Id, string Name)> listStyle = new List<(int Id, string Name)>();
            //List<(int Id, string Name)> listImage = new List<(int Id, string Name)>();
            //for (int i = 0; i < styles.Count; i++)
            //{
            //    var style = _styleRepository.FirstOrDefault(x => x.Id == styles[i].StyleId);
            //    (int Id, string Name) styless = (style.Id, style.Name);
            //    listStyle.Add(styless);
            //}
            //var images = _imageRepository.Get(x => x.ModelId == model.Id).ToList();

            for (int i = 0; i < images.Count; i++)
            {
                var image = _styleRepository.FirstOrDefault(x => x.Id == images[i].Id);
                Tuple<int, string> imagess = new Tuple<int, string>(image.Id, image.Name);
                listImage.Add(imagess);
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
                Styles = listStyle,
                Images = listImage
            };
            return cm;
        }
        */
    }
}
