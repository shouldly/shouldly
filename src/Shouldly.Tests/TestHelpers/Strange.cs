using System.Collections;
using System.Collections.Generic;

namespace Shouldly.Tests.TestHelpers
{
    internal class Strange : IEnumerable<Strange>
    {
        readonly string? _thing;

        public Strange()
        {
        }

        Strange(string thing)
        {
            _thing = thing;
        }

        public IEnumerator<Strange> GetEnumerator()
        {
            return new List<Strange>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator Strange(string thing)
        {
            return new Strange(thing);
        }

        protected bool Equals(Strange other)
        {
            return string.Equals(_thing, other._thing);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Strange) obj);
        }

        public override int GetHashCode()
        {
            return _thing != null ? _thing.GetHashCode() : 0;
        }

        public static bool operator ==(Strange left, Strange right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Strange left, Strange right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return _thing ?? "null";
        }
    }
}