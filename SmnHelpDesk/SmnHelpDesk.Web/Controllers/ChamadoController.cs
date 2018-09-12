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
            var comboChamadoTipoStatus = GetChamadoTipoStatus(null);
            if (comboChamadoTipoStatus == null)
                return Error(new List<string> { "Erro ao buscar tipos de criticidades" });

            if (id == null)
                return Error(new List<string> { "Erro chamado inexistente" });

            var response = _chamadoApplication.Get((int)id);
            if (!response.Ok)
                return Error(new List<string> { "Erro ao buscar detalhes do chamado" });

            var chamado = new ChamadoViewModel(response.Dados);
            chamado.ComboStatus = GetChamadoTipoStatus(chamado.IdStatus);
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

        private SelectList GetChamadoTipoStatus(int? selectedValue)
        {
            var responseChamadoTipoStatus = _chamadoApplication.GetChamadoTipoStatus();
            return !responseChamadoTipoStatus.Ok
                ? null
                : new SelectList(responseChamadoTipoStatus.Dados,
                    nameof(ChamadoTipoStatusModel.Id), nameof(ChamadoTipoStatusModel.Nome), selectedValue);
        }
    }

}
