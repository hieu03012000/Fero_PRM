using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/model-offers")]
    public partial class ModelOfferController : ControllerBase
    {
        private readonly IModelOfferService _modelOfferService;

        public ModelOfferController(IModelOfferService modelOfferService)
        {
            _modelOfferService = modelOfferService;
        }

        [HttpPut]
        public async Task<IActionResult> Update(ModelOfferViewModel viewModel)
        {
            var result = await _modelOfferService.Update(viewModel);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
