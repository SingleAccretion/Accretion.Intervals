using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class TypeExtensions
    {
        private const char GenericSpecialChar = '`';
        private const string GenericSeparator = ", ";

        public static string GetNameInCode(this Type t)
        {
            var sb = new StringBuilder();

            sb.Append(t.GetCleanName());

            if (t.IsGenericType)
            {
                var names = t.GetGenericArguments().Select(x => GetNameInCode(x)).ToArray();

                sb.Append("<");
                sb.Append(string.Join(GenericSeparator, names));
                sb.Append(">");
            }
            return sb.ToString();
        }

        public static string GetCleanName(this Type t)
        {
            string name = t.Name;
            if (t.IsGenericType)
            {
                name = name.Remove(name.IndexOf(GenericSpecialChar));
            }
            return name;
        }        
    }
}
