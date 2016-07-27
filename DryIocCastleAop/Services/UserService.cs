using DryIocCastleAop.Aspects;
using System;

namespace DryIocCastleAop.Services
{
    public class UserService
    {
        // Intercepted
        [TransactionAspect]
        public virtual void Validate()
        {
            Console.WriteLine("\tValidate()");
        }

        // Not intercept
        public virtual void Create()
        {
            Console.WriteLine("\tCreate()");
        }

        // Not intercept non-virtual calls
        public void Delete()
        {
            Console.WriteLine("\tDelete()");
        }

        // Virtual, but not intercept without AspectAttribute
        public virtual void Update()
        {
            Console.WriteLine("\tUpdate()");
        }
    }
}
