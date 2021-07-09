using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public partial class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICastingService _castingService;
        private readonly IOfferService _offerService;
        private readonly IFavoriteModelService _favoriteModelService;

        public CustomersController(ICustomerService customerService, ICastingService castingService, IOfferService offerService, IFavoriteModelService favoriteModelService)
        {
            _customerService = customerService;
            _castingService = castingService;
            _offerService = offerService;
            _favoriteModelService = favoriteModelService;
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckGmail(string gmail)
        {
            return Ok(await _customerService.CheckCustomerGmail(gmail));
        }

        [HttpGet("gmail/{gmail}")]
        public async Task<IActionResult>GetCustomerByGmail(string gmail)
        {
            var customer = await _customerService.GetCustomerProfile(gmail);
            if (customer != null)
            {
                return Ok(customer);
            } else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/castings")]
        public async Task<IActionResult> GetCastings(string id)
        {
            return Ok(await _castingService.GetList(id));
        }

        [HttpGet("{id}/casting-ids")]
        public async Task<IActionResult> GetCastingIds(string id)
        {
            return Ok(await _castingService.GetIdList(id));
        }

        [HttpGet("{id}/import-castings")]
        public async Task<IActionResult> GetImportCastings(string id)
        {
            return Ok(await _castingService.GetImportList(id));
        }

        [HttpGet("{id}/offers")]
        public async Task<IActionResult> GetOffers(string id)
        {
            return Ok(await _offerService.GetList(id));
        }

        [HttpGet("{id}/favorite-models")]
        public async Task<IActionResult> GetFavoriteModels(string id)
        {
            return Ok(await _favoriteModelService.GetAll(id));
        }

        [HttpPost]
        public async Task<IActionResult>CreateCustomer(CreateCustomerAccountViewModel customer)
        {
            var result = await _customerService.CreateCustomerAccount(customer);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return StatusCode(201);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeviceToken(string id, string deviceToken)
        {
            var result = await _customerService.UpdateDeviceToken(id, deviceToken);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
