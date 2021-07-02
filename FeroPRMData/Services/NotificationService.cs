using AutoMapper;
using FeroPRMData.Models;
using FeroPRMData.Repositories;
using FeroPRMData.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeroPRMData.Services
{
    public partial interface INotificationService:IBaseService<Notification>
    {
        Task<Notification> UpdateStatus(int notiId, int status);
        Task<List<Notification>> Get(string userId);
        Task<Notification> CreateNoti(string userId, Notification noti);
        Task<Notification> CreateNoti(Notification noti);
    }
    public partial class NotificationService:BaseService<Notification>,INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly ICastingRepository _castingRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICustomerRepository _customerRepository;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, ICustomerRepository customerRepository, IModelRepository modelRepository, ICastingRepository castingRepository) : base(notificationRepository)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _castingRepository = castingRepository;
            _modelRepository = modelRepository;
            _customerRepository = customerRepository;
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

        //bi ngu moi nguoi eiii
        /*        public async Task<Notification> UpdateNoti(int notiId, Notification noti)
                {
                    var notification = await _notificationRepository.FirstOrDefaultAsyn(x => x.Id == notiId);
                    noti.Id = notification.Id;
                    noti.UserId = notification.UserId;
                    if (noti.Status == null)
                    {
                        noti.Status = notification.Status;
                    }
                    if (noti.Time == null)
                    {
                        noti.Time = notification.Time;
                    }
                    if (noti.Title == null)
                    {
                        noti.Title = notification.Title;
                    }
                    if (noti.Description == null)
                    {
                        noti.Description = notification.Description;
                    }
                    await UpdateAsync(noti);
                    return noti;
                }*/

        public async Task<Notification> UpdateStatus(int notificationId, int status)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsyn(x => x.Id == notificationId);
            notification.Status = status;
            await UpdateAsync(notification);
            return notification;
        }

        public async Task<List<Notification>> Get(string userId)
        {
            var listNoti = await _notificationRepository
                .Get(x => x.UserId == userId)
                .OrderByDescending(x => x.Time)
                .ToListAsync();
            return listNoti;
        }

        public async Task<Notification> CreateNoti(Notification noti)
        {
            var customer = await _customerRepository.FirstOrDefaultAsyn(x => x.Id == noti.UserId);
            var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id ==  noti.UserId);
            if(customer != null || model != null)
            {
                await _notificationRepository.CreateAsyn(noti);
                return noti;
            }
            else
            {
                return null;
            }
        }

        public async Task<Notification> CreateNoti(string userId, Notification noti)
        {
            var customer = await _customerRepository.FirstOrDefaultAsyn(x => x.Id == userId);
            var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id == userId);
            noti.UserId = userId;
            if (customer != null || model != null)
            {
                await _notificationRepository.CreateAsyn(noti);
                return noti;
            }
            else
            {
                return null;
            }
        }
    }
}
