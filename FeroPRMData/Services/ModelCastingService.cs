using AutoMapper;
using Fero.Data.ViewModels;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface IModelCastingService : IBaseService<ModelCasting>
    {
        #region hdev
        Task<AcceptCastingViewModel> AcceptCastingCall(AcceptCastingViewModel subCasting);
        Task<AcceptCastingViewModel> DenyCastingCall(AcceptCastingViewModel subCasting);
        #endregion
    }
    public partial class ModelCastingService : BaseService<ModelCasting>, IModelCastingService
    {
        private readonly IMapper _mapper;
        public ModelCastingService(IModelCastingRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        #region hdev
        public async Task<AcceptCastingViewModel> AcceptCastingCall(AcceptCastingViewModel subCasting)
        {
            var apply = await Get(ac => ac.CastingId == subCasting.CastingId && ac.ModelId == subCasting.ModelId)
                .FirstOrDefaultAsync();
            if (apply == null) return null;
            apply.Status = 1;
            await CreateAsyn(apply);
            return subCasting;
        }

        public async Task<AcceptCastingViewModel> DenyCastingCall(AcceptCastingViewModel subCasting)
        {
            var apply = await Get(ac => ac.CastingId == subCasting.CastingId && ac.ModelId == subCasting.ModelId)
               .FirstOrDefaultAsync();
            if (apply != null) return null;
            apply.Status = 2;
            await CreateAsyn(apply);
            return subCasting;
        }
        #endregion
    }
}
