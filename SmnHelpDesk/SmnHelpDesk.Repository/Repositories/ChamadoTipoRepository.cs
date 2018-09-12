using SmnHelpDesk.Domain.ChamadoTipo;
using SmnHelpDesk.Domain.ChamadoTipo.Dto;
using SmnHelpDesk.Repository.Infra.Extension;
using System.Collections.Generic;

namespace SmnHelpDesk.Repository.Repositories
{
    public class ChamadoTipoRepository : IChamadoTipoRepository
    {
        private readonly Conexao _conexao;
        public ChamadoTipoRepository(Conexao conexao)
        {
            _conexao = conexao;
        }
        private enum Procedures
        {
            GKSSP_SelChamadoTipos
        }

        public IEnumerable<TipoChamadoDto> Get()
        {
            var tiposChamado = new List<TipoChamadoDto>();
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelChamadoTipos);
            using (var r = _conexao.ExecuteReader())
                while (r.Read())
                    tiposChamado.Add(new TipoChamadoDto
                    {
                        Id = r.GetValue<byte>("Id"),
                        Nome = r.GetValue<string>("Nome")
                    });
            return tiposChamado;
        }
    }
}
