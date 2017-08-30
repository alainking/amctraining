using AMC.Domain;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AMC.WebAPI.Controllers
{
  public class SuppliersController : ApiController
  {

    private string ConnectionString = @"Data Source =.\sqlexpress;Initial Catalog = amctraining; Integrated Security = True";
    private SuppliersApi _suppliersApi;

    public SuppliersController()
    {
      _suppliersApi = new SuppliersApi(ConnectionString);
    }

    // GET api/v1/suppliers 
    public async Task<IEnumerable<Supplier>> Get()
    {
      var suppliers = await _suppliersApi.ListAsync();
      return suppliers;
    }

    // GET api/v1/suppliers/5 
    public async Task<Supplier> Get(int id)
    {
      var supplier = await _suppliersApi.FindByIdAsync(id);
      return supplier;
    }

    // POST api/v1/suppliers 
    public async Task<int> Post(Supplier supplier)
    {
      var supplierId = await _suppliersApi.AddAsync(supplier);
      return supplierId;
    }

    // PUT api/v1/suppliers/5 
    public async Task<HttpResponseMessage> Put(int id, Supplier supplier)
    {
      if (id != supplier.ID)
      {
        return Request.CreateResponse(HttpStatusCode.Conflict, false);
      }
      var edited = await _suppliersApi.EditAsync(supplier);
      return Request.CreateResponse(HttpStatusCode.OK, edited);
    }

    // DELETE api/v1/suppliers/5 
    public async Task<bool> Delete(int id)
    {
      var deleted = await _suppliersApi.DeleteAsync(id);
      return deleted;
    }
  }
}
