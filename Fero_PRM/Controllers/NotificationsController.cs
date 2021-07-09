using FeroPRMData.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public partial class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService){
            _notificationService=notificationService;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserNoti(string id)
        {
            return Ok(await _notificationService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, int status)
        {
            return Ok(await _notificationService.UpdateStatus(id, status));
        }
     
    }
}
