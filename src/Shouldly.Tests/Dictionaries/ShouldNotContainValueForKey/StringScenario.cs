using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainValueForKey
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Foo", "Bar"}
        };
        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainValueForKey("Foo", "Bar");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Dictionary \"_dictionary\" should not contain key \"Foo\" with value \"Bar\" but does"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainValueForKey("Foo", "baz");
        }
    }
}