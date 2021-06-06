using Fero.Data.ViewModels;
using FeroPRMData.Models;
using FeroPRMData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/model-castings")]
    public partial class ModelCastingsController : ControllerBase
    {
        private readonly IModelCastingService _modelCastingService;
        public ModelCastingsController(IModelCastingService modelCastingService)
        {
            _modelCastingService = modelCastingService;
        }
        //[HttpGet]
        //public IActionResult Gets()
        //{
        //    return Ok(_modelCastingService.Get().ToList());
        //}
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetById(int id)
        //{
        //    return Ok(_modelCastingService.Get(id));
        //}
        #region hdev
        /// <summary>
        /// Accept casting (offer)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("accept")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Accept(AcceptCastingViewModel entity)
        {
            return Ok(await _modelCastingService.AcceptCastingCall(entity));
        }

        /// <summary>
        /// deny casting (offer)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("deny")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Deny(AcceptCastingViewModel entity)
        {
            return Ok(await _modelCastingService.DenyCastingCall(entity));
        }
        #endregion

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(int id,ModelCasting entity)
        //{
        //    _modelCastingService.Update(entity);
        //    return Ok();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id,ModelCasting entity)
        //{
        //    _modelCastingService.Delete(entity);
        //    return Ok();
        //}
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_modelCastingService.Count());
        //}
    }
}
