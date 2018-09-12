using SmnHelpDesk.Domain.TipoCriticidade.Dto;
using System.Collections.Generic;

namespace SmnHelpDesk.Domain.TipoCriticidade
{
    public interface ITipoCriticidadeRepository
    {
        IEnumerable<TipoCriticidadeDto> Get();
    }
}
