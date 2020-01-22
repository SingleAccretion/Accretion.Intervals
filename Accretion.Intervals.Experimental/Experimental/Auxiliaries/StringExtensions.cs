using System;

namespace Accretion.Intervals.Experimental
{
    internal static class StringExtensions
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

        public static bool StartsWith(this string self, char ch)
        {            
            return self.Length != 0 && self[0] == ch;
        }

        public static bool EndsWith(this string self, char ch)
        {
            return self.Length != 0 && self[self.Length - 1] != ch;
        }
    }
}
