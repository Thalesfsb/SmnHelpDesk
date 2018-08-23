using SmnHelpDesk.Domain.Endereco.Dto;
using SmnHelpDesk.Domain.Telefone.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmnHelpDesk.Domain.Empresa.Dto
{
    public class EmpresaDto
    {
        public int Id { get; set; }
        public int IdEnderecoPrincipal { get; set; }
        public int IdTelefonePrincipal { get; set; }
        public decimal Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int IdColaboradorCadastro { get; set; }
        public string NomeColaboradorCad { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? IdColaboradorAlteracao { get; set; }
        public string NomeColaboradorAlt { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public EnderecoDto Endereco { get; set; }
        public TelefoneDto Telefone { get; set; }

        public bool Ativo => !DataInativacao.HasValue;

        public bool IsValid(Notification notification)
        {
            var camposObrigatorios = new List<string>();

            if (Cnpj == default(decimal))
                camposObrigatorios.Add("Cnpj");

            if (string.IsNullOrEmpty(RazaoSocial))
                camposObrigatorios.Add("RazaoSocial");

            if (string.IsNullOrEmpty(NomeFantasia))
                camposObrigatorios.Add("NomeFantasia");

            if (camposObrigatorios.Any())
                notification.Add("Favor informar os campos: " + string.Join(", ", camposObrigatorios));

            return !notification.Any;

        }
    }
}
