using Autofac;
using Olf.GoldenHorse.Core.Controllers;
using Olf.GoldenHorse.Foundation.Controllers;

namespace Olf.GoldenHorse.Core.Autofac
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AppController>().As<IAppController>().SingleInstance();
        }
    }
}