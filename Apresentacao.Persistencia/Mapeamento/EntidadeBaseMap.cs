using Apresentacao.Dominio.Entidades;
using FluentNHibernate.Mapping;

namespace Apresentacao.Persistencia.Mapeamento
{
    public class EntidadeBaseMap<T> : ClassMap<T> where T : EntidadeBase
    {
        public EntidadeBaseMap()
        {            
            Id(x => x.Id).GeneratedBy.Identity();
        }
        
    }
}