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
    [Route("api/model-offers")]
    public partial class ModelOfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public ModelOfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateModelOffer(ShowModelOffer modelOffer)
        {
            var result = await _offerService.UpdateModelOffer(modelOffer);
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
