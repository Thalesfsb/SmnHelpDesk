using SmnHelpDesk.Domain.Chamado.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.Chamado
{
    public interface IChamadoRepository
    {
        int Post(ChamadoDto chamado);
        ChamadoDto Get(int id);
        IEnumerable<ChamadoDto> Get(int? idEmpresa, int? idStatus, int? idCliente);
        void Put(ChamadoDto chamado);
        void Put(int id, string descricaoMotivoCancel);
        int GetProximoNumero(int idEmpresa);
        void PostHistoricoStatus(ChamadoHistoricoStatusDto historicoStatus);
        void PutStatus(int idChamado, int idStatus);
    }
}
