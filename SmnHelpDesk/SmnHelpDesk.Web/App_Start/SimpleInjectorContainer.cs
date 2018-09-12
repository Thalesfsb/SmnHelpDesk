using SimpleInjector;
using SmnHelpDesk.Web.Application.Chamado;
namespace SmnHelpDesk.Web
{
    public class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            var container = new Container();
            container.Register<ChamadoApplication>();
            container.Verify();
            return container;
        }
    }
}