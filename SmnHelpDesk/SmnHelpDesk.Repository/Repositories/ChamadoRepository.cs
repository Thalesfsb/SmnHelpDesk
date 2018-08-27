using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.Chamado.Dto;
using SmnHelpDesk.Repository.Infra.Extension;
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
            GKSSP_UpdDescricaoMotivoCancel,
            GKSFNC_GetProximoNumeroChamado,
            GKSSP_SelChamadoHistoricoStatus,
            GKSSP_SelTipoStatus,
            GKSSP_UpdChamadoHistoricoStatus
        }

        public IEnumerable<ChamadoDto> Get(int? idEmpresa, int? idStatus, int? idCliente)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamados);
            _conexao.AddParameter("@IdEmpresa", idEmpresa);
            _conexao.AddParameter("@IdStatus", idStatus);
            _conexao.AddParameter("@IdCliente", idCliente);

            var chamados = new List<ChamadoDto>();
            using (var r = _conexao.ExecuteReader())
                while (r.Read())
                    chamados.Add(new ChamadoDto
                    {
                        Id = r.GetValue<int>("Id"),
                        NumeroChamado = r.GetValue<int>("NumeroChamado"),
                        NomeEmpresa = r.GetValue<string>("NomeEmpresa"),
                        NomeClienteCad = r.GetValue<string>("NomeClienteCad"),
                        NomeProblema = r.GetValue<string>("NomeProblema"),
                        NomeCriticidade = r.GetValue<string>("NomeCriticidade"),
                        NomeTipoStatus = r.GetValue<string>("NomeTipoStatus"),
                        DescricaoMotivoCancel = r.GetValue<string>("DescricaoMotivoCancel")
                    });
            return chamados;
        }

        public ChamadoDto Get(int id)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamado);
            _conexao.AddParameter("@Id", id);
            using (var r = _conexao.ExecuteReader())
                return !r.Read()
                    ? null
                    : new ChamadoDto
                    {
                        Id = r.GetValue<int>("Id"),
                        IdClienteCad = r.GetValue<int>("IdClienteCad"),
                        IdCriticidade = r.GetValue<byte>("IdCriticidade"),
                        IdTipo = r.GetValue<byte>("IdTipo"),
                        IdStatus = r.GetValue<byte>("IdStatus"),
                        IdClienteAlt = r.GetValue<int>("IdClienteAlt"),
                        Descricao = r.GetValue<string>("Descricao"),
                        NumeroChamado = r.GetValue<int>("NumeroChamado"),
                        NomeCriticidade = r.GetValue<string>("NomeCriticidade"),
                        NomeTipoStatus = r.GetValue<string>("NomeTipoStatus"),
                        DescricaoMotivoCancel = r.GetValue<string>("DescricaoMotivoCancel")
                    };
        }

        public int GetProximoNumero(int idEmpresa)
        {
            _conexao.ExecuteProcedure(Procedures.GKSFNC_GetProximoNumeroChamado);
            _conexao.AddParameter("@IdEmpresa", idEmpresa);
            return _conexao.ExecuteNonQueryWithReturn();
        }

        public int Post(ChamadoDto chamado)
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
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamadoHistoricoStatus);
            _conexao.AddParameter("IdChamado", historicoStatus.IdChamado);
            _conexao.AddParameter("IdStatus", historicoStatus.IdStatus);
            _conexao.AddParameter("IdColaborador", historicoStatus.IdColaborador);
            _conexao.AddParameter("IdCliente", historicoStatus.IdCliente);
            _conexao.ExecuteNonQuery();
        }

        public void Put(ChamadoDto chamado)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_UpdChamado);
            _conexao.AddParameter("Nome", chamado.NomeProblema);
            _conexao.AddParameter("Descricao", chamado.Descricao);
            _conexao.AddParameter("IdCriticidade", chamado.IdCriticidade);
            _conexao.AddParameter("IdTipo", chamado.IdTipo);
            _conexao.AddParameter("IdStatus", chamado.IdStatus);
            _conexao.AddParameter("IdClienteAlt", chamado.IdClienteAlt);
            _conexao.ExecuteNonQuery();
        }

        public void Put(int id, string descricaoMotivoCancel)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_UpdDescricaoMotivoCancel);
            _conexao.AddParameter("@Id", id);
            _conexao.AddParameter("@DescricaoMotivoCancel", descricaoMotivoCancel);
            _conexao.ExecuteNonQuery();
        }

        public void PutStatus(int idChamado, int idStatus)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_UpdChamadoHistoricoStatus);
            _conexao.AddParameter("Id", idChamado);
            _conexao.AddParameter("IdStatus", idStatus);
            _conexao.ExecuteNonQuery();
        }
    }
}
