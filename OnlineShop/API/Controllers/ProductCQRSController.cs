using Application.CQRS.ProductCommandQuery.Command;
using Application.CQRS.ProductCommandQuery.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCQRSController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductCQRSController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveProductCommand saveProductCommand)
        {
            var result = await mediator.Send(saveProductCommand);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get([FromQuery]GetProductQuery getProductQuery)
        {
            return Ok(await mediator.Send(getProductQuery));
        }
    }
}
