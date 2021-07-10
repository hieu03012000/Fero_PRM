using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/castings")]
    public partial class CastingsController : ControllerBase
    {
        private readonly ICastingService _castingService;
        private readonly IApplyCastingService _applyCastingService;

        public CastingsController(ICastingService castingService, IApplyCastingService applyCastingService)
        {
            _castingService = castingService;
            _applyCastingService = applyCastingService;
        }

        [HttpGet("new")]
        public async Task<IActionResult> GetNewCasting()
        {
            return Ok(await _castingService.GetNew());
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCasting(string search, double? min, double? max)
        {
            return Ok(await _castingService.Search(search, min, max));
        }

        [HttpGet("{id}/model/{modelId}")]
        public async Task<IActionResult> GetCastingByModel(int id, string modelId)
        {
            var result = await _castingService.GetByModel(id, modelId);
            if (result == null)
            {
                return NotFound();
            } else
            {
                return Ok(result);
            }
        }

        [HttpGet("{id}/customer/{customerId}")]
        public async Task<IActionResult> GetCastingByCustomer(int id, string customerId)
        {
            var result = await _castingService.GetByCustomer(id, customerId);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCasting(CastingViewModel viewModel)
        {
            var result = await _castingService.Add(viewModel);
            if (result != null) {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCasting(int id, CastingViewModel viewModel)
        {
            viewModel.Id = id;
            var result = await _castingService.Update(viewModel);
            if (result != null)
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/stop")]
        public async Task<IActionResult> StopCasting(int id, string customerId)
        {
            var result = await _castingService.Stop(id, customerId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}/{customerId}")]
        public async Task<IActionResult> DeleteCasting(int id, string customerId)
        {
            var result = await _castingService.Delete(id, customerId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/applicants")]
        public async Task<IActionResult> GetListModelByCastingId(int id)
        {
            return Ok(await _applyCastingService.GetApplyModel(id));
        }

    }
}
