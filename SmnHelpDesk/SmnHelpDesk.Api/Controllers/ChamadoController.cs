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

        [HttpGet, Route("")]
        public IHttpActionResult Get(int? idEmpresa, int? idStatus, int? idCliente)
        {
            var chamados = _chamadoRepository.Get(idEmpresa, idStatus, idCliente);
            if (chamados == null)
                return BadRequest("Não foram encontrados chamados");
            return Ok(chamados);
        }

        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var chamado = _chamadoRepository.Get(id);
            if (chamado == null)
                return BadRequest("Não foi encontrado nenhum chamado");
            return Ok(chamado);
        }

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

        [HttpPatch, Route("{id}")]
        public IHttpActionResult PutMotivoCancelamento(int id, string descricaoMotivoCancel)
        {
            _chamadoRepository.Put(id, descricaoMotivoCancel);
            return Ok();
        }

        [HttpPut, Route("{idChamado}")]
        public IHttpActionResult PutStatusCancelamento(int idChamado, int idStatus)
        {
            var historicoChamado = new ChamadoHistoricoStatusDto
            {
                IdChamado = idChamado,
                IdStatus = idStatus
            };

            if (idStatus != 1 || idStatus != 2)
                return BadRequest("Não é possível cancelar chamado");
            _chamadoService.PutStatus(historicoChamado);
            return Ok();
        }
    }
}
