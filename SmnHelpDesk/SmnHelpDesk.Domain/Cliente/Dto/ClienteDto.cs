using System;

namespace SmnHelpDesk.Domain.Cliente.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeColaboradorCad { get; set; }
        public string NomeColaboradorAlt { get; set; }
        public decimal Cpf { get; set; }
        public int IdEmpresa { get; set; }
        public int IdColaboradorCad { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdColaboradorAlt { get; set; }
        public DateTime DataAlteracao { get; set; }
        public DateTime DataInativacao { get; set; }
    }
}
