using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKeyAndValue
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Foo", ""}
        };
        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKeyAndValue("bar", "baz");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should contain key and value \"bar\" but does not"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKeyAndValue("Foo", "");
        }
    }
}