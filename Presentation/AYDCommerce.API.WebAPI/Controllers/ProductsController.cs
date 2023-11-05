using AYDCommerce.API.Application.Features.Products.Commands;
using AYDCommerce.API.Application.Features.Products.Queries;
using AYDCommerce.API.Application.Interfaces.UnitOfWorks;
using AYDCommerce.API.Domain.Entities;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AYDCommerce.API.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() 
        {
            var response = await _mediator.Send(new GetAllProductsRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
