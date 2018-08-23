using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace SmnHelpDesk.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(config => config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(SimpleInjectorContainer.RegisterServices()));
        }
    }
}
