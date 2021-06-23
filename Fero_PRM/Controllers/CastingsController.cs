using FeroPRMData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/castings")]
    public partial class CastingsController : ControllerBase
    {
        private readonly ICastingService _castingService;
        private readonly IModelService _modelService;
        public CastingsController(ICastingService castingService, IModelService modelService)
        {
            _castingService = castingService;
            _modelService = modelService;
        }

        /// <summary>
        /// Filter casting
        /// </summary>
        /// <returns></returns>
        [HttpGet("new")]
        public async Task<IActionResult> GetNewCasting()
        {
            return Ok(await _castingService.NewCasting());
        }

        [HttpGet]
        public async Task<IActionResult> GetCastings([FromBody]string customerId)
        {
            return Ok(await _castingService.GetListCasting(customerId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCastingById(int id)
        {
            return Ok(await _castingService.GetCastingById(id));
        }

        //#region hdev
        ///// <summary>
        ///// Filter casting
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="min"></param>
        ///// <param name="max"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> Get(string name, decimal? min, decimal? max)
        //{
        //    return Ok(await _castingService.FilterCasting(name, min, max));
        //}

        ///// <summary>
        ///// Get casting by Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    return Ok(await _castingService.GetCastingById(id));
        //}

        ///// <summary>
        ///// Create casting
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Create(CreateCastingCallViewModel entity)
        //{
        //    return Ok(await _castingService.CreateCasting(entity));
        //}

        ///// <summary>
        ///// Update casting
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Update(int id, UpdateCastingViewModel entity)
        //{
        //    if (id != entity.Id) return BadRequest();
        //    return Ok(await _castingService.UpdateCasting(entity));
        //}

        ///// <summary>
        ///// Public casting
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPut("{id}/public")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Public(int id, PublicCastingViewModel model)
        //{
        //    return Ok(await _castingService.PublicCasting(id, model));
        //}

        ///// <summary>
        ///// Stop casting
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpPut("{id}/stop")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Stop(int id)
        //{
        //    return Ok(await _castingService.StopCasting(id));
        //}

        ///// <summary>
        ///// Delete casting
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return Ok(await _castingService.DeleteCasting(id));
        //}

        ///// <summary>
        ///// Get applicant list
        ///// </summary>
        ///// <param name="castingId"></param>
        ///// <returns></returns>
        //[HttpGet("{castingId}/applicants")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetApplicant(int castingId)
        //{
        //    return Ok(await _modelService.GetApplicantList(castingId));
        //}

        ///// <summary>
        ///// Get applicant list
        ///// </summary>
        ///// <param name="offer"></param>
        ///// <returns></returns>
        //[HttpPost("offer")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> MakeOffer(MakeOfferViewModel offer)
        //{
        //    return Ok(await _castingService.MakeOffer(offer));
        //}
        //#endregion
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_castingService.Count());
        //}
    }
}
