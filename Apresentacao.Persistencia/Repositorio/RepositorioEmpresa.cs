using Apresentacao.Dominio.Entidades;
using Apresentacao.Dominio.Repositorio;

namespace Apresentacao.Persistencia.Repositorio
{
    public class RepositorioEmpresa : RepositorioBase<Empresa>, IRepositorioEmpresa
    {
        public RepositorioEmpresa(IUnidadeDeTrabalho unidadeDeTrabalho) : base(unidadeDeTrabalho)
        {
        }
    }
}
