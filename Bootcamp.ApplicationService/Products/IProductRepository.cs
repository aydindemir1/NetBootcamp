using Bootcamp.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.ApplicationService.Products
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product product);
        Task<Product?> GetProductById(int id);
    }
}
