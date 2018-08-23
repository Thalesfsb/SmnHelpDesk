namespace SmnHelpDesk.Domain.Endereco.Dto
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public int Cep { get; set; }
        public string NomEndereco { get; set; }
        public int NumEndereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }

    }
}
