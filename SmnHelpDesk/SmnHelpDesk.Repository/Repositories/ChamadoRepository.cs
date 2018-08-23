using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Chamado;
using SmnHelpDesk.Domain.Chamado.Dto;
using System;
using System.Collections.Generic;

namespace SmnHelpDesk.Repository.Repositories
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly Conexao _conexao;

        private readonly Notification _notification;

        public ChamadoRepository(Conexao conexao, Notification notification)
        {
            _conexao = conexao;
            _notification = notification;
        }

        private enum Procedures
        {
            GKSSP_InsChamado,
            GKSSP_SelChamado,
            GKSSP_SelChamados,
            GKSSP_UpdChamado
        }

        public IEnumerable<ChamadoDto> Get(int? idEmpresa, int? idStatus, int? idCliente)
        {
            var chamados = new List<ChamadoDto>();
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamados);
                _conexao.AddParameter("@IdEmpresa", idEmpresa);
                _conexao.AddParameter("@IdStatus", idStatus);
                _conexao.AddParameter("@IdCliente", idCliente);

                using (var r = _conexao.ExecuteReader())
                    while (r.Read())
                        chamados.Add(new ChamadoDto
                        {
                            Id = int.Parse(r["Id"].ToString()),
                            NumeroChamado = int.Parse(r["NumeroChamado"].ToString()),
                            NomeEmpresa = r["NomeEmpresa"].ToString(),
                            NomeClienteCad = r["NomeClienteCad"].ToString(),
                            NomeProblema = r["NomeProblema"].ToString(),
                            NomeCriticidade = r["NomeCriticidade"].ToString(),
                            NomeTipoStatus = r["NomeTipoStatus"].ToString()
                        });
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
            return chamados;
        }

        public ChamadoDto Get(int numeroChamado)
        {
            var chamado = new ChamadoDto();
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamado);
                _conexao.AddParameter("@NumeroChamado", numeroChamado);

                using (var r = _conexao.ExecuteReader())
                    if (r.Read())
                    {
                        chamado.Descricao = r["Descricao"].ToString();
                        chamado.NumeroChamado = int.Parse(r["NumeroChamado"].ToString());
                        chamado.NomeCriticidade = r["NomeCriticidade"].ToString();
                        chamado.NomeTipoStatus = r["NomeTipoStatus"].ToString();
                    }
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
            return chamado;
        }

        public void Post(ChamadoDto chamado)
        {
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_InsChamado);
                _conexao.AddParameter("NumeroChamado", chamado.NumeroChamado);
                _conexao.AddParameter("NomeProblema", chamado.NomeProblema);
                _conexao.AddParameter("Descricao", chamado.Descricao);
                _conexao.AddParameter("IdCriticidade", chamado.IdCriticidade);
                _conexao.AddParameter("IdTipo", chamado.IdTipo);
                _conexao.AddParameter("IdStatus", chamado.IdStatus);
                _conexao.AddParameter("IdClienteCad", chamado.IdClienteCad);
                _conexao.ExecuteNonQuery();
                _conexao.CloseConnection();
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
        }

        public void Put(ChamadoDto chamado)
        {
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_UpdChamado);
                _conexao.AddParameter("Nome", chamado.NomeProblema);
                _conexao.AddParameter("Descricao", chamado.Descricao);
                _conexao.AddParameter("IdCriticidade", chamado.IdCriticidade);
                _conexao.AddParameter("IdTipo", chamado.IdTipo);
                _conexao.AddParameter("IdStatus", chamado.IdStatus);
                _conexao.AddParameter("IdClienteAlt", chamado.IdClienteAlt);
                _conexao.ExecuteNonQuery();
                _conexao.CloseConnection();
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
        }
    }
}
