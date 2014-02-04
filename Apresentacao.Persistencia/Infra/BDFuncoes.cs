using NHibernate.Tool.hbm2ddl;

namespace Apresentacao.Persistencia.Infra
{
    public class BDFuncoes
    {
        public static void CriarBanco()
        {
            var config = SessionFactory.Instancia.FluentlyConfig;
            config.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));
            config.BuildSessionFactory();
        }

        public static void AtualizarBanco()
        {
            var config = SessionFactory.Instancia.FluentlyConfig;
            config.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true));
            config.BuildSessionFactory();
        }

        public static void ExcluirBanco()
        {
            var config = SessionFactory.Instancia.FluentlyConfig;
            config.ExposeConfiguration(cfg => new SchemaExport(cfg).Drop(true, true));
            config.BuildSessionFactory();
        }
    }
}
