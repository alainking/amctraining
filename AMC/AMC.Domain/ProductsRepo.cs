using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AMC.Domain
{
  internal class ProductsRepo
  {
    private string _connectionString;

    public ProductsRepo(string connectionString)
    {
      _connectionString = connectionString;
    }

    internal async Task<IEnumerable<Product>> ListAsync()
    {
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var products = await conn.QueryAsync<Product>("rsp_Products_Get", commandType: CommandType.StoredProcedure);
        return products;
      }
    }

    internal async Task<Product> FindAsync(int id)
    {
      var parms = new { ID = id };
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var products = await conn.QueryAsync<Product>("rsp_Products_Find", parms, commandType: CommandType.StoredProcedure);
        return products.FirstOrDefault();
      }
    }

    internal async Task<Product> FindByCodeAsync(string code)
    {
      var parms = new { Code = code };
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var products = await conn.QueryAsync<Product>("rsp_Products_FindByCode", parms, commandType: CommandType.StoredProcedure);
        return products.FirstOrDefault();
      }
    }

    internal async Task<bool> DeleteAsync(int id)
    {
      var parms = new { ID = id };
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var deletedRows = await conn.ExecuteAsync("rsp_Products_Del", parms, commandType: CommandType.StoredProcedure);
        return deletedRows == 1;
      }
    }

    internal async Task<bool> EditAsync(Product product)
    {
      var parms = new
      {
        ID = product.ID,
        Code = product.Code,
        Description = product.Description,
        EditUser = product.EditUser
      };
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var rowsEdited = await conn.ExecuteAsync("rsp_Products_Edit", parms, commandType: CommandType.StoredProcedure);
        return rowsEdited == 1;
      }
    }

    internal async Task<int> AddAsync(Product product)
    {
      var parms = new DynamicParameters();
      parms.Add("@Code", product.Code);
      parms.Add("@Description", product.Description);
      parms.Add("@CreateUser", product.CreateUser);
      parms.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        await conn.ExecuteAsync("rsp_Products_Add", parms, commandType: CommandType.StoredProcedure);
        var productId = parms.Get<int>("@ID");
        return productId;
      }
    }
  }
}