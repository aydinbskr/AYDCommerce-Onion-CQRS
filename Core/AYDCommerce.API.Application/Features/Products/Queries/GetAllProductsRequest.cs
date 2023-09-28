using AYDCommerce.API.Application.Features.Products.Dtos;
using AYDCommerce.API.Application.Interfaces.UnitOfWorks;
using AYDCommerce.API.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Application.Features.Products.Queries
{
    public class GetAllProductsRequest:IRequest<IList<ProductDto>>
    {
    }

    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, IList<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
            return products.Select(x => new ProductDto
            {
                Description = x.Description,
                Discount = x.Discount,
                Price = x.Price - (x.Price * x.Discount / 100),
                Title = x.Title
            }).ToList();
        }
    }
}
