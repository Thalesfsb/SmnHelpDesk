using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.Chamado.Dto;
using SmnHelpDesk.Domain.Entities;
using SmnHelpDesk.Repository.Infra.Extension;
using System;
using System.Collections.Generic;

namespace SmnHelpDesk.Repository.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly Conexao _conexao;
        public ChamadoRepository(Conexao conexao)
        {
            _conexao = conexao;
        }
        private enum Procedures
        {
            GKSSP_InsChamado,
            GKSSP_SelChamado,
            GKSSP_SelChamados,
            GKSSP_UpdChamado,
            GKSSP_InsChamadoHistoricoStatus,
            GKSSP_UpdChamadoStatus,
            GKSFNC_GetProximoNumeroChamado
        }

        public IEnumerable<ChamadoDto> Get(int? idEmpresa)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamados);
            _conexao.AddParameter("@IdEmpresa", idEmpresa);

            var chamados = new List<ChamadoDto>();
            using (var r = _conexao.ExecuteReader())
                while (r.Read())
                    chamados.Add(new ChamadoDto
                    {
                        Id = r.GetValue<int>("Id"),
                        IdStatus = r.GetValue<byte>("IdStatus"),
                        NumeroChamado = r.GetValue<int>("NumeroChamado"),
                        NomeEmpresa = r.GetValue<string>("NomeEmpresa"),
                        NomeClienteCad = r.GetValue<string>("NomeClienteCad"),
                        NomeProblema = r.GetValue<string>("NomeProblema"),
                        NomeCriticidade = r.GetValue<string>("NomeCriticidade"),
                        NomeTipoStatus = r.GetValue<string>("NomeTipoStatus"),
                        DataCadastro = r.GetValue<DateTime>("DataCadastro")
                    });
            return chamados;
        }

        public ChamadoDto Get(int idChamado)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamado);
            _conexao.AddParameter("@Id", idChamado);
            using (var r = _conexao.ExecuteReader())
                return !r.Read()
                    ? null
                    : new ChamadoDto
                    {
                        Id = r.GetValue<int>("Id"),
                        NomeProblema = r.GetValue<string>("NomeProblema"),
                        IdTipo = r.GetValue<byte>("IdTipo"),
                        NumeroChamado = r.GetValue<int>("NumeroChamado"),
                        IdCriticidade = r.GetValue<byte>("IdCriticidade"),
                        Descricao = r.GetValue<string>("Descricao"),
                        IdStatus = r.GetValue<byte>("IdStatus"),
                        NomeClienteCad = r.GetValue<string>("NomeClienteCad"),
                        NomeCriticidade = r.GetValue<string>("NomeCriticidade")
                    };
        }

        public int GetProximoNumeroChamado(int idEmpresa)
        {
            _conexao.ExecuteProcedure(Procedures.GKSFNC_GetProximoNumeroChamado);
            _conexao.AddParameter("@IdEmpresa", idEmpresa);
            return _conexao.ExecuteNonQueryWithReturn();
        }

        public int Post(Chamado chamado)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_InsChamado);
            _conexao.AddParameter("NumeroChamado", chamado.NumeroChamado);
            _conexao.AddParameter("NomeProblema", chamado.NomeProblema);
            _conexao.AddParameter("Descricao", chamado.Descricao);
            _conexao.AddParameter("IdCriticidade", chamado.IdCriticidade);
            _conexao.AddParameter("IdTipo", chamado.IdTipo);
            _conexao.AddParameter("IdStatus", chamado.IdStatus);
            _conexao.AddParameter("IdClienteCad", chamado.IdClienteCad);
            return _conexao.ExecuteNonQueryWithReturn();
        }

        public void PostHistoricoStatus(ChamadoHistoricoStatusDto historicoStatus)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_InsChamadoHistoricoStatus);
            _conexao.AddParameter("IdChamado", historicoStatus.IdChamado);
            _conexao.AddParameter("IdStatus", historicoStatus.IdStatus);
            _conexao.AddParameter("IdColaborador", historicoStatus.IdColaborador == default(int) ? (int?)null : historicoStatus.IdColaborador);
            _conexao.AddParameter("IdCliente", historicoStatus.IdCliente == default(int) ? (int?)null : historicoStatus.IdCliente);
            _conexao.ExecuteNonQuery();
        }

        public void Put(Chamado chamado)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_UpdChamado);
            _conexao.AddParameter("Id", chamado.Id);
            _conexao.AddParameter("NomeProblema", chamado.NomeProblema);
            _conexao.AddParameter("Descricao", chamado.Descricao);
            _conexao.AddParameter("IdCriticidade", chamado.IdCriticidade);
            _conexao.AddParameter("IdTipo", chamado.IdTipo);
            _conexao.AddParameter("IdStatus", chamado.IdStatus);
            _conexao.AddParameter("IdClienteAlt", chamado.IdClienteAlt);
            //_conexao.AddParameter("IdColaboradorPrincipal", chamado.IdColaboradorPrincipal); //TODO: Como pegar colaborador
            _conexao.ExecuteNonQuery();
        }

        public void PutStatus(int idChamado, int idStatus, string descricaoMotivoCancel)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_UpdChamadoStatus);
            _conexao.AddParameter("IdChamado", idChamado);
            _conexao.AddParameter("IdStatus", idStatus);
            _conexao.AddParameter("DescricaoMotivoCancel", idStatus == 7 ? descricaoMotivoCancel : null);
            _conexao.ExecuteNonQuery();
        }
    }
}
