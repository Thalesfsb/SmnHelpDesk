using SmnHelpDesk.Domain.Empresa;
using SmnHelpDesk.Domain.Empresa.Dto;
using SmnHelpDesk.Repository.Infra.Extension;
using System;
using System.Collections.Generic;

namespace SmnHelpDesk.Repository.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly Conexao _conexao;

        public EmpresaRepository(Conexao conexao)
        {
            _conexao = conexao;
        }

        private enum Procedures
        {
            GKSSP_DelEmpresa,
            GKSSP_SelEmpresas,
            GKSSP_SelEmpresa,
            GKSSP_InsEmpresa,
            GKSSP_UpdEmpresa
        }

        public void Delete(decimal cnpj)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_DelEmpresa);
            _conexao.AddParameter("@Cnpj", cnpj);
            _conexao.ExecuteNonQuery();
        }

        public IEnumerable<EmpresaDto> GetAll(int? idCliente)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelEmpresas);
            _conexao.AddParameter("IdCliente", idCliente);

            var empresa = new List<EmpresaDto>();
            using (var r = _conexao.ExecuteReader())
                while (r.Read())
                    empresa.Add(new EmpresaDto
                    {
                        Id = r.GetValue<int>("Id"),
                        Cnpj = r.GetValue<decimal>("Cnpj"),
                        RazaoSocial = r.GetValue<string>("RazaoSocial"),
                        NomeFantasia = r.GetValue<string>("NomeFantasia"),
                        DataInativacao = r.GetValue<DateTime>("DataInativacao"),
                        Ddd = r.GetValue<byte>("Ddd"),
                        Numero = r.GetValue<int>("Numero"),
                        Cep = r.GetValue<int>("Cep"),
                        NomEndereco = r.GetValue<string>("NomEndereco"),
                        NumEndereco = r.GetValue<int>("NumEndereco"),
                        Bairro = r.GetValue<string>("Bairro"),
                        Complemento = r.GetValue<string>("Complemento"),
                        Cidade = r.GetValue<string>("Cidade"),
                        Uf = r.GetValue<string>("Uf"),
                        NomCliente = r.GetValue<string>("NomCliente")
                    });
            return empresa;
        }

        public EmpresaDto Get(decimal cnpj)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_SelEmpresa);
            _conexao.AddParameter("@Cnpj", cnpj);

            var empresa = new EmpresaDto();
            using (var r = _conexao.ExecuteReader())
                if (r.Read())
                {
                    empresa.Id = r.GetValue<int>("Id");
                    empresa.Cnpj = r.GetValue<decimal>("Cnpj");
                    empresa.RazaoSocial = r.GetValue<string>("RazaoSocial");
                    empresa.NomeFantasia = r.GetValue<string>("NomeFantasia");
                    empresa.DataInativacao = r.GetValue<DateTime>("DataInativacao");
                }
            return empresa;
        }

        public int Post(EmpresaDto empresa)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_InsEmpresa);
            _conexao.AddParameter("@Cnpj", empresa.Cnpj);
            _conexao.AddParameter("@RazaoSocial", empresa.RazaoSocial);
            _conexao.AddParameter("@NomeFantasia", empresa.NomeFantasia);
            _conexao.AddParameter("@IdColaboradorCadastro", empresa.IdColaboradorCadastro);
            _conexao.AddParameter("IdEnderecoPrincipal", empresa.IdEnderecoPrincipal);
            _conexao.AddParameter("IdTelefonePrincipal", empresa.IdTelefonePrincipal);
            return _conexao.ExecuteNonQueryWithReturn();
        }

        public void Put(EmpresaDto empresa)
        {
            _conexao.ExecuteProcedure(Procedures.GKSSP_UpdEmpresa);
            _conexao.AddParameter("@Id", empresa.Id);
            _conexao.AddParameter("@Cnpj", empresa.Cnpj);
            _conexao.AddParameter("@RazaoSocial", empresa.RazaoSocial);
            _conexao.AddParameter("@NomeFantasia", empresa.Cnpj);
            _conexao.AddParameter("@IdColaboradorAlteracao", empresa.IdColaboradorAlteracao);
            _conexao.AddParameter("IdEnderecoPrincipal", empresa.IdEnderecoPrincipal);
            _conexao.AddParameter("IdTelefonePrincipal", empresa.IdTelefonePrincipal);
            _conexao.ExecuteNonQuery();
        }
    }
}
