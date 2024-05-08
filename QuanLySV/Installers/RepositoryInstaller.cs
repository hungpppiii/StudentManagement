using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using QuanLySV.Repositories;
using QuanLySV.Services;

namespace QuanLySV.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IStudentRepository>().ImplementedBy<StudentRepository>());
            container.Register(Component.For<ISubjectRepository>().ImplementedBy<SubjectRepository>());
            container.Register(Component.For<ISubjectResultRepository>().ImplementedBy<SubjectResultRepository>());
        }
    }
}