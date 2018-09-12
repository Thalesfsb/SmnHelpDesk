using SmnHelpDesk.Domain.Chamado.Dto;
using SmnHelpDesk.Domain.Cliente;
using SmnHelpDesk.Domain.Empresa;
using System.Linq;

namespace SmnHelpDesk.Domain.Chamado
{
    public class ChamadoService : IChamadoService
    {
        private readonly Notification _notification;

        private readonly IChamadoRepository _chamadoRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IClienteRepository _clienteRepository;

        public ChamadoService(Notification notification, IChamadoRepository chamadoRepository, IEmpresaRepository empresaRepository, IClienteRepository clienteRepository)
        {
            _notification = notification;
            _chamadoRepository = chamadoRepository;
            _empresaRepository = empresaRepository;
            _clienteRepository = clienteRepository;
        }

        public bool Exists(int id)
        {
            var chamado = _chamadoRepository.Get(id);
            return chamado != null;
        }

        public void Post(Entities.Chamado chamado)
        {
            // validando o chamado
            if (!chamado.IsValid(_notification))
                return;

            var cliente = _clienteRepository.Get(chamado.NumeroCpfCliente);
            if (cliente == null)
            {
                _notification.Add("Cliente não cadastrado");
                return;
            }

            // buscando a empresa do cliente
            var empresa = _empresaRepository.GetAll(cliente.Id).ToList().FirstOrDefault(x => x.Id == chamado.IdEmpresa);
            if (empresa == null)
            {
                _notification.Add("A empresa do cliente não foi encontrada");
                return;
            }

            // buscando o próximo numero para o chamado
            chamado.NumeroChamado = _chamadoRepository.GetProximoNumeroChamado(empresa.Id);

            // cadastrando o chamado
            chamado.IdStatus = 1; // Pendente de Análise
            chamado.IdClienteCad = cliente.Id;
            var idChamado = _chamadoRepository.Post(chamado);

            // inserindo historico de status
            _chamadoRepository.PostHistoricoStatus(new ChamadoHistoricoStatusDto
            {
                IdChamado = idChamado,
                IdStatus = chamado.IdStatus,
                IdCliente = chamado.IdClienteCad
            });
        }

        public void Put(Entities.Chamado chamado)
        {
            var dadosChamado = _chamadoRepository.Get(chamado.Id);
            if (dadosChamado == null)
            {
                _notification.Add("Chamado não encontrado");
                return;
            }
            var cliente = _clienteRepository.Get(chamado.NumeroCpfCliente);
            if (cliente == null)
            {
                _notification.Add("Cliente não cadastrado");
                return;
            }
            if (!dadosChamado.IsPendenteAnalise)
            {
                _notification.Add("Não é possivel editar chamdados que não estejam pendentes de análise.");
                return;
            }
            chamado.IdClienteAlt = cliente.Id;
            chamado.IdStatus = dadosChamado.IdStatus;
            _chamadoRepository.Put(chamado);
        }

        public void PutStatus(ChamadoHistoricoStatusDto chamadoHistoricoStatus)
        {
            var cliente = _clienteRepository.Get(chamadoHistoricoStatus.NumeroCpfCliente);
            if (cliente == null)
            {
                _notification.Add("Cliente não cadastrado");
                return;
            }
            chamadoHistoricoStatus.IdCliente = cliente.Id;
            chamadoHistoricoStatus.IsValid(_notification);

            if (_notification.Any)
                return;

            var dadosChamado = _chamadoRepository.Get(chamadoHistoricoStatus.IdChamado);
            if (dadosChamado == null)
            {
                _notification.Add("Chamado não encontrado");
                return;
            }
            if (chamadoHistoricoStatus.IsCancel && dadosChamado.IdStatus != 1 && dadosChamado.IdStatus != 2)
            {
                _notification.Add("Não é possível cancelar um chamado que já não está mais em análise");
                return;
            }
            _chamadoRepository.PutStatus(chamadoHistoricoStatus.IdChamado, chamadoHistoricoStatus.IdStatus, chamadoHistoricoStatus.DescricaoMotivoCancel);
            _chamadoRepository.PostHistoricoStatus(chamadoHistoricoStatus);

            if (chamadoHistoricoStatus.IdStatus == 2) // Em análise
            {
                _chamadoRepository.Put(new Entities.Chamado
                {
                    Id = dadosChamado.Id,
                    NomeProblema = dadosChamado.NomeProblema,
                    IdStatus = chamadoHistoricoStatus.IdStatus,
                    IdTipo = dadosChamado.IdTipo,
                    IdCriticidade = dadosChamado.IdCriticidade,
                    Descricao = dadosChamado.Descricao,
                    IdClienteAlt = dadosChamado.IdClienteAlt,
                    IdColaboradorPrincipal = chamadoHistoricoStatus.IdColaborador
                    //Post na tabela de ColaboradorChamado
                });
            }
        }
    }
}
