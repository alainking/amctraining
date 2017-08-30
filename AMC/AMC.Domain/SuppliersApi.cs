using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMC.Domain
{
  public class SuppliersApi
  {
    private string connectionString;
    private SuppliersRepo _suppliersRepo;

    public SuppliersApi(string connectionString)
    {
      _suppliersRepo = new SuppliersRepo(connectionString);
    }

    public async Task<IEnumerable<Supplier>> ListAsync()
    {
      var suppliers = await _suppliersRepo.ListAsync();
      return suppliers;
    }

    public async Task<Supplier> FindByIdAsync(int id)
    {
      var supplier = await _suppliersRepo.FindByIdAsync(id);
      return supplier;
    }

    public async Task<int> AddAsync(Supplier supplier)
    {
      var supplierId = await _suppliersRepo.AddAsync(supplier);
      return supplierId;
    }

    public async Task<bool> EditAsync(Supplier supplier)
    {
      var edited = await _suppliersRepo.EditAsync(supplier);
      return edited;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var deleted = await _suppliersRepo.DeleteAsync(id);
      return deleted;
    }
  }
}
