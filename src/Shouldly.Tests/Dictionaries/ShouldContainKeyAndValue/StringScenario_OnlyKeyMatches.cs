using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKeyAndValue
{
    public class StringScenario_OnlyKeyMatches : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Foo", "Bar"}
        };
        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKeyAndValue("Foo", "baz");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Dictionary \"_dictionary\" should contain key \"Foo\" with value \"baz\" but value was \"Bar\""; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKeyAndValue("Foo", "Bar");
        }
    }
}