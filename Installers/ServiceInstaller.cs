﻿using Castle.MicroKernel.Registration;
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
                    .ConnectionString("Data Source=Keydiaz;Initial Catalog=StudentManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True")
                    .ShowSql())
                .Mappings(m =>
                    m.FluentMappings
                        .AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildConfiguration();

                return cfg.BuildSessionFactory();
            }).LifestyleSingleton());
        }
    }
}