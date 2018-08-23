using System;

namespace SmnHelpDesk.Domain.Colaborador.Dto
{
    public class ColaboradorDto
    {
        public int Id { get; set; }
        public decimal Cpf { get; set; }
        public string Nome { get; set; }
        public string Logon { get; set; }
        public string Senha { get; set; }
        public int IdTipoColaborador { get; set; }
        public int IdColaboradorCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? IdColaboradorAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}
