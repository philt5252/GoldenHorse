using Autofac;
using Olf.GoldenHorseTraining.Core.Controllers;
using Olf.GoldenHorseTraining.Foundation.Controllers;

namespace Olf.GoldenHorseTraining.Core.Autofac
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