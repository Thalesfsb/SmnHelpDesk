namespace SmnHelpDesk.Domain.Chamado.Dto
{
    public class ChamadoHistoricoStatusDto
    {
        public int IdChamado { get; set; }
        public int IdStatus { get; set; }
        public int? IdCliente { get; set; }
        public int? IdColaborador { get; set; }
    }
}
