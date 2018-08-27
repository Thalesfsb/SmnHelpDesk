using SmnHelpDesk.Domain.Chamado.Dto;

namespace SmnHelpDesk.Domain.Chamado
{
    public interface IChamadoService
    {
        bool Exists(int id);
        void Post(ChamadoDto chamado);
        void Put(ChamadoDto chamado);
        void PutStatus(ChamadoHistoricoStatusDto chamadoHistoricoStatus);
    }
}
