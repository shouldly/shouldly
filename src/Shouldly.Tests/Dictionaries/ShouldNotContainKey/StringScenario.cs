using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainKey
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Foo", ""}
        };
        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainKey("Foo", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "Dictionary \"_dictionary\" should not contain key \"Foo\" but does " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainKey("bar");
        }
    }
}