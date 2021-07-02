using FeroPRMData.Models;
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

        public CustomersController(ICustomerService customerService, ICastingService castingService, IOfferService offerService)
        {
            _customerService = customerService;
            _castingService = castingService;
            _offerService = offerService;
        }

        [HttpGet("{id}/castings")]
        public async Task<IActionResult> GetCastings(string id)
        {
            return Ok(await _castingService.GetListCasting(id));
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

        [HttpGet("{id}/offers")]
        public async Task<IActionResult> Gets(string id)
        {
            return Ok(await _offerService.GetOfferById(id));
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
    }
}
