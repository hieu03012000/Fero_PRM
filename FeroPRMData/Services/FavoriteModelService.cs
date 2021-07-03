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

    }
    public partial class FavoriteModelService : BaseService<FavoriteModel>, IFavoriteModelService
    {
        private readonly IMapper _mapper;
        private readonly ISubscribeCastingRepository _subscribeCastingRepository;
        private readonly ICastingRepository _castingRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICustomerRepository _customerRepository;

        public FavoriteModelService(IMapper mapper, ISubscribeCastingRepository subscribeCastingRepository, ICastingRepository castingRepository, IModelRepository modelRepository, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _subscribeCastingRepository = subscribeCastingRepository;
            _castingRepository = castingRepository;
            _modelRepository = modelRepository;
            _customerRepository = customerRepository;
        }
    }
}
