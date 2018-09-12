using SmnHelpDesk.Web.Application.Chamado;
using SmnHelpDesk.Web.Application.Chamado.Entities;
using SmnHelpDesk.Web.Application.Chamado.Model;
using SmnHelpDesk.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmnHelpDesk.Web.Controllers
{
    public class ChamadoController : BaseController
    {
        private readonly ChamadoApplication _chamadoApplication;

        public ChamadoController(ChamadoApplication chamadoApplication)
        {
            _chamadoApplication = chamadoApplication;
        }

        public ActionResult Index() => View();

        public ActionResult GetAll()
        {
            var response = _chamadoApplication.Get();
            return response.Ok ? View("_Grid", response.Dados) : Error(response.Erros);
        }

        public ActionResult Get(int? id)
        {
            var comboCriticidades = GetComboCriticidade(null);
            if (comboCriticidades == null)
                return Error(new List<string> { "Erro ao buscar tipos de criticidades" });

            var comboTipos = GetComboChamadoTipo(null);
            if (comboTipos == null)
                return Error(new List<string> { "Erro ao buscar tipos de chamado" });

            if (id == null)
                return Error(new List<string> { "Erro chamado inexistente" });

            var response = _chamadoApplication.Get((int)id);
            if (!response.Ok)
                return Error(new List<string> { "Erro ao buscar detalhes do chamado" });

            var chamado = new ChamadoViewModel(response.Dados);
            chamado.ComboTipos = GetComboChamadoTipo(chamado.IdTipo);
            chamado.ComboCriticidades = GetComboCriticidade(chamado.IdCriticidade);
            return View("_Dados", chamado);
        }

        public ActionResult Delete(int id, string motivo)
        {
            return View("_Grid");
        }

        public ActionResult Save(Chamado chamado)
        {
            return View("_Grid");
        }

        private SelectList GetComboCriticidade(int? selectedValue)
        {
            var responseCriticidades = _chamadoApplication.GetTipoCriticidade();
            return !responseCriticidades.Ok
                ? null
                : new SelectList(responseCriticidades.Dados,
                    nameof(TipoCriticidadeModel.Id), nameof(TipoCriticidadeModel.Nome), selectedValue);
        }

        private SelectList GetComboChamadoTipo(int? selectedValue)
        {
            var responseChamadoTipo = _chamadoApplication.GetChamadoTipo();
            return !responseChamadoTipo.Ok
                ? null
                : new SelectList(responseChamadoTipo.Dados,
                    nameof(ChamadoTipoModel.Id), nameof(ChamadoTipoModel.Nome), selectedValue);
        }
    }

}
