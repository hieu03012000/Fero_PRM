using FeroPRMData.Models;
using FeroPRMData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/model-styles")]
    public partial class ModelStylesController : ControllerBase
    {
        private readonly IModelStyleService _modelStyleService;
        public ModelStylesController(IModelStyleService modelStyleService){
            _modelStyleService=modelStyleService;
        }
        //[HttpGet]
        //public IActionResult Gets()
        //{
        //    return Ok(_modelStyleService.Get().ToList());
        //}
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetById(string id)
        //{
        //    return Ok(_modelStyleService.Get(id));
        //}
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Create(ModelStyle entity)
        //{
        //    _modelStyleService.Create(entity);
        //    return  CreatedAtAction(nameof(GetById), new { id = entity}, entity);
        //}
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(string id,ModelStyle entity)
        //{
        //    _modelStyleService.UpdateAsync(entity);
        //    return Ok();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id,ModelStyle entity)
        //{
        //    _modelStyleService.DeleteAsync(entity);
        //    return Ok();
        //}
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_modelStyleService.Count());
        //}
    }
}
