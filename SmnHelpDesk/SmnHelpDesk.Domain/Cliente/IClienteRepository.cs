using SmnHelpDesk.Domain.Cliente.Dto;

namespace SmnHelpDesk.Domain.Cliente
{
    public interface IClienteRepository
    {
        ClienteDto Get(decimal cpf);
    }
}
