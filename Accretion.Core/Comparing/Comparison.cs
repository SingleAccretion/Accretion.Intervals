using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accretion.Core
{
    public struct Comparison : IEquatable<Comparison>
    {
        private readonly sbyte _comparisonResult;

        public Comparison(int comparisonResult)
        {
            _comparisonResult = (sbyte)Math.Sign(comparisonResult);
        }

        public bool IsEqual { get => _comparisonResult == 0; }
        public bool IsLess { get => _comparisonResult < 0; }
        public bool IsGreater { get => _comparisonResult > 0; }

        public override bool Equals(object obj)
        {
            if (!(obj is Comparison comparison))
            {
                return false;
            }

            return Equals(comparison);
        }

        public bool Equals(Comparison other)
        {
            return _comparisonResult == other._comparisonResult;
        }

        public override int GetHashCode()
        {
            return _comparisonResult;
        }

        public static bool operator ==(Comparison left, Comparison right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Comparison left, Comparison right)
        {
            return !(left == right);
        }
    }
}
