using AYDCommerce.API.Application.Features.Products.Rules;
using AYDCommerce.API.Application.Interfaces.UnitOfWorks;
using AYDCommerce.API.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Application.Features.Products.Commands
{
    public class CreateProductRequest:IRequest<Unit>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public IList<int> CategoryIds { get; set; }
    }

    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductRules _productRules;

        public CreateProductRequestHandler(IUnitOfWork unitOfWork, ProductRules productRules)
        {
            _unitOfWork = unitOfWork;
            _productRules = productRules;
        }

        public async Task<Unit> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            IList<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();

            await _productRules.ProductTitleMustNotBeSame(products, request.Title);

            Product product = new Product(
                request.Title, 
                request.Description,
                request.BrandId, 
                request.Price, 
                request.Discount);
            await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);

            int result = await _unitOfWork.SaveAsync();

            if(result > 0)
            {
                foreach (int categoryId in request.CategoryIds)
                {
                    await _unitOfWork
                        .GetWriteRepository<ProductCategory>()
                        .AddAsync(new ProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId
                        });
                }

                await _unitOfWork.SaveAsync();
            }

            return Unit.Value;
        }
    }
}
