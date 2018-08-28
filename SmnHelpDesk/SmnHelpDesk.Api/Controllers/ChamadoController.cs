using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.Chamado.Dto;
using System.Net;
using System.Web.Http;

namespace SmnHelpDesk.Api.Controllers
{
    [RoutePrefix("api/chamado")]
    public class ChamadoController : BaseController
    {
        private readonly IChamadoService _chamadoService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly Notification _notification;

        public ChamadoController(Notification notification, IChamadoService chamadoService, IChamadoRepository chamadoRepository)
        {
            _notification = notification;
            _chamadoService = chamadoService;
            _chamadoRepository = chamadoRepository;
        }

        //Buscar os chamados para o grid de acordo com o idEmpresa ou senão passar busca todos chamados
        [HttpGet, Route("")]
        public IHttpActionResult Get(int? idEmpresa)
        {
            var chamados = _chamadoRepository.Get(idEmpresa);
            if (chamados == null)
                return BadRequest("Não foram encontrados chamados");
            return Ok(chamados);
        }

        //Buscar o chamado por id para edição
        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var chamado = _chamadoRepository.Get(id);
            if (chamado == null)
                return BadRequest("Não foi encontrado nenhum chamado");
            return Ok(chamado);
        }

        //Cadastra um novo chamado
        [HttpPost, Route("")]
        public IHttpActionResult Post(ChamadoDto chamado)
        {
            if (chamado == null)
                return BadRequest("Os dados do chamado não foram enviados");

            _chamadoService.Post(chamado);

            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }

        //Faz alteração no chamado
        [HttpPut, Route("{id}")]
        public IHttpActionResult Put(int id, ChamadoDto chamado)
        {
            if (chamado == null)
                return BadRequest("Os dados do chamado não foram enviados");

            chamado.Id = id;

            if (!_chamadoService.Exists(chamado.Id))
                return BadRequest("Esse chamado não existe");

            _chamadoService.Put(chamado);

            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }

        //Atualiza os status de um chamado e inseri no historico de status
        [HttpPut, Route("{idChamado}/status")]
        public IHttpActionResult PutStatus(int idChamado, ChamadoHistoricoStatusDto chamadoHistorico)
        {
            if (chamadoHistorico == null)
                return BadRequest("Os dados do chamado não foram enviados");

            chamadoHistorico.IdChamado = idChamado;
            _chamadoService.PutStatus(chamadoHistorico);

            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }
    }
}
