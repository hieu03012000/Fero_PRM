using FeroPRMData.Services;
using FeroPRMData.ViewModels;
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

        [HttpDelete]
        public async Task<IActionResult> DeleteSubCasting(SubscribeCastingViewModel subscribeCasting)
        {
            var result = await _subscribeCastingService.CancelSubscribeCastingCall(subscribeCasting);
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
        public async Task<IActionResult> CheckSubCastingId(int castingId, string modelId)
        {
            return Ok(await _subscribeCastingService.CheckSubscribeCasting(castingId, modelId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCasting(SubscribeCastingViewModel subscribeCasting)
        {
            var result = await _subscribeCastingService.SubscribeCastingCall(subscribeCasting);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return StatusCode(201);
            }
        }

    }
}
