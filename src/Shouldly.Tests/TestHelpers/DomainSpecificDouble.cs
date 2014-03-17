namespace Shouldly.Tests.TestHelpers
{
    public struct DomainSpecificDouble
    {
        public static readonly DomainSpecificDouble MinValue = 0;
        public static readonly DomainSpecificDouble MaxValue = 255;

        private readonly double _value;

        private DomainSpecificDouble(double value)
        {
            _value = value;
        }

        public static implicit operator DomainSpecificDouble(double value)
        {
            return new DomainSpecificDouble(value);
        }

        public bool Equals(DomainSpecificDouble other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DomainSpecificDouble && Equals((DomainSpecificDouble)obj);
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

}