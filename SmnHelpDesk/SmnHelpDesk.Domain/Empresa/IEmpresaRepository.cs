using SmnHelpDesk.Domain.Empresa.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.Empresa
{
    public interface IEmpresaRepository
    {
        void Post(EmpresaDto empresa);
        EmpresaDto Get(decimal cnpj);
        IEnumerable<EmpresaDto> Get();
        void Put(EmpresaDto empresa);
        void Delete(decimal cnpj);

    }
}
