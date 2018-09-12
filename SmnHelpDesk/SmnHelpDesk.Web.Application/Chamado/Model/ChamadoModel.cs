using System;

namespace SmnHelpDesk.Web.Application.Chamado.Model
{
    public class ChamadoModel
    {
        public int Id { get; set; }
        public int NumeroChamado { get; set; }
        public string NomeProblema { get; set; }
        public string Descricao { get; set; }
        public byte IdCriticidade { get; set; }
        public byte IdTipo { get; set; }
        public byte IdStatus { get; set; }
        public string NomeClienteCad { get; set; }
        public string NomeCriticidade { get; set; }
        public string NomeTipoStatus { get; set; }
        public int IdClienteCad { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? IdClienteAlt { get; set; }
        public string DescricaoMotivoCancel { get; set; }
        public string NomeColaboradorPrincipal { get; set; }
        public string DataFormatada => $"{DataCadastro:dd-MM-yyyy}";
        public bool IsCadastro => Id == default(int);
    }
}
