using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainValueForKey
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Foo", ""}
        };
        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainValueForKey("Foo", "");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should not contain value for key \"\" but was \"\""; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainValueForKey("Foo", "baz");
        }
    }
}