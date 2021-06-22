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
    [Route("api/models")]
    public partial class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;
        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        public IActionResult Gets()
        {
            return Ok(_modelService.Get().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Gets(string id)
        {
            return Ok(_modelService.GetCompleteModelsById(id));
        }

        [HttpGet("gmail/{id}")]
        public IActionResult GetModelByGmail(string id)
        {
            return Ok(_modelService.GetCompleteModelByGmail(id));
        }

        /*        [HttpGet("gmail/{id}")]
                public async Task<IActionResult> GetByGmail(string id)
                {
                    var user = await _modelService.GetModelByGmail(id);
                    if(user != null)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }*/

        [HttpGet("check")]
        public bool CheckGmail(string gmail)
        {
            return _modelService.CheckModelGmail(gmail);
        }

        [HttpGet("check")]
        public bool CheckGmail(CheckGmail gmail)
        {
            return _modelService.CheckModelGmail(gmail.Id, gmail.Gmail);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateModel(Model model)
        {
            var result = await _modelService.CreateModel(model);
            if (result != null) {
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        }

        #region hdev
        /*        /// <summary>
                /// Get model by Id
                /// </summary>
                /// <param name="id"></param>
                /// <returns></returns>
                [HttpGet("{id}")]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                public async Task<IActionResult> GetById(string id)
                {
                    return Ok(await _modelService.GetModelById(id));
                }
        */
        /// <summary>
        /// Model sign up
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateModelAccountViewModel entity)
        {
            return Ok(await _modelService.CreateModelAccount(entity));
        }
        #endregion

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult Update(string id,Model entity)
        //{
        //    _modelService.Update(entity);
        //    return Ok();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id,Model entity)
        //{
        //    _modelService.Delete(entity);
        //    return Ok();
        //}
        //[HttpGet("count")]
        //public IActionResult Count()
        //{
        //    return Ok(_modelService.Count());
        //}
    }
}
