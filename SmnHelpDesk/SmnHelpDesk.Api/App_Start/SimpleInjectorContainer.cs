using SimpleInjector;
using SimpleInjector.Lifestyles;
using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.Empresa;
using SmnHelpDesk.Repository.Repositories;

namespace SmnHelpDesk.Api
{
    public class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<Conexao>();
            container.Register<IEmpresaRepository, EmpresaRepository>();
            container.Register<IEmpresaService, EmpresaService>();
            container.Register<IChamadoRepository, ChamadoRepository>();
            container.Register<IChamadoService, ChamadoService>();
            container.Register<Notification>(Lifestyle.Scoped);
            container.Verify();
            return container;
        }
    }
}