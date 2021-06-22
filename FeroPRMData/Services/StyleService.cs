using AutoMapper;
using AutoMapper.QueryableExtensions;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using FeroPRMData.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface IStyleService:IBaseService<Style>
    {
        Task<IQueryable<GetAllStyleViewModel>> GetAllStyle();
    }
    public partial class StyleService:BaseService<Style>,IStyleService
    {
        private readonly IMapper _mapper;
        private readonly IStyleRepository _styleRepository;
        public StyleService(IMapper mapper,IStyleRepository styleRepository) :base(styleRepository)
        {
            _mapper = mapper;
            _styleRepository = styleRepository;
        }

        public async Task<IQueryable<GetAllStyleViewModel>> GetAllStyle()
        {
            if (await Get().FirstOrDefaultAsync() == null)
                return null;
            var styleList = Get().ProjectTo<GetAllStyleViewModel>(_mapper.ConfigurationProvider);
            return styleList;
        }
    }
}
