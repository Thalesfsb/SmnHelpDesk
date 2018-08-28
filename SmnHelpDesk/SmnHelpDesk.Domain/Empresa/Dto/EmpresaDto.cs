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
        public string NomCliente { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? IdColaboradorAlteracao { get; set; }
        public string NomeColaboradorAlt { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public int Cep { get; set; }
        public string NomEndereco { get; set; }
        public int NumEndereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public byte Ddd { get; set; }
        public int Numero { get; set; }
        public int Ramal { get; set; }

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
