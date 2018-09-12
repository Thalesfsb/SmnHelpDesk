using SimpleInjector;
using SimpleInjector.Lifestyles;
using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.ChamadoTipo;
using SmnHelpDesk.Domain.ChamadoTipoStatus;
using SmnHelpDesk.Domain.Cliente;
using SmnHelpDesk.Domain.Empresa;
using SmnHelpDesk.Domain.TipoCriticidade;
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
            container.Register<IChamadoTipoRepository, ChamadoTipoRepository>();
            container.Register<IClienteRepository, ClienteRepository>();
            container.Register<ITipoCriticidadeRepository, TipoCriticidadeRepository>();
            container.Register<IChamadoTipoStatusRepository, ChamadoTipoStatusRepository>();
            container.Register<Notification>(Lifestyle.Scoped);
            container.Verify();
            return container;
        }
    }
}