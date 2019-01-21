using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DryIocCastleAop.Aspects;

namespace DryIocCastleAop
{
    public static class AspectExtensions
    {
        public static IEnumerable<Aspect> GetAspects(
            this MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(true)
                .Where(w => w.GetType().IsSubclassOf(typeof(Aspect)))
                .Select(s => (Aspect)s);
        }

        public static bool HasAspects(
            this MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(true)
                .Any(w => w.GetType().IsSubclassOf(typeof(Aspect)));
        }
    }
}
