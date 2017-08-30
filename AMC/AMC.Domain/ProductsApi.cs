using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Domain
{
  public class ProductsApi
  {
    private ProductsRepo _productsRepo;

    public ProductsApi(string connectionString)
    {
      _productsRepo = new ProductsRepo(connectionString);
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
      var products = await _productsRepo.ListAsync();
      return products;
    }

    public async Task<Product> FindAsync(int id)
    {
      var product = await _productsRepo.FindAsync(id);
      return product;
    }

    public async Task<Product> FindByCodeAsync(string code)
    {
      var product = await _productsRepo.FindByCodeAsync(code);
      return product;
    }

    public async Task<int> AddAsync(Product product)
    {
      var productId = await _productsRepo.AddAsync(product);
      return productId;
    }

    public async Task<bool> EditAsync(Product product)
    {
      var edited = await _productsRepo.EditAsync(product);
      return edited;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var deleted = await _productsRepo.DeleteAsync(id);
      return deleted;
    }
  }
}
