using SmnHelpDesk.Web.Application.Chamado.Model;
using System.Collections.Generic;

namespace SmnHelpDesk.Web.Application.Chamado
{
    public class ChamadoApplication : BaseApplication
    {
        public ChamadoApplication() : base("chamado") { }

        public Response<IEnumerable<ChamadoModel>> Get() => Get<IEnumerable<ChamadoModel>>(null, "", new { idEmpresa = 2 });

        public Response<ChamadoModel> Get(int? idChamado) => Get<ChamadoModel>(idChamado, "");

        public Response<IEnumerable<TipoCriticidadeModel>> GetTipoCriticidade() => Get<IEnumerable<TipoCriticidadeModel>>("TipoCriticidade");

        public Response<IEnumerable<ChamadoTipoModel>> GetChamadoTipo() => Get<IEnumerable<ChamadoTipoModel>>("ChamadoTipo");

        public Response<IEnumerable<ChamadoTipoStatusModel>> GetChamadoTipoStatus()
            => Get<IEnumerable<ChamadoTipoStatusModel>>("ChamadoTipoStatus");

        public Response Post(Entities.Chamado chamado) => Post<Entities.Chamado>(chamado);

        public Response<Entities.Chamado> Put(Entities.Chamado chamado) => (Response<Entities.Chamado>)Put(chamado, chamado.Id);

        public Response<Entities.Chamado> PutStatus(Entities.Chamado chamado) => (Response<Entities.Chamado>)Put("/status", chamado.Id);
    }
}

