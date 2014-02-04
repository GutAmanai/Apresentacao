using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apresentacao.Servicos.Dto
{
    public class DtoPesquisaContato
    {
        public List<DtoContato> Pesquisa { get; set; }
        public int PaginaSelecionada { get; set; }
        public int TotalPagina { get; set; }
        public int TotalRegistro { get; set; }
    }
}
