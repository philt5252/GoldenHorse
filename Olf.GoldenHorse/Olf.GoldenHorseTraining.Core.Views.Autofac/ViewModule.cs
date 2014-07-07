using Autofac;
using Olf.GoldenHorseTraining.Core.Views.Factories;
using Olf.GoldenHorseTraining.Foundation.Views.Factories;

namespace Olf.GoldenHorseTraining.Core.Views.Autofac
{
    public class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MainWindowFactory>().As<IMainWindowFactory>().SingleInstance();
        }
    }
}