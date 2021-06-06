using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;

namespace FeroPRMData.Services
{
    public partial interface INotificationService:IBaseService<Notification>
    {
    }
    public partial class NotificationService:BaseService<Notification>,INotificationService
    {
        public NotificationService(INotificationRepository repository):base(repository){}
    }
}
