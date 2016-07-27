using Castle.DynamicProxy;
using DryIoc;
using DryIocCastleAop.Aspects;
using System;
using System.Linq;

namespace DryIocCastleAop
{
    public class AspectInterceptor : IInterceptor
    {
        readonly IContainer _container;

        public AspectInterceptor(IContainer container)
        {
            _container = container;
        }

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("{0} invocation", invocation);

            var aspects = invocation
                .MethodInvocationTarget
                .GetAspects()
                .Union(invocation.TargetType.GetAspects())
                .ToList();

            var args = new AspectArgs(_container, invocation);

            try
            {
                aspects.ForEach(f => f.OnEntry(args));

                invocation.Proceed();

                aspects.ForEach(f => f.OnSucess(args));
            }

            catch (Exception exception)
            {
                aspects.ForEach(f => f.OnException(args, exception));
            }

            finally
            {
                aspects.ForEach(f => f.OnExit(args));
            }
        }
    }
}
