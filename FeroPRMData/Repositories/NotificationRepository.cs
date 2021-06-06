using FeroPRMData.Models;
using FeroPRMData.Repository.Base;
using Microsoft.EntityFrameworkCore;
namespace FeroPRMData.Repositories
{
    public partial interface INotificationRepository :IBaseRepository<Notification>
    {
    }
    public partial class NotificationRepository :BaseRepository<Notification>, INotificationRepository
    {
         public NotificationRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

