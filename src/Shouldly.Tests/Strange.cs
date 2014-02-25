using System.Collections;
using System.Collections.Generic;

namespace Shouldly.Tests
{
    class Strange : IEnumerable<Strange>
    {
        private readonly string _thing;

        public Strange()
        {
        }

        private Strange(string thing)
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

        public override string ToString()
        {
            return _thing ?? "null";
        }
    }
}