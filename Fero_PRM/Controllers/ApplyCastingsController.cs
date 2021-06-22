using FeroPRMData.Models;
using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/apply-castings")]
    public partial class ApplyCastingsController : ControllerBase
    {
        private readonly IApplyCastingService _applyCastingService;
        public ApplyCastingsController(IApplyCastingService applyCastingService)
        {
            _applyCastingService = applyCastingService;
        }

        [HttpPost]
        public IActionResult CreateApplyCasting(ApplyCastingViewModel apply)
        {
            System.Console.WriteLine(apply.CastingId);
            System.Console.WriteLine(apply.ModelId);
            var applyCasting = _applyCastingService.ApplyCastingCall(apply);
            if(applyCasting == null)
            {
                return BadRequest("already created");
            }
            else
            {
                return StatusCode(201);
            }
            
        }

        //[HttpGet]
        //public IActionResult Gets()
        //{
        //    return Ok(_applyCastingService.Get().ToList());
        //}
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetById(string id)
        //{
        //    return Ok(_applyCastingService.Get(id));
        //}
        #region hdev
        /// <summary>
        /// Model apply vào casting call
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
/*        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ApplyCastingViewModel entity)
        {

            return Ok(await _applyCastingService.ApplyCastingCall(entity));
        }*/

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(string id,ApplyCasting entity)
        //{
        //    _applyCastingService.Update(entity);
        //    return Ok();
        //}

        /// <summary>
        /// Cancel aplly casting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(ApplyCastingViewModel entity)
        {
            return Ok(await _applyCastingService.CancelApplyCastingCall(entity));
        }
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_applyCastingService.Count());
        //}
        #endregion
    }
}
