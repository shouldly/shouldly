using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKeyAndValue
{
    public class OnlyKeyMatches : ShouldlyShouldTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing,MyThing>
        {
            {ThingKey, ThingValue}
        };

        private static readonly MyThing ThingKey = new MyThing();
        private static readonly MyThing ThingValue = new MyThing();

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKeyAndValue(ThingKey, new MyThing(), () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return
                    "Dictionary \"_dictionary\" should contain key \"Shouldly.Tests.TestHelpers.MyThing\" with value \"Shouldly.Tests.TestHelpers.MyThing\" but value was \"Shouldly.Tests.TestHelpers.MyThing\" " +
                    "Additional Info: " +
                    "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKeyAndValue(ThingKey, ThingValue);
        }
    }
}