using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;

namespace NHibernateVsEf.Core
{
    public class SessionFactoryBuilder
    {
        private static ISessionFactory _sessionFactory;

        public ISessionFactory BuildSessionFactory(string ctx = "web")
        {
            if (_sessionFactory != null) return _sessionFactory;

            var cfg = new Configuration();
            cfg.DataBaseIntegration
            (
                db =>
                {
                    db.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
                    db.Driver<NHibernate.Driver.Sql2008ClientDriver>();
                    db.ConnectionStringName = Constants.ConnectionStringName;
                }
            );
            cfg.AddInputStream(HbmSerializer.Default.Serialize(Assembly.GetAssembly(typeof (SessionFactoryBuilder))));
            cfg.SetProperty("show_sql", "true");
            //cfg.SetProperty("hbm2ddl.auto", "create");
            cfg.SetProperty("current_session_context_class", ctx);
            _sessionFactory = cfg.BuildSessionFactory();
            return _sessionFactory;
        }     
    }
}