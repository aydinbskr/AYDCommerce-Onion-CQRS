using AYDCommerce.API.Application.Bases;
using AYDCommerce.API.Application.Exceptions;
using AYDCommerce.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Application.Features.Products.Rules
{
    
    public class ProductRules : BaseRules
    {
        public Task ProductTitleMustNotBeSame(IList<Product> products, string requestTitle)
        {
            if (products.Any(x => x.Title == requestTitle)) throw new NotBeSameException("Ürün başlığı zaten var!");
            return Task.CompletedTask;
        }
    }
}
