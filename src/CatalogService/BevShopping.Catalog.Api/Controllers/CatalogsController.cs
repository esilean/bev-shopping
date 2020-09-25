using BevShopping.Catalog.Application.Catalogs;
using BevShopping.Shared.Core.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BevShopping.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogsController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public CatalogsController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }


        [HttpGet]
        public IActionResult GetAll(CancellationToken cancellationToken)
        {
            return Ok("CatalogServiceOK");
        }


        /// <summary>
        /// Create a catalog item
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/catalogs
        ///     {
        ///        "Name": "Shorts Black and White Up",
        ///        "Description": "Description of Shorts Black and White Up"
        ///        "Price": 99.99
        ///        "PictureUri": "/image/products"
        ///        "CatalogType": 1
        ///     }
        ///
        /// </remarks>        
        /// <param name="command"></param>  
        /// <param name="cancellationToken"></param>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Create(Create.Command command, CancellationToken cancellationToken)
        {
            await _commandBus.Send(command, cancellationToken);
        }

    }
}
