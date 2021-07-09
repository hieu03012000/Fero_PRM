using FeroPRMData.Models;
using FeroPRMData.Services;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/images")]
    public partial class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        //        [HttpGet("{id}")]
        //        [ProducesResponseType(StatusCodes.Status200OK)]
        //        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //        public IActionResult GetById(int id)
        //        {
        //            return Ok(_imageService.Get(id));
        //        }
        //        [HttpPost]
        //        [ProducesResponseType(StatusCodes.Status201Created)]
        //        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //        public IActionResult Create(Image entity)
        //        {
        //            _imageService.Create(entity);
        //            return  CreatedAtAction(nameof(GetById), new { id = entity}, entity);
        //        }
        //        [HttpPut("{id}")]
        //        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //        public IActionResult Update(int id,Image entity)
        //        {
        //            _imageService.UpdateAsync(entity);
        //            return Ok();
        //        }
        //        [HttpDelete("{id}")]
        //        public IActionResult Delete(int id,Image entity)
        //        {
        //            _imageService.DeleteAsync(entity);
        //            return Ok();
        //        }
        //        [HttpGet("count")]
        //        public IActionResult Count()
        //        {
        //            return Ok(_imageService.Count());
        //        }
    }
}
