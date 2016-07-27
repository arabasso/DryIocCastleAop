using Castle.DynamicProxy;
using DryIocCastleAop.Aspects;
using System;
using System.Linq;
using System.Reflection;

namespace DryIocCastleAop
{
    public class ProxyGenerationHook : IProxyGenerationHook
    {
        public void MethodsInspected()
        {
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            var aspectType = typeof(Aspect);

            if (type.GetCustomAttributes(true).Any(a => a.GetType().IsSubclassOf(aspectType))) return true;

            return methodInfo
                .GetCustomAttributes(true)
                .Any(a => a.GetType().IsSubclassOf(aspectType));
        }
    }
}
