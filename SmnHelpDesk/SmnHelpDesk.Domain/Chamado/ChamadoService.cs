﻿using SmnHelpDesk.Domain.Chamado.Dto;
using SmnHelpDesk.Domain.Empresa;
using System.Linq;

namespace SmnHelpDesk.Domain.Chamado
{
    public class ChamadoService : IChamadoService
    {
        private readonly Notification _notification;

        private readonly IChamadoRepository _chamadoRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public ChamadoService(Notification notification, IChamadoRepository chamadoRepository, IEmpresaRepository empresaRepository)
        {
            _notification = notification;
            _chamadoRepository = chamadoRepository;
            _empresaRepository = empresaRepository;
        }

        public bool Exists(int id)
        {
            var chamado = _chamadoRepository.Get(id);
            return chamado != null;
        }

        public void Post(ChamadoDto chamado)
        {
            // validando o chamado
            if (!chamado.IsValid(_notification))
                return;

            // buscando a empresa do cliente
            var empresas = _empresaRepository.GetAll(chamado.IdClienteCad).ToList();
            if (!empresas.Any())
            {
                _notification.Add("A empresa do cliente não foi encontrada");
                return;
            }

            // buscando o próximo numero para o chamado
            var numeroChamado = _chamadoRepository.GetProximoNumero(empresas.First().Id);
            chamado.NumeroChamado = numeroChamado;

            // cadastrando o chamado
            var idChamado = _chamadoRepository.Post(chamado);

            // inserindo historico de status
            _chamadoRepository.PostHistoricoStatus(new ChamadoHistoricoStatusDto
            {
                IdChamado = idChamado,
                IdStatus = 1, // Pendente de Análise
                IdCliente = chamado.IdClienteCad
            });
        }

        public void Put(ChamadoDto chamado)
        {
            if (!chamado.IsValid(_notification))
                return;

            var dadosChamado = _chamadoRepository.Get(chamado.Id);
            if (dadosChamado == null)
            {
                _notification.Add("Chamado não encontrado");
                return;
            }

            if (!dadosChamado.IsPendenteAnalise)
            {
                _notification.Add("Não é possivel editar chamdados que não estejam pendentes de análise.");
                return;
            }

            _chamadoRepository.Put(chamado);
        }

        public void PutStatus(ChamadoHistoricoStatusDto chamadoHistoricoStatus)
        {
            _chamadoRepository.PutStatus(chamadoHistoricoStatus.IdChamado, chamadoHistoricoStatus.IdStatus);

            // inserindo historico de status
            _chamadoRepository.PostHistoricoStatus(chamadoHistoricoStatus);
        }
    }
}
