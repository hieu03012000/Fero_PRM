using FeroPRMData.Models;
using FeroPRMData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        //[HttpGet]
        //public IActionResult Gets()
        //{
        //    return Ok(_notificationService.Get().ToList());
        //}
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetById(int id)
        //{
        //    return Ok(_notificationService.Get(id));
        //}
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Create(Notification entity)
        //{
        //    _notificationService.Create(entity);
        //    return  CreatedAtAction(nameof(GetById), new { id = entity}, entity);
        //}
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(int id,Notification entity)
        //{
        //    _notificationService.UpdateAsync(entity);
        //    return Ok();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id,Notification entity)
        //{
        //    _notificationService.DeleteAsync(entity);
        //    return Ok();
        //}
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_notificationService.Count());
        //}
    }
}
