using FeroPRMData.Models;
using FeroPRMData.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/models")]
    public partial class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly ISubscribeCastingService _subscribeCastingService;
        private readonly IModelOfferService _modelOfferService;

        public ModelsController(IModelService modelService, ISubscribeCastingService subscribeCastingService, IModelOfferService modelOfferService)
        {
            _modelService = modelService;
            _subscribeCastingService = subscribeCastingService;
            _modelOfferService = modelOfferService;
        }

        [HttpGet("new")]
        public IActionResult Gets()
        {
            return Ok(_modelService.GetNew());
        }

        [HttpGet("{id}/subscribe-castings")]
        public async Task<IActionResult> GetModelCastings(string id)
        {
            return Ok(await _subscribeCastingService.GetSubscribeCastings(id));
        }

        [HttpGet("{id}/offers")]
        public async Task<IActionResult> GetModelOffers(string id)
        {
            return Ok(await _modelOfferService.GetListByModel(id));
        }

        [HttpGet("gmail/{gmail}")]
        public IActionResult GetModelByGmail(string gmail)
        {
            var result = _modelService.GetCompleteModelByGmail(gmail);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/{customerId}")]
        public IActionResult GetModelById(string id, string customerId)
        {
            var result = _modelService.GetCompleteModelById(id, customerId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("check")]
        public bool CheckGmail(string gmail)
        {
            return _modelService.CheckModelGmail(gmail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(Model model)
        {
            var result = await _modelService.CreateModel(model);
            if (result != null) {
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchModel(string location, int? gender, double? maxW, double? minW, double? maxH, double? minH)
        {
            return Ok(await _modelService.SearchListModel(location, gender, minW, maxW, minH, maxH));
        }

    }
}
