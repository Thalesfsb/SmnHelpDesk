using Newtonsoft.Json;
using SmnHelpDesk.Domain.Colaborador.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmnHelpDesk.Domain.Chamado.Dto
{
    public class ChamadoDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int NumeroChamado { get; set; }
        public string NomeProblema { get; set; }
        public string Descricao { get; set; }
        public byte IdCriticidade { get; set; }
        public byte IdTipo { get; set; }
        public byte IdStatus { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeClienteCad { get; set; }
        public string NomeCriticidade { get; set; }
        public string NomeTipoStatus { get; set; }
        public int IdClienteCad { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? IdClienteAlt { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string DescricaoMotivoCancel { get; set; }
        public bool IsPendenteAnalise => IdStatus == 1;

        public IEnumerable<ColaboradorDto> Colaboradores { get; set; }

        public bool IsValid(Notification notification)
        {
            var camposObrigatorios = new List<string>();

            if (string.IsNullOrEmpty(NomeProblema))
                camposObrigatorios.Add("NomeProblema");

            if (IdTipo == default(int))
                camposObrigatorios.Add("Tipo");

            if (IdClienteCad == default(int))
                camposObrigatorios.Add("Codigo do Cliente");

            if (IdCriticidade == default(int))
                camposObrigatorios.Add("Criticidade");

            if (string.IsNullOrEmpty(Descricao))
                camposObrigatorios.Add("Descrição");

            if (camposObrigatorios.Any())
                notification.Add("Favor informar os campos " + string.Join(", ", camposObrigatorios));

            return notification.Any;
        }
    }
}
