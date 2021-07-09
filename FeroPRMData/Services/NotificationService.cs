using FeroPRMData.Commons;
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
        Task<List<Notification>> Get(string userId);
        Task<Notification> UpdateStatus(int notiId, int status);
        Task Add(Notification notification);
        Task ScanCasting();
        Task ComposeReceiveOfferNotification(int offerId, string modelId, string customerId);
        Task ComposeAcceptOfferNotification(int offerId, string modelId, string customerId);
    }
    public partial class NotificationService : BaseService<Notification>, INotificationService
    {
        public static readonly int IDLE_MINUTES = 4;
        private readonly string TOPIC_PREFIX = "CASTING_ID_";
        private readonly INotificationRepository _notificationRepository;
        private readonly ICastingRepository _castingRepository;
        private readonly ISubscribeCastingRepository _subscribeCastingRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICustomerRepository _customerRepository;

        public NotificationService(INotificationRepository notificationRepository,
            ICastingRepository castingRepository, ISubscribeCastingRepository subscribeCastingRepository,
            IModelRepository modelRepository, ICustomerRepository customerRepository):base(notificationRepository)
        {
            _notificationRepository = notificationRepository;
            _castingRepository = castingRepository;
            _subscribeCastingRepository = subscribeCastingRepository;
            _modelRepository = modelRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<Notification>> Get(string userId)
          {
              var listNoti = await _notificationRepository
                  .Get(x => x.UserId == userId)
                  .OrderByDescending(x => x.Time)
                  .ToListAsync();
              return listNoti;
          }

          public async Task<Notification> UpdateStatus(int notificationId, int status)
          {
              var notification = await _notificationRepository.FirstOrDefaultAsyn(x => x.Id == notificationId);
              notification.Status = status;
              await UpdateAsync(notification);
              return notification;
          }

          public async Task Add(Notification notification)
          {
              await _notificationRepository.CreateAsyn(notification);
          }

          public async Task ScanCasting()
          {
              var current = DateTime.UtcNow;
              current = current.AddSeconds(-current.Second).AddMilliseconds(-current.Millisecond);
              var next = current.AddMinutes(IDLE_MINUTES);
              var castings = await _castingRepository
                  .Get(c => ((c.OpenTime != null
                      && DateTime.Compare((DateTime)c.OpenTime, current) > 0
                      && DateTime.Compare((DateTime)c.OpenTime, next) <= 0)
                      || (c.CloseTime != null
                      && DateTime.Compare((DateTime)c.CloseTime, current) > 0
                      && DateTime.Compare((DateTime)c.CloseTime, next) <= 0)
                      ) && c.Status == 1
                  ).ToListAsync();
              foreach (var casting in castings)
              {
                  bool isOpenNotification = DateTime.Compare((DateTime)casting.OpenTime, current) > 0
                      && DateTime.Compare((DateTime)casting.OpenTime, next) <= 0;
                  string verd = isOpenNotification ? "open" : "close";
                  DateTime time = isOpenNotification ? (DateTime)casting.OpenTime : (DateTime)casting.CloseTime;
                  TimeSpan period = time.Subtract(current);
                  int minute = period.Minutes + 1;
                  string periodStr = minute > 1 ? minute + " minutes." : minute + " minute.";
                  string title = "News From Casting '" + casting.Name + "'";
                  string body = "The casting will " + verd + " in " + periodStr + " Don't miss out!";
                  SendToTopic(TOPIC_PREFIX + casting.Id, title, body);
                  string description = "The casting " + casting.Name + " will " + verd + " in " + periodStr;
                  var subscribeCastings = await _subscribeCastingRepository
                      .Get(x => x.CastingId == casting.Id)
                      .ToListAsync();
                  foreach (var subscribe in subscribeCastings)
                  {
                      await Add(new Notification()
                      {
                          Description = description,
                          LinkObjectType = LINK_OBJECT_TYPE.CASTING,
                          LinkObjectId = casting.Id,
                          UserId = subscribe.ModelId,
                          Time = current,
                          Status = 0
                      });
                  }
                  await Add(new Notification()
                  {
                      Description = description,
                      LinkObjectType = LINK_OBJECT_TYPE.CASTING,
                      LinkObjectId = casting.Id,
                      UserId = casting.CustomerId,
                      Time = current,
                      Status = 0
                  });
              }
          }

          private async void SendToTopic(string topic, string title, string body)
          {
              var message = new FirebaseAdmin.Messaging.Message()
              {
                  Notification = new FirebaseAdmin.Messaging.Notification()
                  {
                      Title = title,
                      Body = body
                  },
                  Topic = topic
              };
              await FirebaseAdmin.Messaging.FirebaseMessaging.DefaultInstance.SendAsync(message);
          }

          private async void SendToSpecificUser(string token, string title, string body)
          {
              if (token == null)
              {
                  return;
              }
              var message = new FirebaseAdmin.Messaging.Message()
              {
                  Notification = new FirebaseAdmin.Messaging.Notification()
                  {
                      Title = title,
                      Body = body
                  },
                  Token = token
              };
              await FirebaseAdmin.Messaging.FirebaseMessaging.DefaultInstance.SendAsync(message);
          }

          public async Task ComposeReceiveOfferNotification(int offerId, string modelId, string customerId)
          {
              var customer = await _customerRepository.FirstOrDefaultAsyn(x => x.Id.Equals(customerId));
              string title = "New Offer For You";
              string body = customer.Name + " has sent an offer to you. Let check it out!";
              await Add(new Notification()
              {
                  Description = body,
                  LinkObjectType = LINK_OBJECT_TYPE.OFFER,
                  LinkObjectId = offerId,
                  UserId = modelId,
                  Time = DateTime.UtcNow,
                  Status = 0
              });
              var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id.Equals(modelId));
              SendToSpecificUser(model.DeviceToken, title, body);
          }

          public async Task ComposeAcceptOfferNotification(int offerId, string modelId, string customerId)
          {
              string title = "News About Your Offer";
              var model = await _modelRepository.FirstOrDefaultAsyn(x => x.Id.Equals(modelId));
              string body = model.Name + " has accepted your offer. Let check it out!";
              await Add(new Notification() { Description = body, LinkObjectType = LINK_OBJECT_TYPE.OFFER, LinkObjectId = offerId, UserId = customerId, Time = DateTime.UtcNow, Status = 0 });
              var customer = await _customerRepository.FirstOrDefaultAsyn(x => x.Id.Equals(customerId));
              SendToSpecificUser(customer.DeviceToken, title, body);
          }
    }

}
