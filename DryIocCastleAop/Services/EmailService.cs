using DryIocCastleAop.Aspects;
using System;

namespace DryIocCastleAop.Services
{
    // Interception interface
    public class EmailService : IEmailService
    {
        [ExceptionAspect]
        public void Send()
        {
            Console.WriteLine("\tSend()");

            throw new NotImplementedException();
        }
    }
}
