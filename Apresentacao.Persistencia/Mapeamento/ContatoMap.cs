using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apresentacao.Dominio.Entidades;

namespace Apresentacao.Persistencia.Mapeamento
{
    public class ContatoMap : EntidadeBaseMap<Contato>
    {
        public ContatoMap()
        {
            Table("Contato");
            Map(x => x.Nome);
            Map(x => x.Celular);
            Map(x => x.Email);
            Map(x => x.Telefone);
            References(x => x.Empresa).Nullable();
        }
    }
}
