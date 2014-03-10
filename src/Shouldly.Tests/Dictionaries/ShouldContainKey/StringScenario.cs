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
            _dictionary.ShouldContainKey("bar");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should contain key \"bar\" but does not"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKey("Foo");
        }
    }
}