using SmnHelpDesk.Domain.Chamado.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.Chamado
{
    public interface IChamadoRepository
    {
        IEnumerable<ChamadoDto> Get(int? idEmpresa);
        ChamadoDto Get(int id);
        int Post(Entities.Chamado chamado);
        void Put(Entities.Chamado chamado);
        int GetProximoNumeroChamado(int idEmpresa);
        void PostHistoricoStatus(ChamadoHistoricoStatusDto historicoStatus);
        void PutStatus(int idChamado, int idStatus, string descricaoMotivoCancel);
    }
}
