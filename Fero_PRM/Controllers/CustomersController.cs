using FeroPRMData.Models;
using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> CheckGmail(string mail)
        {
            return Ok(await _customerService.CheckCusGmail(mail));
        }

        [HttpGet("{gmail}")]
        public async Task<IActionResult>GetCusByGmail(string gmail)
        {
            return Ok(await _customerService.GetCustomerProfile(gmail));
        }

        [HttpGet("{id}/offers")]
        public async Task<IActionResult> Gets(string id)
        {
            return Ok(await _offerService.GetOfferById(id));
        }

        [HttpPost]
        public async Task<IActionResult>CreateCustomer(Customer customer)
        {
            var result = await _customerService.CreateCustomer(customer);
            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return StatusCode(201);
            }
        }


        //[HttpGet]
        //public IActionResult Gets()
        //{
        //    return Ok(_customerService.Get().ToList());
        //}
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetById(string id)
        //{
        //    return Ok(_customerService.Get(id));
        //}
/*        #region hdev
        /// <summary>
        /// Ceate customer account
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateCustomerAccountViewModel entity)
        {
            return Ok(await _customerService.CreateCustomerAccount(entity));
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(string email)
        {
            return Ok(await _customerService.CustomerLogin(email));
        }
        #endregion*/
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(string id,Customer entity)
        //{
        //    _customerService.Update(entity);
        //    return Ok();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id,Customer entity)
        //{
        //    _customerService.Delete(entity);
        //    return Ok();
        //}
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_customerService.Count());
        //}
    }
}
