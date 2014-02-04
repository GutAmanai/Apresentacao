using Apresentacao.Dominio.Entidades;
using Apresentacao.Dominio.Repositorio;

namespace Apresentacao.Persistencia.Repositorio
{
    public class RepositorioContato : RepositorioBase<Contato>, IRepositorioContato
    {
        public RepositorioContato(IUnidadeDeTrabalho unidadeDeTrabalho) : base(unidadeDeTrabalho)
        {
        }
    }
}
