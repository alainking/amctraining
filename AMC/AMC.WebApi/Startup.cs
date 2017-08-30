using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace AMC.WebApi
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      var httpConfig = SetupHttpConfiguration();
      app.UseCors(CorsOptions.AllowAll)
         .UseWebApi(httpConfig);
    }

    private HttpConfiguration SetupHttpConfiguration()
    {
      var httpConfiguration = new HttpConfiguration();

      // Configure Web API Routes:
      // - Enable Attribute Mapping
      // - Enable Default routes at /api.
      httpConfiguration.MapHttpAttributeRoutes();
      httpConfiguration.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/v1/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
          );

      // Only allow json output format -- prevent XML serialization errorMsg
      // http://codeglee.com/2014/07/20/serializationexception-when-using-entityframework-pocos-in-webapi/

      httpConfiguration.Formatters.Clear();
      httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());
      httpConfiguration.Formatters.JsonFormatter.SerializerSettings =
          new JsonSerializerSettings
          {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
          };

      //Swashbuckle Configuration
      httpConfiguration
        .EnableSwagger(c =>
        {
          c.SingleApiVersion("v1", "AMC Web API");
          c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        })
        .EnableSwaggerUi();

      return httpConfiguration;
    }
  }
}