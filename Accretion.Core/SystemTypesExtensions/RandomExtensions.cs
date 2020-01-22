using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public static class RandomExtensions
    {                
        public static string NextString(this Random random, int minStringLength, int maxStringLength, IList<char> possibleCharacters)
        {
            var numberOfCharacters = possibleCharacters.Count;            
            var newStringLength = random.Next(minStringLength, maxStringLength + 1);
            var newStringCharArray = new char[newStringLength];

            for (int i = 0; i < newStringLength; i++)
            {
                newStringCharArray[i] = possibleCharacters[random.Next(numberOfCharacters)];
            }

            return new string(newStringCharArray);
        }

        public static string NextString(this Random random, int maxStringLength, IList<char> possibleCharacters)
        {
            return random.NextString(0, maxStringLength, possibleCharacters);
        }
    }
}
