using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/apply-castings")]
    public partial class ApplyCastingsController : ControllerBase
    {
        private readonly IApplyCastingService _applyCastingService;

        public ApplyCastingsController(IApplyCastingService applyCastingService)
        {
            _applyCastingService = applyCastingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplyCasting(ApplyCastingViewModel apply)
        {
            var applyCasting = await _applyCastingService.ApplyCastingCall(apply);
            if(applyCasting == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApplyCasting(ApplyCastingViewModel apply)
        {
            var applyCasting = await _applyCastingService.CancelApplyCastingCall(apply);
            if (applyCasting == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckApplyCastingId(int castingId, string modelId)
        {
            return Ok(await _applyCastingService.CheckApplyCasting(castingId, modelId));
        }

    }
}
