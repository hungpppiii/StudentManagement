﻿using Castle.Windsor.Installer;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySV.App_Start
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {

        readonly IWindsorContainer container;

        ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        public IWindsorContainer Container
        {
            get { return container; }
        }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer().
                Install(FromAssembly.This());
            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}