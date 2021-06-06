using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;

namespace FeroPRMData.Services
{
    public partial interface IImageService:IBaseService<Image>
    {
    }
    public partial class ImageService:BaseService<Image>,IImageService
    {
        public ImageService(IImageRepository repository):base(repository){}
    }
}
