using System;

namespace DryIocCastleAop.Aspects
{
    public class TransactionAspect : Aspect
    {
        public override void OnEntry(AspectArgs args)
        {
            Console.WriteLine("BeginTransaction");
        }

        public override void OnSucess(AspectArgs args)
        {
            Console.WriteLine("Commit");
        }

        public override void OnException(AspectArgs args, Exception exception)
        {
            Console.WriteLine("Rollback");
        }

        public override void OnExit(AspectArgs args)
        {
            Console.WriteLine("Dispose");
        }
    }
}
