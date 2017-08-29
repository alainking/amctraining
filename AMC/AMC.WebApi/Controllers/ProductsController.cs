﻿using AMC.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace AMC.WebApi.Controllers
{
  public class ProductsController : ApiController
  {
    private string _connectionString = new SqlConnectionStringBuilder
    {
      DataSource = @"localhost\sqlexpress",
      IntegratedSecurity = false,
      UserID = "sa",
      Password = "P@ssw0rd",
      InitialCatalog = "amctraining"
    }.ToString();
    private ProductsApi _productsApi;

    public ProductsController()
    {
      _productsApi = new ProductsApi(_connectionString);
    }

    // GET api/v1/products 
    public async Task<IEnumerable<Product>> Get()
    {
      var products = await _productsApi.ListAsync();
      return products;
    }

    // GET api/v1/products/5
    public async Task<Product> Get(int id)
    {
      var product = await _productsApi.FindAsync(id);
      return product;
    }

    // POST api/v1/products 
    public async Task<int> Post(Product product)
    {
      var productId = await _productsApi.AddAsync(product);
      return productId;
    }

    // PUT api/v1/product/5 
    public async Task<HttpResponseMessage> Put(int id, Product product)
    {
      if (id != product.ID)
      {
        return Request.CreateResponse(HttpStatusCode.Conflict, false);
      }
      else
      {
        var edited = await _productsApi.EditAsync(product);
        return Request.CreateResponse(HttpStatusCode.OK, edited);
      }
    }

    // DELETE api/v1/values/5 
    public async Task<bool> Delete(int id)
    {
      var deleted = await _productsApi.DeleteAsync(id);
      return deleted;
    }
  }

}
