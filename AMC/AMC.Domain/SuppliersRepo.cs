using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AMC.Domain
{
  internal class SuppliersRepo
  {
    private string _connectionString;

    public SuppliersRepo(string connectionString)
    {
      _connectionString = connectionString;
    }

    internal async Task<IEnumerable<Supplier>> ListAsync()
    {
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var suppliers = await conn.QueryAsync<Supplier>("rsp_Suppliers_Get", commandType: CommandType.StoredProcedure);
        return suppliers;
      }
    }

    internal async Task<Supplier> FindByIdAsync(int id)
    {
      var parms = new { ID = id };
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var suppliers = await conn.QueryAsync<Supplier>("rsp_Suppliers_Find", parms, commandType: CommandType.StoredProcedure);
        return suppliers.FirstOrDefault();
      }
    }

    internal async Task<bool> DeleteAsync(int id)
    {
      var parms = new { ID = id };
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var rowsDeleted = await conn.ExecuteAsync("rsp_Suppliers_Del", parms, commandType: CommandType.StoredProcedure);
        return rowsDeleted == 1;
      }
    }

    internal async Task<bool> EditAsync(Supplier supplier)
    {
      var parms = new
      {
        ID = supplier.ID,
        Code = supplier.Code,
        Description = supplier.Description,
        ContactNo = supplier.ContactNo,
        EditUser = supplier.EditUser
      };
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        var rowsEdited = await conn.ExecuteAsync("rsp_Suppliers_Edit", parms, commandType: CommandType.StoredProcedure);
        return rowsEdited == 1;
      }
    }

    internal async Task<int> AddAsync(Supplier supplier)
    {
      var parms = new DynamicParameters();
      parms.Add("@Code", supplier.Code);
      parms.Add("@Description",supplier.Description);
      parms.Add("@ContactNo", supplier.ContactNo);
      parms.Add("@CreateUser", supplier.CreateUser);
      parms.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
      using (var conn = new SqlConnection(_connectionString))
      {
        await conn.OpenAsync();
        await conn.ExecuteAsync("rsp_Suppliers_Add", parms, commandType: CommandType.StoredProcedure);
        var supplierId = parms.Get<int>("@ID");
        return supplierId;
      }
    }
  }
}