using Accretion.Intervals.StringConversion;
using System.Globalization;

namespace Accretion.Intervals.Tests
{
    public static class StringConstants
    {
        public static readonly string Empty = Symbols.EmptySetString.ToString();
        public static readonly string MaxDouble = double.MaxValue.ToString("R", CultureInfo.InvariantCulture);
        public static readonly string MinDouble = double.MinValue.ToString("R", CultureInfo.InvariantCulture);
        /*
        public static readonly string Monday = nameof(Day.Monday);
        public static readonly string Tuesday = nameof(Day.Tuesday);
        public static readonly string Wednesday = nameof(Day.Wednesday);
        public static readonly string Thursday = nameof(Day.Thursday);
        public static readonly string Friday = nameof(Day.Friday);
        public static readonly string Saturday = nameof(Day.Saturday);
        public static readonly string Sunday = nameof(Day.Sunday);
        */
    }
}
