using Apresentacao.Dominio.Repositorio;
using Apresentacao.Persistencia.Repositorio;
using Ninject.Modules;

namespace Apresentacao.Persistencia.Infra
{
    public class DependenciaResolvedor : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnidadeDeTrabalho>().To<UnidadeDeTrabalho>();
            Bind<IRepositorioEmpresa>().To<RepositorioEmpresa>();
            Bind<IRepositorioContato>().To<RepositorioContato>();
        }
    }
}
