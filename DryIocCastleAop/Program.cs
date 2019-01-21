/**
 * 2016 - Raphael Basso - arabasso@host.sk
 * Example of DryIoC + Castle.Core + Aspect Oriented Programming
 */

using DryIoc;
using DryIocCastleAop.Forms;
using DryIocCastleAop.Services;
using DryIocCastleAop.Views;
using System;

using System.Windows.Forms;
using DryIocCastleAop.DryIoc;

namespace DryIocCastleAop
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var container = new Container(rules => rules.WithoutThrowOnRegisteringDisposableTransient()))
            {
                container.Register<AspectInterceptor>();

                container.Register<UserService>();
                container.Intercept<UserService, AspectInterceptor>();

                container.Register<GroupService>();
                container.Intercept<GroupService, AspectInterceptor>();

                container.Register<IEmailService, EmailService>();
                container.Intercept<IEmailService, AspectInterceptor>();

                container.Register<MainForm>();
                container.Intercept<MainForm, AspectInterceptor>();

                container.Register<MainView>();
                container.Intercept<MainView, AspectInterceptor>();

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

                Application.Run(container.Resolve<MainForm>());
            }
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
