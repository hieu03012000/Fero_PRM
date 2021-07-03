using AutoMapper;
using AutoMapper.QueryableExtensions;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface IFavoriteModelService : IBaseService<FavoriteModel>
    {
        Task<FavoriteModelViewModel> Add(FavoriteModelViewModel viewModel);
        Task<FavoriteModelViewModel> Remove(FavoriteModelViewModel viewModel);
        Task<List<GetGeneralModelViewModel>> GetAll(string customerId);
    }
    public partial class FavoriteModelService : BaseService<FavoriteModel>, IFavoriteModelService
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteModelRepository _favoriteModelRepository;
        private readonly IModelRepository _modelRepository;

        public FavoriteModelService(IMapper mapper, IFavoriteModelRepository favoriteModelRepository, IModelRepository modelRepository)
        {
            _mapper = mapper;
            _favoriteModelRepository = favoriteModelRepository;
            _modelRepository = modelRepository;
        }

        public async Task<FavoriteModelViewModel> Add(FavoriteModelViewModel viewModel)
        {
            var entity = _mapper.Map<FavoriteModel>(viewModel);
            await _favoriteModelRepository.CreateAsyn(entity);
            return viewModel;
        }

        public async Task<List<GetGeneralModelViewModel>> GetAll(string customerId)
        {
            var favoriteModels = await _favoriteModelRepository.Get(x => x.CustomerId.Equals(customerId))
                .ToListAsync();
            var models = new List<GetGeneralModelViewModel>();
            foreach (var favoriteModel in favoriteModels)
            {
                var model = await _modelRepository
                    .Get(x => x.Id.Equals(favoriteModel.ModelId))
                    .ProjectTo<GetGeneralModelViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                models.Add(model);
            }
            return models;
        }

        public async Task<FavoriteModelViewModel> Remove(FavoriteModelViewModel viewModel)
        {
            var entity = await _favoriteModelRepository
                .FirstOrDefaultAsyn(x => x.ModelId.Equals(viewModel.ModelId)
                    && x.CustomerId.Equals(viewModel.CustomerId));
            if (entity == null)
            {
                return null;
            }
            await _favoriteModelRepository.DeleteAsync(entity);
            return viewModel;
        }
    }
}
