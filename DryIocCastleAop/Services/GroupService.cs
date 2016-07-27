using DryIocCastleAop.Aspects;
using System;
using System.Threading;

namespace DryIocCastleAop.Services
{
    [LogAspect]
    public class GroupService
    {
        public virtual void Rename()
        {
            Console.WriteLine("\tRename()");
        }

        [TimingAspect]
        public virtual void AddUser()
        {
            Console.WriteLine("\tAddUser()");

            Thread.Sleep(100);
        }
    }
}
