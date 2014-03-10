using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainValueForKey
{
    public class ClassScenario : ShouldlyShouldTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing,MyThing>
        {
            {ThingKey, ThingValue}
        };

        private static readonly MyThing ThingKey = new MyThing();
        private static readonly MyThing ThingValue = new MyThing();

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainValueForKey(ThingKey, ThingValue);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should not contain value for key Shouldly.Tests.TestHelpers.MyThing but was Shouldly.Tests.TestHelpers.MyThing"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainValueForKey(ThingKey, new MyThing());
        }
    }
}