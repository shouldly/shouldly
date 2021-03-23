using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shouldly.Tests.ShouldBeEquivalentTo
{
    public class FakeObject
    {
        public int Id { get; set; }
        public string? Title;
        public string? Name { get; set; }
        public FakeObject? Child { get; set; }
        public ICollection<string>? Colors { get; set; }
        public IEnumerable? Adjectives { get; set; }
    }

    public class IndexableObject
    {
        private Dictionary<int, string> _indexedStrings;
        public IndexableObject(IEnumerable<string> strings)
        {
            _indexedStrings =
                strings
                    .Select((item, index) => new KeyValuePair<int, string>(index, item))
                    .ToDictionary(x => x.Key, x => x.Value);
        }

        // Indexing is different to standard properties, in that indexing is a property that takes an argument.
        public string this[int index] => _indexedStrings[index];
    }
}
