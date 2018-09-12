using SmnHelpDesk.Domain.Chamado.Dto;

namespace SmnHelpDesk.Domain.Chamado
{
    public interface IChamadoService
    {
        bool Exists(int id);
        void Post(Entities.Chamado chamado);
        void Put(Entities.Chamado chamado);
        void PutStatus(ChamadoHistoricoStatusDto chamadoHistoricoStatus);
    }
}
