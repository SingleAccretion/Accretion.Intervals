namespace Accretion.Intervals.Tests
{
    public class ValueClass
    {
        public ValueClass(int value) => Value = value;

        public int Value { get; }

        public static implicit operator ValueClass(int? value) => value.HasValue ? new ValueClass(value.Value) : null;
    }
}
