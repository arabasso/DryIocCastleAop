using System;

namespace DryIocCastleAop.Aspects
{
    public class ExceptionAspect : Aspect
    {
        public override void OnException(AspectArgs args, Exception exception)
        {
            Console.WriteLine("OnException: {0}", exception.Message);

            args.Invocation.ReturnValue = false;
        }
    }
}
