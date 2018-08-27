using SmnHelpDesk.Domain.Empresa.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.Empresa
{
    public interface IEmpresaRepository
    {
        int Post(EmpresaDto empresa);
        EmpresaDto Get(decimal cnpj);
        IEnumerable<EmpresaDto> GetAll(int? idCliente); // TODO: filtrar por cliente na proc
        void Put(EmpresaDto empresa);
        void Delete(decimal cnpj);
    }
}
