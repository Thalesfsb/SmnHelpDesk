using SmnHelpDesk.Domain.TipoCriticidade;
using SmnHelpDesk.Domain.TipoCriticidade.Dto;
using SmnHelpDesk.Repository.Infra.Extension;
using System.Collections.Generic;

namespace SmnHelpDesk.Repository.Repositories
{
    public class TipoCriticidadeRepository : ITipoCriticidadeRepository
    {
        private readonly Conexao _conexao;
        public TipoCriticidadeRepository(Conexao conexao)
        {
            _conexao = conexao;
        }
        private enum Procedures
        {
            GKSSP_SelTipoCriticidade
        }

        public IEnumerable<TipoCriticidadeDto> Get()
        {
            var tiposCriticidade = new List<TipoCriticidadeDto>();
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelTipoCriticidade);

            using (var r = _conexao.ExecuteReader())
                while (r.Read())
                    tiposCriticidade.Add(new TipoCriticidadeDto
                    {
                        Id = r.GetValue<byte>("Id"),
                        Nome = r.GetValue<string>("Nome")
                    });
            return tiposCriticidade;
        }
    }
}
