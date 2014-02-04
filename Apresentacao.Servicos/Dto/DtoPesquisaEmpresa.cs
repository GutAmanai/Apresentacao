using System.Collections.Generic;

namespace Apresentacao.Servicos.Dto
{
    public class DtoPesquisaEmpresa
    {
        public List<DtoEmpresa> Pesquisa    { get; set; }
        public int PaginaSelecionada        { get; set; }
        public int TotalPagina              { get; set; }
        public int TotalRegistro            { get; set; }
    }
}
