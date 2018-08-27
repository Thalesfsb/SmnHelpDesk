using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Empresa;
using SmnHelpDesk.Domain.Empresa.Dto;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace SmnHelpDesk.Api.Controllers
{
    public class EmpresaController : BaseController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IEmpresaService _empresaService;
        private readonly Notification _notification;

        public EmpresaController(IEmpresaRepository empresaRepository, IEmpresaService empresaService, Notification notification)
        {
            _empresaRepository = empresaRepository;
            _empresaService = empresaService;
            _notification = notification;
        }

        [HttpDelete, Route("{cnpj}")]
        public IHttpActionResult Delete(decimal cnpj)
        {
            _empresaRepository.Delete(cnpj);
            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }

        [HttpGet, Route("{idCliente}")]
        public IHttpActionResult Get(int idCliente)
        {
            var empresas = _empresaRepository.Get(idCliente);
            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok(empresas);
        }


        [HttpGet, Route("{cnpj}")]
        public IHttpActionResult Get(decimal cnpj)
        {

            _empresaService.IsValidCnpj(cnpj);
            var empresa = _empresaRepository.Get(cnpj);
            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok(empresa);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post(EmpresaDto empresa)
        {
            _empresaService.IsValidCnpj(empresa.Cnpj);
            _empresaRepository.Post(empresa);
            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }

        [HttpPut, Route("")]
        public IHttpActionResult Put(EmpresaDto empresa)
        {
            _empresaRepository.Put(empresa);
            if (_notification.Any)
                return Content(HttpStatusCode.BadRequest, _notification.Get);
            return Ok();
        }
    }
}