using SmnHelpDesk.Domain.ChamadoTipoStatus;
using SmnHelpDesk.Domain.ChamadoTipoStatus.Dto;
using SmnHelpDesk.Repository.Infra.Extension;
using System.Collections.Generic;

namespace SmnHelpDesk.Repository.Repositories
{
    public class ChamadoTipoStatusRepository : IChamadoTipoStatusRepository
    {
        private readonly Conexao _conexao;
        public ChamadoTipoStatusRepository(Conexao conexao)
        {
            _conexao = conexao;
        }
        private enum Procedures
        {
            GKSSP_ChamadoTipoStatus
        }

        public IEnumerable<ChamadoTipoStatusDto> Get()
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_ChamadoTipoStatus);
            var tipoStatus = new List<ChamadoTipoStatusDto>();
            using (var r = _conexao.ExecuteReader())
                while (r.Read())
                    tipoStatus.Add(new ChamadoTipoStatusDto
                    {
                        Id = r.GetValue<byte>("Id"),
                        Nome = r.GetValue<string>("Nome")
                    });

            return tipoStatus;
        }
    }
}
