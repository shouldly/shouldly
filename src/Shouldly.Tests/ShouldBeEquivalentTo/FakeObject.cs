using System.Collections;
using System.Collections.Generic;

namespace Shouldly.Tests.ShouldBeEquivalentTo
{
    public class FakeObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FakeObject Child { get; set; }
        public ICollection<string> Colors { get; set; }
        public IEnumerable Adjectives { get; set; }
    }
}
