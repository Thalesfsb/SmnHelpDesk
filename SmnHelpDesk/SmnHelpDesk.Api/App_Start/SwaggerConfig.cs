using SmnHelpDesk.Api;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SmnHelpDesk.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "SmnHelpDesk.Api");

                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Momentum[docs]");
                    });
        }
    }
}
