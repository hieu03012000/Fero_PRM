using Fero.Data.ViewModels;
using FeroPRMData.Services;
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
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
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
        #region hdev
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
        #endregion
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
