using FeroPRMData.Models;
using FeroPRMData.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/offer")]
    public partial class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Gets(int id)
        {
            return Ok(await _offerService.GetOfferWithListModel(id));
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
