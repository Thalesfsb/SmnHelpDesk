using SmnHelpDesk.Domain.Chamado.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.Chamado
{
    public interface IChamadoRepository
    {
        IEnumerable<ChamadoDto> Get(int? idEmpresa);
        ChamadoDto Get(int id);
        int Post(ChamadoDto chamado);
        void Put(ChamadoDto chamado);
        int GetProximoNumeroChamado(int idEmpresa);
        void PostHistoricoStatus(ChamadoHistoricoStatusDto historicoStatus);
        void PutStatus(int idChamado, int idStatus, string descricaoMotivoCancel);
    }
}
