using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.Chamado.Dto;
using SmnHelpDesk.Domain.ChamadoTipo;
using SmnHelpDesk.Domain.ChamadoTipoStatus;
using SmnHelpDesk.Domain.Entities;
using SmnHelpDesk.Domain.TipoCriticidade;
using System.Net;
using System.Web.Http;

namespace SmnHelpDesk.Api.Controllers
{
    [RoutePrefix("api/chamado")]
    public class ChamadoController : BaseController
    {
        private readonly IChamadoService _chamadoService;
        private readonly IChamadoRepository _chamadoRepository;
        private readonly ITipoCriticidadeRepository _tipoCriticidadeRepository;
        private readonly IChamadoTipoRepository _chamadoTipoRepository;
        private readonly IChamadoTipoStatusRepository _chamadoTipoStatusRepository;
        private readonly Notification _notification;

        public ChamadoController(Notification notification, IChamadoService chamadoService, IChamadoRepository chamadoRepository,
            ITipoCriticidadeRepository tipoCriticidadeRepository, IChamadoTipoRepository chamadoTipoRepository, IChamadoTipoStatusRepository chamadoTipoStatusRepository)
        {
            _notification = notification;
            _chamadoService = chamadoService;
            _chamadoRepository = chamadoRepository;
            _tipoCriticidadeRepository = tipoCriticidadeRepository;
            _chamadoTipoRepository = chamadoTipoRepository;
            _chamadoTipoStatusRepository = chamadoTipoStatusRepository;
        }

        //Buscar os chamados para o grid de acordo com o idEmpresa ou senão passar busca todos chamados
        [HttpGet, Route("")]
        public IHttpActionResult Get(int? idEmpresa)
        {
            var chamados = _chamadoRepository.Get(idEmpresa);
            if (chamados == null)
                return BadRequest("Não foi encontrado nenhum chamado");
            return Ok(chamados);
        }

        //Buscar o chamado por id para edição
        [HttpGet, Route("{idChamado}")]
        public IHttpActionResult Get(int idChamado)
        {
            var chamado = _chamadoRepository.Get(idChamado);
            if (chamado == null)
                return BadRequest("Não foi encontrado nenhum chamado");
            return Ok(chamado);
        }

        //Buscar os tipos de criticidade
        [HttpGet, Route("TipoCriticidade")]
        public IHttpActionResult GetTipoCriticidade()
        {
            var tipoCriticidade = _tipoCriticidadeRepository.Get();
            if (tipoCriticidade == null)
                return BadRequest("Não foram encontrados os tipos de criticidade de chamado");
            return Ok(tipoCriticidade);
        }

        //Buscar os tipos de chamado
        [HttpGet, Route("ChamadoTipo")]
        public IHttpActionResult GetChamadoTipo()
        {
            var chamadoTipo = _chamadoTipoRepository.Get();
            if (chamadoTipo == null)
                return BadRequest("Não foram encontrados os tipos de chamados");
            return Ok(chamadoTipo);
        }

        //Buscar os tipos de status
        [HttpGet, Route("ChamadoTipoStatus")]
        public IHttpActionResult GetChamadoTipoStatus()
        {
            var chamadoTipoStatus = _chamadoTipoStatusRepository.Get();
            if (chamadoTipoStatus == null)
                return BadRequest("Não foram encontrados os tipos de status de chamado");
            return Ok(chamadoTipoStatus);
        }

        //Cadastra um novo chamado
        [HttpPost, Route("")]
        public IHttpActionResult Post(Chamado chamado)
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
        public IHttpActionResult Put(int id, Chamado chamado)
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
            //_chamadoRepository.OpenTransaction();
            _chamadoService.PutStatus(chamadoHistorico);

            if (_notification.Any)
            {
                //_chamadoRepository.RollbackTransaction();
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            }
            //_chamadoRepository.CommitTransaction();
            return Ok();
        }
    }
}
