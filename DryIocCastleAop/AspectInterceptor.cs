using Castle.DynamicProxy;
using DryIoc;
using DryIocCastleAop.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DryIocCastleAop
{
    public class AspectInterceptor
        : IInterceptor
    {
        readonly IContainer _container;

        public AspectInterceptor(
            IContainer container)
        {
            _container = container;
        }

        public void Intercept(
            IInvocation invocation)
        {
            Console.WriteLine("{0} invocation", invocation);

            var aspects = GetAspects(invocation);

            var args = new AspectArgs(_container, invocation);

            Exception ex = null;

            try
            {
                foreach(var aspect in aspects)
                {
                    aspect.OnEntry(args);
                }

                invocation.Proceed();
            }

            catch (Exception exception)
            {
                ex = exception;
            }

            finally
            {
                foreach (var aspect in aspects.Reverse())
                {
                    if (ex != null)
                    {
                        aspect.OnException(args, ex);
                    }

                    else
                    {
                        aspect.OnSucess(args);
                    }

                    aspect.OnExit(args);
                }
            }
        }

        private static IList<Aspect> GetAspects(
            IInvocation invocation)
        {
            return invocation.TargetType.GetAspects()
                .Union(invocation.MethodInvocationTarget.GetAspects())
                .ToList();
        }
    }
}
