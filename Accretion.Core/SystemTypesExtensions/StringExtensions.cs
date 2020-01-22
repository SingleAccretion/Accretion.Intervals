using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class StringExtensions
    {
        public static string RemoveString(this string self, string ch)
        {            
            return self.Replace(ch, string.Empty);
        }

        public static string RemoveChar(this string self, char ch)
        {
            return self.Replace(ch.ToString(), string.Empty);
        }

        public static string CommonStartWith(this string thisString, string otherString)
        {
            var maxLength = Math.Min(thisString.Length, otherString.Length);

            if (maxLength == 0)
            {
                return string.Empty;
            }

            var lastSameCharIndex = 0;
            while (lastSameCharIndex < maxLength && thisString[lastSameCharIndex] == otherString[lastSameCharIndex])
            {
                lastSameCharIndex++;
            }

            return thisString.Substring(0, lastSameCharIndex);
        }        

        public static string CommonEndWith(this string thisString, string otherString)
        {
            var thisStringIndex = thisString.Length - 1;
            var otherStringIndex = otherString.Length - 1;            

            if (thisString.Length == 0 || otherString.Length == 0)
            {
                return string.Empty;
            }

            while (Math.Min(thisStringIndex, otherStringIndex) > 0 && thisString[thisStringIndex] == otherString[otherStringIndex])
            {
                thisStringIndex--;
                otherStringIndex--;
            }

            return thisString.Substring(thisStringIndex + 1);
        }        

        public static IReadOnlyList<string> SplitByUpper(this string self)
        {
            var result = new List<string>();

            if (self.Length == 0)
            {
                return result;
            }

            var lastUpperIndex = 0;
            for (int i = 1; i < self.Length; i++)
            {
                if (char.IsUpper(self[i]))
                {
                    result.Add(self[lastUpperIndex..i]);
                    lastUpperIndex = i;
                }
            } 
            result.Add(self[lastUpperIndex..]);

            return result;
        }

        public static bool StartsWith(this string self, char ch)
        {            
            return self.Length != 0 && self[0] == ch;
        }

        public static bool EndsWith(this string self, char ch)
        {
            return self.Length != 0 && self[^1] != ch;
        }
    }
}
