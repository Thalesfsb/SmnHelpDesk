using SmnHelpDesk.Domain;
using SmnHelpDesk.Domain.Empresa;
using SmnHelpDesk.Domain.Empresa.Dto;
using SmnHelpDesk.Domain.Endereco.Dto;
using SmnHelpDesk.Domain.Telefone.Dto;
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

        public IEnumerable<EmpresaDto> Get()
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
                            Id = int.Parse(r["Id"].ToString()),
                            Cnpj = decimal.Parse(r["Cnpj"].ToString()),
                            RazaoSocial = r["RazaoSocial"].ToString(),
                            NomeFantasia = r["NomeFantasia"].ToString(),
                            DataInativacao = DateTime.Parse(r["DataInativacao"].ToString()),
                            Telefone = new TelefoneDto
                            {
                                Ddd = int.Parse(r["DDD"].ToString()),
                                Numero = int.Parse(r["Numero"].ToString())
                            },
                            Endereco = new EnderecoDto
                            {
                                Cep = int.Parse(r["Cep"].ToString()),
                                NomEndereco = r["Nom_Endereco"].ToString(),
                                NumEndereco = int.Parse(r["Num_Endereco"].ToString()),
                                Bairro = r["Bairro"].ToString(),
                                Complemento = r["Complemento"].ToString(),
                                Cidade = r["Cidade"].ToString(),
                                Uf = r["Uf"].ToString(),
                                Id = int.Parse(r["Id"].ToString())
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

                        empresa.Id = int.Parse(r["Id"].ToString());
                        empresa.Cnpj = decimal.Parse(r["Cnpj"].ToString());
                        empresa.RazaoSocial = r["RazaoSocial"].ToString();
                        empresa.NomeFantasia = r["NomeFantasia"].ToString();
                        empresa.DataInativacao = DateTime.Parse(r["DataInativacao"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
            return empresa;
        }

        public void Post(EmpresaDto empresa)
        {
            try
            {
                _conexao.ExecuteProcedure(Procedures.GKSSP_InsEmpresa);
                _conexao.AddParameter("@Cnpj", empresa.Cnpj);
                _conexao.AddParameter("@RazaoSocial", empresa.RazaoSocial);
                _conexao.AddParameter("@NomeFantasia", empresa.NomeFantasia);
                _conexao.AddParameter("@IdColaboradorCadastro", empresa.IdColaboradorCadastro);
                _conexao.AddParameter("IdEnderecoPrincipal", empresa.IdEnderecoPrincipal);
                _conexao.AddParameter("IdTelefonePrincipal", empresa.IdTelefonePrincipal);
                _conexao.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                _notification.Add(e.ToString());
            }
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
