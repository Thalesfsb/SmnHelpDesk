using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SmnHelpDesk.Domain.Entities
{
    public class Chamado
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int NumeroChamado { get; set; }
        public string NomeProblema { get; set; }
        public string Descricao { get; set; }
        public byte IdCriticidade { get; set; }
        public byte IdTipo { get; set; }
        [JsonIgnore]
        public byte IdStatus { get; set; }
        public int IdClienteCad { get; set; }
        public int? IdClienteAlt { get; set; }
        public int? IdColaboradorPrincipal { get; set; }
        public decimal NumeroCpfCliente { get; set; }
        public int IdEmpresa { get; set; }

        public bool IsValid(Notification notification)
        {
            var camposObrigatorios = new List<string>();

            if (string.IsNullOrEmpty(NomeProblema))
                camposObrigatorios.Add("NomeProblema");

            if (IdTipo == default(int))
                camposObrigatorios.Add("Tipo");

            if (NumeroCpfCliente == default(int))
                camposObrigatorios.Add("NumeroCpfCliente");

            if (IdEmpresa == default(int))
                camposObrigatorios.Add("IdEmpresa");

            if (IdCriticidade == default(int))
                camposObrigatorios.Add("Criticidade");

            if (string.IsNullOrEmpty(Descricao))
                camposObrigatorios.Add("Descrição");

            if (camposObrigatorios.Any())
                notification.Add("Favor informar os campos: " + string.Join(", ", camposObrigatorios));

            return !notification.Any;
        }
    }
}
