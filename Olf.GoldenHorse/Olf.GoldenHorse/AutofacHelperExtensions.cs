using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Features.OwnedInstances;

namespace Olf.GoldenHorse
{
    public static class AutofacHelperExtensions
    {
        private static IContainer container;

        public static void RegisterAllFuncsAsOwned(this IContainer container)
        {
            AutofacHelperExtensions.container = container;

            ContainerBuilder builder2 = new ContainerBuilder();

            foreach (var componentRegistration in container.ComponentRegistry.Registrations)
            {
                Type type = componentRegistration.Activator.LimitType;

                Type funcType = typeof(Func<>).MakeGenericType(type);
                Type regFuncType = typeof(Func<,>).MakeGenericType(typeof(IComponentContext), funcType);

                MethodInfo registerMethod =
                    typeof(RegistrationExtensions).GetMethods().First(m => m.Name == "Register")
                    .MakeGenericMethod(new[] { funcType });

                MethodInfo createMethod = typeof(AutofacHelperExtensions).GetMethod("RegisterFunc",
                    BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(funcType);

                Delegate del = Delegate.CreateDelegate(regFuncType, createMethod);

                registerMethod.Invoke(null, new object[] { builder2, del });
            }

            builder2.Update(container);
        }

        private static T RegisterFunc<T>(IComponentContext c)
        {
            if (!typeof(T).GetGenericArguments().Any())
                throw new Exception(string.Format("Type '{0}' must be of type Func<>", typeof(T).FullName));

            Type type = typeof(T).GetGenericArguments().First();

            Type funcType = typeof(Func<>).MakeGenericType(type);
            Type resolveFuncFuncType = typeof(Func<>).MakeGenericType(funcType);

            MethodInfo resolveFuncMethodInfo = typeof(AutofacHelperExtensions).GetMethod("ResolveFunc", BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(type);

            var del = Delegate.CreateDelegate(resolveFuncFuncType, resolveFuncMethodInfo);

            return (T)del.Method.Invoke(null, null);
        }

        private static Func<T> ResolveFunc<T>()
        {
            return () => container.Resolve<Owned<T>>().Value;
        }
    }
}