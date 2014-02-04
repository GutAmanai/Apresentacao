using Apresentacao.Dominio.Entidades;

namespace Apresentacao.Persistencia.Mapeamento
{
    public class EmpresaMap : EntidadeBaseMap<Empresa>
    {
        public EmpresaMap()
        {
            Table("Empresa");
            Map(x => x.Nome);
            Map(x => x.Cnpj);
            Map(x => x.NomeFantasia);
            Map(x => x.WebSite);            
            //HasMany(x => x.Contatos);
            HasMany<Contato>(x => x.Contatos).Cascade.AllDeleteOrphan().Not.LazyLoad();
        }
    }
}
