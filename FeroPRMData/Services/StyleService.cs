using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;

namespace FeroPRMData.Services
{
    public partial interface IStyleService:IBaseService<Style>
    {
    }
    public partial class StyleService:BaseService<Style>,IStyleService
    {
        public StyleService(IStyleRepository repository):base(repository){}
    }
}
