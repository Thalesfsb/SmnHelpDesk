using SmnHelpDesk.Domain.ChamadoTipoStatus.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.ChamadoTipoStatus
{
    public interface IChamadoTipoStatusRepository
    {
        IEnumerable<ChamadoTipoStatusDto> Get();
    }
}
