using BevShopping.Catalog.Application.Catalogs;
using BevShopping.Shared.Core.Commands;
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

        [HttpPost]
        public async Task Create(Create.Command command, CancellationToken cancellationToken)
        {
            await _commandBus.Send(command, cancellationToken);
        }

    }
}
