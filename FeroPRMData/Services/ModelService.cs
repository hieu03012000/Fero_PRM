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
        bool CheckModelGmail(string gmail);
        Task<Model> CreateModel(Model model);
        GetModelViewModel GetCompleteModelByGmail(string gmail);
        GetModelViewModel GetCompleteModelById(string id, string customerId);
        Task<List<GetGeneralModelViewModel>> SearchListModel(string location, int? gender, double? minW, double? maxW, double? minH, double? maxH);
        List<GetGeneralModelViewModel> GetNew();
    }
    public partial class ModelService : BaseService<Model>, IModelService
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;
        private readonly IModelStyleRepository _modelStyleRepository;
        private readonly IStyleRepository _styleRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IFavoriteModelRepository _favoriteModelRepository;

        public ModelService(IMapper mapper, IModelRepository modelRepository, 
            IModelStyleRepository modelStyleRepository, IStyleRepository styleRepository,
            IImageRepository imageRepository, IFavoriteModelRepository favoriteModelRepository):base(modelRepository)
        {
            _mapper = mapper;
            _modelRepository = modelRepository;
            _modelStyleRepository = modelStyleRepository;
            _styleRepository = styleRepository;
            _imageRepository = imageRepository;
            _favoriteModelRepository = favoriteModelRepository;
        }

        private string GetModelId()
        {
            var model = Get().OrderByDescending(m => m.Id).FirstOrDefault();
            int num;
            if (model == null) num = 0;
            else num = int.Parse(model.Id.Substring(2));
            return "MD" + string.Format("{0 :D4}", ++num);
        }

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

        public async Task<Model> CreateModel(Model model)
        {
            if (await FirstOrDefaultAsyn(m => m.Gmail == model.Gmail) != null) return null;
            model.Id = GetModelId();
            await _modelRepository.CreateAsyn(model);
            return model;
        }

        public GetModelViewModel GetCompleteModelByGmail(string gmail)
        {
            var model = _modelRepository.FirstOrDefault(x => x.Gmail.Equals(gmail));
            if (model == null)
            {
                return null;
            }
            var dto = _mapper.Map<GetModelViewModel>(model);
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
        
        private double GetMaxWeight()
        {
            var model = _modelRepository.Get().OrderByDescending(x => x.Weight).FirstOrDefault();
            Console.WriteLine(model.Weight);
            return (double)model.Weight;

        }

        private double GetMaxHeight()
        {
            var model = _modelRepository.Get().OrderByDescending(x => x.Height).FirstOrDefault();
            Console.WriteLine(model.Height);
            return (double)model.Height;

        }

        public async Task<List<GetGeneralModelViewModel>> SearchListModel(string location, int? gender, double? minW, double? maxW, double? minH, double? maxH)
        {
            if (minW == null)
            {
                minW = 0;
            }
            if (minH == null)
            {
                minH = 0;
            }
            if (maxH == null)
            {
                maxH = GetMaxHeight();
            }
            if (maxW == null)
            {
                maxW = GetMaxWeight();
            }
            if (location == null)
            {
                location = "";
            }
            if(gender == null)
            {
                var listModel = await _modelRepository
                    .Get(x => x.Address.Contains(location)
                        && x.Weight >= minW && x.Weight <= maxW 
                        && x.Height >= minH && x.Height <= maxH)
                    .ProjectTo<GetGeneralModelViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                foreach (var item in listModel)
                {
                    List<GetStyleViewModel> ls = new List<GetStyleViewModel>();
                    var styleIds = await _modelStyleRepository
                        .Get(x => x.ModelId == item.Id)
                        .ToListAsync();
                    foreach (var styleId in styleIds)
                    {
                        var style = await _styleRepository.FirstOrDefaultAsyn(x => x.Id == styleId.StyleId);
                        ls.Add(new GetStyleViewModel { Id = style.Id, Name = style.Name }) ;
                    }
                    item.Styles = ls;
                }
                return listModel;
            }
            else
            {
                var listModel = await _modelRepository
                    .Get(x => x.Address.Contains(location)
                        && x.Weight >= minW && x.Weight <= maxW
                        && x.Height >= minH && x.Height <= maxH
                        && x.Gender == gender)
                    .ProjectTo<GetGeneralModelViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                foreach (var item in listModel)
                {
                    List<GetStyleViewModel> ls = new List<GetStyleViewModel>();
                    var styleIds = await _modelStyleRepository
                        .Get(x => x.ModelId == item.Id)
                        .ToListAsync();
                    foreach (var styleId in styleIds)
                    {
                        var style = await _styleRepository.FirstOrDefaultAsyn(x => x.Id == styleId.StyleId);
                        ls.Add(new GetStyleViewModel { Id = style.Id, Name = style.Name });
                    }
                    item.Styles = ls;
                }
                return listModel;
            }
        }

        public GetModelViewModel GetCompleteModelById(string id, string customerId)
        {
            var model = _modelRepository.FirstOrDefault(x => x.Id.Equals(id));
            if (model == null)
            {
                return null;
            }
            var dto = _mapper.Map<GetModelViewModel>(model);
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
            var favorite = _favoriteModelRepository.FirstOrDefault(x => x.ModelId.Equals(id) && x.CustomerId.Equals(customerId));
            dto.IsFavorite = favorite != null;
            return dto;
        }

        public List<GetGeneralModelViewModel> GetNew()
        {
            var models = _modelRepository.Get()
                .ProjectTo<GetGeneralModelViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            return models.TakeLast(10).Reverse().ToList();
        }

    }
}
