using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Server.DataAccess
{
    internal class DbContext
    {
        ISessionFactory sessionFactory;
        public DbContext()
        {
            var connectionString = "host=ep-sweet-butterfly-47683425.us-east-2.aws.neon.tech; database=StaffDb; search path=StaffDb; port=5432; user id=polyteh.diavol; password=***;";

            var configuration = Fluently.Configure()
               .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString))
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<WorkerMap>())
               .BuildConfiguration();

            var exporter = new SchemaUpdate(configuration);
            exporter.Execute(true, true);

            sessionFactory = configuration.BuildSessionFactory();
        }

        public ISessionFactory SessionFactory { get => sessionFactory; }
    }
}
