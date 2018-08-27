using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Empresa;
using SmnHelpDesk.Domain.Empresa.Dto;
using SmnHelpDesk.Domain.Endereco.Dto;
using SmnHelpDesk.Domain.Telefone.Dto;
using SmnHelpDesk.Repository.Infra.Extension;
using System;
using System.Collections.Generic;

namespace SmnHelpDesk.Repository.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly Conexao _conexao;

        private readonly Notification _notification;

        public EmpresaRepository(Conexao conexao, Notification notification)
        {
            _conexao = conexao;
            _notification = notification;
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
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_DelEmpresa);
                _conexao.AddParameter("@Cnpj", cnpj);
                _conexao.ExecuteNonQuery();
                _conexao.CloseConnection();
            }
            catch (Exception e)
            {

                _notification.Add(e.ToString());
            }

        }

        public IEnumerable<EmpresaDto> GetAll(int? idCliente)
        {
            var empresa = new List<EmpresaDto>();
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_SelEmpresas);
                using (var r = _conexao.ExecuteReader())
                {
                    while (r.Read())
                        empresa.Add(new EmpresaDto
                        {
                            Id = r.GetValue<int>("Id"),
                            Cnpj = r.GetValue<decimal>("Cnpj"),
                            RazaoSocial = r.GetValue<string>("RazaoSocial"),
                            NomeFantasia = r.GetValue<string>("NomeFantasia"),
                            DataInativacao = r.GetValue<DateTime>("DataInativacao"),
                            Telefone = new TelefoneDto
                            {
                                Ddd = r.GetValue<int>("DDD"),
                                Numero = r.GetValue<int>("Numero")
                            },
                            Endereco = new EnderecoDto
                            {
                                Id = r.GetValue<int>("Id"),
                                Cep = r.GetValue<int>("Cep"),
                                NomEndereco = r.GetValue<string>("Nom_Empresa"),
                                NumEndereco = r.GetValue<int>("Num_Endereco"),
                                Bairro = r.GetValue<string>("Bairro"),
                                Complemento = r.GetValue<string>("Complemento"),
                                Cidade = r.GetValue<string>("Cidade"),
                                Uf = r.GetValue<string>("Uf")
                            }

                        });
                }
            }
            catch (Exception e)
            {

                _notification.Add(e.ToString());
            }
            return empresa;
        }

        public EmpresaDto Get(decimal cnpj)
        {
            var empresa = new EmpresaDto();
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_SelEmpresa);
                _conexao.AddParameter("@Cnpj", cnpj);
                using (var r = _conexao.ExecuteReader())
                {
                    if (r.Read())
                    {
                        empresa.Id = r.GetValue<int>("Id");
                        empresa.Cnpj = r.GetValue<decimal>("Cnpj");
                        empresa.RazaoSocial = r.GetValue<string>("RazaoSocial");
                        empresa.NomeFantasia = r.GetValue<string>("NomeFantasia");
                        empresa.DataInativacao = r.GetValue<DateTime>("DataInativacao");
                    }
                }
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
            return empresa;
        }

        public int Post(EmpresaDto empresa)
        {
            var retorno = 0;
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_InsEmpresa);
                _conexao.AddParameter("@Cnpj", empresa.Cnpj);
                _conexao.AddParameter("@RazaoSocial", empresa.RazaoSocial);
                _conexao.AddParameter("@NomeFantasia", empresa.NomeFantasia);
                _conexao.AddParameter("@IdColaboradorCadastro", empresa.IdColaboradorCadastro);
                _conexao.AddParameter("IdEnderecoPrincipal", empresa.IdEnderecoPrincipal);
                _conexao.AddParameter("IdTelefonePrincipal", empresa.IdTelefonePrincipal);
                retorno = _conexao.ExecuteNonQueryWithReturn<int>();
                _conexao.CloseConnection();
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
            return retorno;
        }

        public void Put(EmpresaDto empresa)
        {
            try
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
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
        }
    }
}
