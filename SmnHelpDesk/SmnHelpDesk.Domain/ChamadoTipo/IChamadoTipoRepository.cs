using SmnHelpDesk.Domain.ChamadoTipo.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.ChamadoTipo
{
    public interface IChamadoTipoRepository
    {
        IEnumerable<TipoChamadoDto> Get();
    }
}
