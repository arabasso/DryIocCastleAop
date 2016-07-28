/**
 * 2016 - Raphael Basso - arabasso@host.sk
 * Example of DryIoC + Castle.Core + Aspect Oriented Programming
 */

using DryIoc;
using DryIocCastleAop.Services;
using System;

namespace DryIocCastleAop
{
    class Program
    {
        static void Main()
        {
            using(var container = new Container())
            {
                container.Register<AspectInterceptor>();
                container.Register<UserService>();
                container.Register<GroupService>();
                container.Register<IEmailService, EmailService>();
                container.RegisterClassInterceptor<UserService, AspectInterceptor>();
                container.RegisterClassInterceptor<GroupService, AspectInterceptor>();
                container.RegisterInterfaceInterceptor<IEmailService, AspectInterceptor>();

                var us = container.Resolve<UserService>();

                // Not intercept
                us.Update();

                Hr();

                // Intercepted by TransactionAspect
                us.Validate();

                Hr();

                // Not intercept
                us.Delete();

                Hr();

                // Not intercept
                us.Create();

                Hr();

                var gs = container.Resolve<GroupService>();

                // Intercepted by LogAspect
                gs.Rename();

                Hr();

                // Intercepted by LogAspect and TimingAspect
                gs.AddUser();

                Hr();

                var es = container.Resolve<IEmailService>();

                // Intercepted by ExceptionAspect
                if (!es.Send())
                {
                    Hr();

                    es.Resend();
                }
            }

            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }

        static void Hr()
        {
            for(var i = 0; i < 80; i++)
            {
                Console.Write('-');
            }

            Console.WriteLine();
        }
    }
}
