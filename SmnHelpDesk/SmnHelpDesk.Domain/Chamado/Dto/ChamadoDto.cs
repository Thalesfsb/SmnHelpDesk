using System;
using System.Collections.Generic;
using System.Linq;

namespace SmnHelpDesk.Domain.Chamado.Dto
{
    public class ChamadoDto
    {
        public int Id { get; set; }
        public int NumeroChamado { get; set; }
        public string NomeProblema { get; set; }
        public string Descricao { get; set; }
        public int IdCriticidade { get; set; }
        public int IdTipo { get; set; }
        public int IdStatus { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeClienteCad { get; set; }
        public string NomeCriticidade { get; set; }
        public string NomeTipoStatus { get; set; }
        public int IdClienteCad { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? IdClienteAlt { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public bool IsValid(Notification notification)
        {
            var camposObrigatorios = new List<string>();

            if (string.IsNullOrEmpty(NomeProblema))
                camposObrigatorios.Add("NomeProblema");

            if (string.IsNullOrEmpty(NomeTipoStatus))
                camposObrigatorios.Add("Tipo");

            if (NumeroChamado == default(int))
                camposObrigatorios.Add("NumeroChamado");

            if (string.IsNullOrEmpty(NomeCriticidade))
                camposObrigatorios.Add("Criticidade");

            if (string.IsNullOrEmpty(Descricao))
                camposObrigatorios.Add("Descrição");

            if (camposObrigatorios.Any())
                notification.Add("Favor informar os campos " + string.Join(", ", camposObrigatorios));

            return notification.Any;
        }
    }
}
