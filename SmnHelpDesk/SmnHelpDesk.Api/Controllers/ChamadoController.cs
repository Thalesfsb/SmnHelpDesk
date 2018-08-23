using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.Chamado.Dto;
using System.Net;
using System.Web.Http;

namespace SmnHelpDesk.Api.Controllers
{
    [RoutePrefix("api/chamado")]
    public class ChamadoController : ApiController
    {
        private readonly Notification _notification;

        private readonly IChamadoService _chamadoService;

        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoController(Notification notification, IChamadoService chamadoService, IChamadoRepository chamadoRepository)
        {
            _notification = notification;
            _chamadoService = chamadoService;
            _chamadoRepository = chamadoRepository;
        }

        [HttpGet, Route("{idEmpresa},{idStatus},{idCliente}")]
        public IHttpActionResult Get(int? idEmpresa, int? idStatus, int? idCliente)
        {
            var chamados = _chamadoRepository.Get(idEmpresa, idStatus, idCliente);
            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok(chamados);

        }

        [HttpGet, Route("{NumeroChamado}")]
        public IHttpActionResult Get(int numeroChamado)
        {
            var chamado = _chamadoRepository.Get(numeroChamado);
            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok(chamado);
        }

        [HttpPut, Route("")]
        public IHttpActionResult Put(ChamadoDto chamado)
        {
            if (chamado.IsValid(_notification))
                return Content(HttpStatusCode.BadRequest, _notification.Get);

            _chamadoRepository.Put(chamado);

            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post(ChamadoDto chamado)
        {
            if (chamado.IsValid(_notification))
                return Content(HttpStatusCode.BadRequest, _notification.Get);

            _chamadoRepository.Post(chamado);

            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }
    }
}
