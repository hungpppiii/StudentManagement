using QuanLySV.App_Start;
using System;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(QuanLySV.App_Start.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(QuanLySV.App_Start.WindsorActivator), "Shutdown")]

namespace QuanLySV.App_Start
{
    public class WindsorActivator
    {
        static ContainerBootstrapper bootstrapper;

        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }

        public static void Shutdown()
        {
            if (bootstrapper != null)
                bootstrapper.Dispose();
        }
    }
}