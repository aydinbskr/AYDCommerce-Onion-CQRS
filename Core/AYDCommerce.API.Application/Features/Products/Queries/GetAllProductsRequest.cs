using AYDCommerce.API.Application.Features.Products.Dtos;
using AYDCommerce.API.Application.Interfaces.AutoMapper;
using AYDCommerce.API.Application.Interfaces.UnitOfWorks;
using AYDCommerce.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public GetAllProductsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork
                .GetReadRepository<Product>()
                .GetAllAsync(include: x => x.Include(b => b.Brand));

            _mapper.Map<BrandDto, Brand>(new Brand());
            var mappedProducts = _mapper.Map<ProductDto, Product>(products);
            foreach (var item in mappedProducts)
            {
                item.Price -= (item.Price * item.Discount / 100);
            }
            return mappedProducts;
        }
    }
}
