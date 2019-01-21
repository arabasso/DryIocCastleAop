using System;
using System.Linq;
using Castle.DynamicProxy;
using DryIoc;

namespace DryIocCastleAop.DryIoc
{
    // Extension methods for interceptor registration using Castle Dynamic Proxy.
    public static class DryIocInterception
    {
        public static void Intercept<TService, TInterceptor>(this IRegistrator registrator, object serviceKey = null) 
            where TInterceptor : class, IInterceptor
        {
            var serviceType = typeof(TService);

            Type proxyType;
            if (serviceType.IsInterface)
                proxyType = ProxyBuilder.Value.CreateInterfaceProxyTypeWithTargetInterface(
                    serviceType, ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
            else if (serviceType.IsClass)
                proxyType = ProxyBuilder.Value.CreateClassProxyType(
                    serviceType, ArrayTools.Empty<Type>(), ProxyOptions.Value);
            else
                throw new ArgumentException(string.Format(
                    "Intercepted service type {0} is not a supported: nor class nor interface", serviceType));

            var decoratorSetup = serviceKey == null
                ? Setup.Decorator
                : Setup.DecoratorWith(r => serviceKey.Equals(r.ServiceKey));

            registrator.Register(serviceType, proxyType,
                made: Made.Of(type => type.GetPublicInstanceConstructors().SingleOrDefault(c => c.GetParameters().Length != 0), 
                    Parameters.Of.Type<IInterceptor[]>(typeof(TInterceptor[]))),
                setup: decoratorSetup);
        }

        private static readonly Lazy<DefaultProxyBuilder> ProxyBuilder = new Lazy<DefaultProxyBuilder>(() => new DefaultProxyBuilder());
        private static readonly Lazy<ProxyGenerationOptions> ProxyOptions = new Lazy<ProxyGenerationOptions>(() => new ProxyGenerationOptions(new ProxyGenerationHook()));
    }
}
