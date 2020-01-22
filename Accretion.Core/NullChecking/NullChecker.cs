using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class NullChecker
    {
        public static bool IsNull<T>(T obj)
        {
            return !(obj is object);
        }
    }
}
