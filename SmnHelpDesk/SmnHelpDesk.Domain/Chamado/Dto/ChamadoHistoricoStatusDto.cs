using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SmnHelpDesk.Domain.Chamado.Dto
{
    public class ChamadoHistoricoStatusDto
    {
        [JsonIgnore]
        public int IdChamado { get; set; }
        public decimal NumeroCpfCliente { get; set; }
        public byte IdStatus { get; set; }
        public int IdCliente { get; set; }
        public int IdColaborador { get; set; }
        public string DescricaoMotivoCancel { get; set; }
        [JsonIgnore]
        public bool IsCancel => IdStatus == 7;

        public bool IsValid(Notification notification)
        {
            if ((IdCliente == default(int) && IdColaborador == default(int))
                || (IdCliente != default(int) && IdColaborador != default(int)))
                notification.Add("Favor informar o cliente ou colaborador.");

            if (IsCancel && IdCliente == default(int))
                notification.Add("Somente clientes podem cancelar chamados.");

            if (IsCancel && string.IsNullOrEmpty(DescricaoMotivoCancel))
                notification.Add("É preciso cadastrar um motivo de cancelamento do chamado.");

            var camposObrigatorios = new List<string>();

            if (IdStatus == default(byte))
                camposObrigatorios.Add("Status");

            if (IdChamado == default(byte))
                camposObrigatorios.Add("Chamado");

            if (camposObrigatorios.Any())
                notification.Add("Favor informar os campos: " + string.Join(", ", camposObrigatorios));

            return !notification.Any;
        }
    }
}

