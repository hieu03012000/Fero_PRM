using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using System;

namespace FeroPRMData.Services
{
    public partial interface INotificationService:IBaseService<Notification>
    {
    }
    public partial class NotificationService:BaseService<Notification>,INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly ICastingRepository _castingRepository;
        private readonly IModelRepository _modelRepository;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, IModelRepository modelRepository, ICastingRepository castingRepository) : base(notificationRepository)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _castingRepository = castingRepository;
            _modelRepository = modelRepository;
        }

        public async void CreateNotiCusEnd(Casting casting)
        {
            if (casting.CloseTime != null)
            {
                if (DateTime.Compare(DateTime.Now, (DateTime)casting.CloseTime) != -1)
                {
                    Notification noti = new Notification
                    {
                        UserId = casting.CustomerId,
                        Time = DateTime.Now,
                        Description = "End of Casting"
                    };
                    await CreateAsyn(noti);
                }
            }
        }
        public async void CreateNotiCusStart(Casting casting)
        {
            if (casting.OpenTime != null)
            {
                if ((DateTime.Compare(DateTime.Now, (DateTime)casting.OpenTime) != -1) || (DateTime.Now == (DateTime)casting.OpenTime))
                {
                    Notification noti = new Notification
                    {
                        UserId = casting.CustomerId,
                        Time = DateTime.Now,
                        Description = "Casting have start"
                    };
                    await CreateAsyn(noti);
                }
            }
        }

        public async void CreateNotiCusAccept(ModelOffer modelOffer)
        {
            //1 is accept
            if (modelOffer.Status == 1)
            {
                var casting = _castingRepository.FirstOrDefault(x => x.Id == modelOffer.OfferId);
                var model = _modelRepository.FirstOrDefault(x => x.Id == modelOffer.ModelId);
                Notification noti = new Notification
                {

                    UserId = casting.CustomerId,
                    Time = DateTime.Now,
                    Description = model.Name + "have accept your invitation"
                };
                await CreateAsyn(noti);
            }
        }
    }
}
