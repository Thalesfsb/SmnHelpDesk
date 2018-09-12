using SmnHelpDesk.Domain.Cliente;
using SmnHelpDesk.Domain.Cliente.Dto;
using SmnHelpDesk.Repository.Infra.Extension;
using System;

namespace SmnHelpDesk.Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Conexao _conexao;
        public ClienteRepository(Conexao conexao)
        {
            _conexao = conexao;
        }
        private enum Procedures
        {
            GKSSP_SelCliente
        }

        public ClienteDto Get(decimal cpf)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelCliente);
            _conexao.AddParameter("Cpf", cpf);
            using (var r = _conexao.ExecuteReader())
            {
                return !r.Read()
                    ? null
                    : new ClienteDto
                    {
                        Id = r.GetValue<int>("Id"),
                        Cpf = r.GetValue<decimal>("Cpf"),
                        NomeEmpresa = r.GetValue<string>("NomeEmpresa"),
                        Nome = r.GetValue<string>("Nome"),
                        DataCadastro = r.GetValue<DateTime>("DataCadastro"),
                        NomeColaboradorCad = r.GetValue<string>("NomeColaboradorCad"),
                        DataAlteracao = r.GetValue<DateTime>("DataAlteracao"),
                        NomeColaboradorAlt = r.GetValue<string>("NomeColaboradorAlt"),
                        DataInativacao = r.GetValue<DateTime>("DataInativacao")
                    };
            }
        }
    }
}
