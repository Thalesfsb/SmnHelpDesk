namespace SmnHelpDesk.Domain.Empresa
{
    public interface IEmpresaService
    {
        bool IsValidCnpj(decimal cnpj);
        bool IsValidCpf(decimal cpf);
    }
}
