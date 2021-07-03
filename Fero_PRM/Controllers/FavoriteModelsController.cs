using FeroPRMData.Services;
using FeroPRMData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fero_PRM.Controllers
{
    [ApiController]
    [Route("api/favorite-models")]
    public partial class FavoriteModelsController : ControllerBase
    {
        private readonly IFavoriteModelService _favoriteModelService;

        public FavoriteModelsController(IFavoriteModelService favoriteModelService)
        {
            _favoriteModelService = favoriteModelService;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(FavoriteModelViewModel viewModel)
        {
            var result = await _favoriteModelService.Remove(viewModel);
            if(result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(FavoriteModelViewModel viewModel)
        {
            var result = await _favoriteModelService.Add(viewModel);
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
