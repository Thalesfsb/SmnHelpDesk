namespace SmnHelpDesk.Domain.Telefone.Dto
{
    public class TelefoneDto
    {
        public int Id { get; set; }
        public TelefoneTipoDto TelefoneTipo { get; set; }
        public int Ddd { get; set; }
        public int Numero { get; set; }
        public int Ramal { get; set; }
    }
}
