using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using QuanLySV.Helpers;
using QuanLySV.Services;

namespace QuanLySV.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISessionFactory>().UsingFactoryMethod(() =>
            {
                var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(FiddleHelper.GetConnectionStringSQLServer())
                    .ShowSql())
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildConfiguration();

                return cfg.BuildSessionFactory();
            }).LifestyleSingleton());

            container.Register(Component.For<IStudentService>().ImplementedBy<StudentService>());
            container.Register(Component.For<ISubjectService>().ImplementedBy<SubjectService>());
            container.Register(Component.For<ISubjectResultService>().ImplementedBy<SubjectResultService>());
        }
    }
}