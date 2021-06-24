using FeroPRMData.Models;
using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/subscribe-castings")]
    public partial class SubscribeCastingsController : ControllerBase
    {
        private readonly ISubscribeCastingService _subscribeCastingService;
        public SubscribeCastingsController(ISubscribeCastingService subscribeCastingService)
        {
            _subscribeCastingService = subscribeCastingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Gets(string id)
        {
            return Ok(await _subscribeCastingService.GetSubscribeCastings(id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubCasting(SubscribeCasting subscribeCasting)
        {
            var result = await _subscribeCastingService.DeleteSubscribeCasting(subscribeCasting);
            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckSubCastingId(SubscribeCasting subscribeCasting)
        {
            return Ok(await _subscribeCastingService.CheckSubscribeCasting(subscribeCasting));
        }

        //[HttpGet]
        //public IActionResult Gets()
        //{
        //    return Ok(_subscribeCastingService.Get().ToList());
        //}
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetById(string id)
        //{
        //    return Ok(_subscribeCastingService.Get(id));
        //}
        /*
        #region hdev
        /// <summary>
        /// Subscribe casting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(SubscribeCastingViewModel entity)
        {
            return Ok(await _subscribeCastingService.SubscribeCastingCall(entity));
        }
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(string id,SubscribeCasting entity)
        //{
        //    _subscribeCastingService.Update(entity);
        //    return Ok();
        //}

        /// <summary>
        /// Unsubscribe casting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(SubscribeCastingViewModel entity)
        {
            return Ok(await _subscribeCastingService.CancelSubscribeCastingCall(entity));
        }
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_subscribeCastingService.Count());
        //}
        #endregion*/
    }
}
