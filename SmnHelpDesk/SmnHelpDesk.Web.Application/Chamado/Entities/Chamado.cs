namespace SmnHelpDesk.Web.Application.Chamado.Entities
{
    public class Chamado
    {
        public int Id { get; set; }
        public int NumeroChamado { get; set; }
        public string NomeProblema { get; set; }
        public string Descricao { get; set; }
        public byte IdCriticidade { get; set; }
        public byte IdTipo { get; set; }
        public byte IdStatus { get; set; }
        public int IdClienteCad { get; set; }
        public int? IdClienteAlt { get; set; }
        public int? IdColaboradorPrincipal { get; set; }
        public decimal NumeroCpfCliente { get; set; }
        public int IdEmpresa { get; set; }
        public string DescricaoMotivoCancel { get; set; }
    }
}
