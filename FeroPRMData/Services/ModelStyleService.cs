using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;

namespace FeroPRMData.Services
{
    public partial interface IModelStyleService:IBaseService<ModelStyle>
    {
    }
    public partial class ModelStyleService:BaseService<ModelStyle>,IModelStyleService
    {
        public ModelStyleService(IModelStyleRepository repository):base(repository){}
    }
}
