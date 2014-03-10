using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKey
{
    public class ClassScenario : ShouldlyShouldTestScenario
    {
        readonly Dictionary<MyThing, MyThing> _dictionary = new Dictionary<MyThing,MyThing>
        {
            {ThingKey, new MyThing()}
        };

        private static readonly MyThing ThingKey = new MyThing();

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKey(new MyThing());
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should contain key Shouldly.Tests.TestHelpers.MyThing but does not"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKey(ThingKey);
        }
    }
}