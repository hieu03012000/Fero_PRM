using FeroPRMData.Models;
using FeroPRMData.Services;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Gets(int id)
        {
            return Ok(await _offerService.GetOfferWithListModel(id));
        }

        [HttpGet("general")]
        public async Task<IActionResult> GetGeneral(string modelId, int offerId)
        {
            return Ok(await _modelOfferService.GetById(modelId, offerId));
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            return Ok(await _offerService.GetOffer());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffers(CreateOffer createOffer)
        {
            return Ok(await _offerService.CreateOffers(createOffer));
        }
    }
}
