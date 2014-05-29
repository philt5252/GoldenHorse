using Autofac;
using Olf.GoldenHorse.Foundation.DataAccess;

namespace Olf.GoldenHorse.Core.DataAccess.Autofac
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().SingleInstance();
        }
    }
}