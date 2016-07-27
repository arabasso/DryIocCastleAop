using System;
using System.Diagnostics;

namespace DryIocCastleAop.Aspects
{
    public class TimingAspect : Aspect
    {
        Stopwatch _watch;

        public override void OnEntry(AspectArgs args)
        {
            Console.WriteLine("Timing Start...");

            _watch = Stopwatch.StartNew();
        }

        public override void OnExit(AspectArgs args)
        {
            _watch.Stop();

            Console.WriteLine("Timing Stop: {0}", _watch.Elapsed);
        }
    }
}
