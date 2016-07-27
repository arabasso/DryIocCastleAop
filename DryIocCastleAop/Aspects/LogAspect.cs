using System;

namespace DryIocCastleAop.Aspects
{
    public class LogAspect : Aspect
    {
        public override void OnEntry(AspectArgs args)
        {
            Console.WriteLine("Log OnEntry");
        }

        public override void OnExit(AspectArgs args)
        {
            Console.WriteLine("Log OnExit");
        }
    }
}
