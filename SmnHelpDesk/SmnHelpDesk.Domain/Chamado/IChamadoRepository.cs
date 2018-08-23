using SmnHelpDesk.Domain.Chamado.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.Chamado
{
    public interface IChamadoRepository
    {
        void Post(ChamadoDto chamado);

        ChamadoDto Get(int numeroChamado);

        IEnumerable<ChamadoDto> Get(int? idEmpresa, int? idStatus, int? idCliente);

        void Put(ChamadoDto chamado);
    }


}
