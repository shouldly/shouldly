using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainValueForKey
{
    public class StringScenario_NoKeyExists : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Foo", "Bar"}
        };
        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainValueForKey("Bar", "Baz");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Dictionary \"_dictionary\" should not contain key \"Bar\" with value \"Baz\" but the key does not exist"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainValueForKey("Foo", "baz");
        }
    }
}