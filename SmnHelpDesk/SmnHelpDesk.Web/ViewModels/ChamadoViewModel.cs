using SmnHelpDesk.Web.Application.Chamado.Model;
using System;
using System.Web.Mvc;

namespace SmnHelpDesk.Web.ViewModels
{
    public class ChamadoViewModel : ChamadoModel
    {
        public ChamadoViewModel() { }

        public ChamadoViewModel(SelectList tipos, SelectList criticidades)
        {
            ComboTipos = tipos;
            ComboCriticidades = criticidades;
        }
        public ChamadoViewModel(ChamadoModel model)
        {

            Id = model.Id;
            NumeroChamado = model.NumeroChamado;
            NomeProblema = model.NomeProblema;
            Descricao = model.Descricao;
            IdCriticidade = model.IdCriticidade;
            IdTipo = model.IdTipo;
            IdStatus = model.IdStatus;
            NomeClienteCad = model.NomeClienteCad;
            NomeCriticidade = model.NomeCriticidade;
            NomeTipoStatus = model.NomeTipoStatus;
            IdClienteCad = model.IdClienteCad;
            IdClienteAlt = model.IdClienteAlt;
            DescricaoMotivoCancel = model.DescricaoMotivoCancel;
            NomeColaboradorPrincipal = model.NomeColaboradorPrincipal;
        }

        public string NomeEmpresa { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool IsPendenteAnalise => IdStatus == 1;
        public SelectList ComboTipos { get; set; }
        public SelectList ComboCriticidades { get; set; }
        public SelectList ComboStatus { get; set; }
    }
}