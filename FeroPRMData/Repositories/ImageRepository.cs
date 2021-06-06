using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface IImageRepository :IBaseRepository<Image>
    {
    }
    public partial class ImageRepository :BaseRepository<Image>, IImageRepository
    {
         public ImageRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

