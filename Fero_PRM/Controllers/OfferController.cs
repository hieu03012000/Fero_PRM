using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public partial class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IModelOfferService _modelOfferService;
        public OfferController(IOfferService offerService, IModelOfferService modelOfferService)
        {
            _offerService = offerService;
            _modelOfferService = modelOfferService;
        }

        [HttpGet("{id}/customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int id, string customerId)
        {
            return Ok(await _offerService.GetByCustomer(id, customerId));
        }

        [HttpGet("{id}/model/{modelId}")]
        public async Task<IActionResult> GetByModel(int id, string modelId)
        {
            return Ok(await _modelOfferService.GetByModel(id, modelId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer(OfferViewModel viewModel)
        {
            return Ok(await _offerService.Add(viewModel));
        }
    }
}
