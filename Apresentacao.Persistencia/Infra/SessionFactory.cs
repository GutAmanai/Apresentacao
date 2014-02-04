using System;
using System.Configuration;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace Apresentacao.Persistencia.Infra
{
    public class SessionFactory
    {
        private static SessionFactory _instancia;
        private static ISessionFactory _sessionFactory;
        private static string _assembliesFolder;

        public SessionFactory()
        {
            _assembliesFolder = AppDomain.CurrentDomain.RelativeSearchPath;
            if (string.IsNullOrWhiteSpace(_assembliesFolder))
                _assembliesFolder = AppDomain.CurrentDomain.BaseDirectory;

            _sessionFactory = FluentlyConfig.BuildSessionFactory();
        }

        public static SessionFactory Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new SessionFactory();
                return _instancia;
            }
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        public FluentConfiguration FluentlyConfig
        {
            get
            {
                return Fluently.Configure()
                    .Database(GetDatabaseConfigurations())
                    .Mappings(
                        m =>
                        m.FluentMappings
                            .AddFromAssembly(Assembly.GetExecutingAssembly())
                            .Conventions.Add<CustomTableNameConvention>()
                            .Conventions.Add<CustomForeignKeyConvention>()
                            .Conventions.Add<CustomForeignKeyConstraintOneToManyConvention>()
                            .Conventions.Add<CustomJoinedSubclassConvention>()
                            .Conventions.Add<CustomManyToManyTableNameConvention>()
                            .Conventions.Add<StringColumnLengthConvention>()
                            .Conventions.Add<StringPropertyConvention>()
                            .Conventions.Add<ColumnNullabilityConvention>()
                            .Conventions.Add(DefaultLazy.Always())

                    )
                    .ExposeConfiguration(cfg => cfg.SetProperty("generate_statistics", "true"));
            }
        }

        private static IPersistenceConfigurer GetDatabaseConfigurations()
        {
            var database = ConfigurationManager.AppSettings["Database"];

            switch (database)
            {
                case "SQLServer":
                    {
                        return MsSqlConfiguration
                                    .MsSql2008
                                    .ConnectionString(c => c.FromConnectionStringWithKey("SQLServerConn"))
                                    .ShowSql();
                    }
                case "MySQL":
                    {
                        return MySQLConfiguration
                                    .Standard
                                    .ConnectionString(c => c.FromConnectionStringWithKey("MySQLConn"))
                                    .ShowSql();
                    }
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Não há suporte para o banco de dados {0}!", database));
            }
        }
    }

}