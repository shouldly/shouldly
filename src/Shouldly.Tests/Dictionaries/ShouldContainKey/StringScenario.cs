using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKey
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Foo", ""}
        };

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKey("bar", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "Dictionary \"_dictionary\" should contain key \"bar\" but does not " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKey("Foo");
        }
    }
}