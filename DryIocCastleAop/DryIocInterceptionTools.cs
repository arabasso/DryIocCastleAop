using Castle.DynamicProxy;
using DryIoc;
using DryIocCastleAop.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DryIocCastleAop
{
    public static class DryIocInterceptionTools
    {
        public static IEnumerable<Aspect> GetAspects(this MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(true)
                .Where(w => w.GetType().IsSubclassOf(typeof(Aspect)))
                .Select(s => (Aspect)s);
        }

        public static bool HasAspects(this MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(true)
                .Any(w => w.GetType().IsSubclassOf(typeof(Aspect)));
        }

        public static void RegisterClassInterceptor<TService, TInterceptor>(this IRegistrator registrator)where TInterceptor : class, IInterceptor
        {
            var serviceType = typeof (TService);

            if (!serviceType.IsClass)
                throw new ArgumentException(string.Format("Intercepted service type {0} is not a class", serviceType));

            var proxyType = ProxyBuilder.Value.CreateClassProxyType(serviceType, new Type[0], ProxyOptions.Value);

            registrator.Register(serviceType, proxyType, made: Made.Of(FactoryMethod.ConstructorWithResolvableArguments, Parameters.Of.Type<IInterceptor[]>(typeof (TInterceptor[]))), setup: Setup.Decorator);
        }

        public static void RegisterInterfaceInterceptor<TService, TInterceptor>(this IRegistrator registrator)where TInterceptor : class, IInterceptor
        {
            var serviceType = typeof (TService);

            if (!serviceType.IsInterface)
                throw new ArgumentException(string.Format("Intercepted service type {0} is not an interface", serviceType));

            var proxyType = ProxyBuilder.Value.CreateInterfaceProxyTypeWithTargetInterface(serviceType, new Type[0], ProxyGenerationOptions.Default);

            registrator.Register(serviceType, proxyType, made: Parameters.Of.Type<IInterceptor[]>(typeof (TInterceptor[])), setup: Setup.Decorator);
        }

        private static readonly Lazy<DefaultProxyBuilder> ProxyBuilder = new Lazy<DefaultProxyBuilder>(() => new DefaultProxyBuilder());
        private static readonly Lazy<ProxyGenerationOptions> ProxyOptions = new Lazy<ProxyGenerationOptions>(() => new ProxyGenerationOptions(new ProxyGenerationHook()));
    }
}
