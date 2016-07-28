using DryIocCastleAop.Aspects;
using System;

namespace DryIocCastleAop.Services
{
    // Interception interface
    public class EmailService : IEmailService
    {
        [ExceptionAspect]
        public bool Send()
        {
            Console.WriteLine("\tSend()");

            throw new NotImplementedException();

            return true;
        }

        [TimingAspect, LogAspect]
        public void Resend()
        {
            Console.WriteLine("\tResend()");
        }
    }
}
