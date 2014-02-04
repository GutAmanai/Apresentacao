using Apresentacao.Persistencia.Infra;
using Ninject;
using Ninject.Modules;

namespace Apresentacao.Infra
{
    public class Fabrica
    {
        private static Fabrica _instancia;
        public static Fabrica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (typeof(Fabrica))
                    {
                        _instancia = new Fabrica();
                    }
                }
                return _instancia;
            }
        }
        public StandardKernel Kernel;

        protected Fabrica()
        {
            Kernel = new StandardKernel(new INinjectModule[] { new DependenciaResolvedor() });
        }

        public T Obter<T>()
        {
            return _instancia.Kernel.Get<T>();
        }
    }
}
