using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace QuanLySV.Data
{
    public class SessionFactory : IDisposable
    {
        private readonly ISessionFactory _sessionFactory;
        public SessionFactory()
        {
            var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString("Data Source=Keydiaz;Initial Catalog=StudentManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True")
                    .ShowSql())
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildConfiguration();

            _sessionFactory = cfg.BuildSessionFactory();
        }

        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }

        public void Dispose()
        {
            _sessionFactory?.Dispose();
        }
    }
}