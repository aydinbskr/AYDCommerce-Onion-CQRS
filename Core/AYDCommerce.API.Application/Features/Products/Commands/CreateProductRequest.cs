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
    public class CreateProductRequest:IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public IList<int> CategoryIds { get; set; }
    }

    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
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
        }
    }
}
