using AYDCommerce.API.Application.Bases;
using AYDCommerce.API.Application.Features.Brands.Dtos;
using AYDCommerce.API.Application.Interfaces.AutoMapper;
using AYDCommerce.API.Application.Interfaces.RedisCache;
using AYDCommerce.API.Application.Interfaces.UnitOfWorks;
using AYDCommerce.API.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Application.Features.Brands.Queries
{
    public class GetAllBrandsQueryRequest : IRequest<IList<BrandDto>>, ICacheableQuery
    {
        public string CacheKey => "GetAllBrands";

        public double CacheTime => 5;
    }

    public class GetAllBrandsQueryHandler : BaseHandler, IRequestHandler<GetAllBrandsQueryRequest, IList<BrandDto>>
    {
        public GetAllBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<IList<BrandDto>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await unitOfWork.GetReadRepository<Brand>().GetAllAsync();

            return mapper.Map<BrandDto, Brand>(brands);
        }
    }
}
