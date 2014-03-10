using System;
using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldNotContainValueForKey
{
    public class GuidScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<Guid, Guid> _dictionary = new Dictionary<Guid, Guid>
        {
            { GuidKey, GuidValue}
        };

        private static readonly Guid GuidKey = new Guid("edae0d73-8e4c-4251-85c8-e5497c7ccad1");
        private static readonly Guid GuidValue = new Guid("fa1e5f58-578f-43d4-b4d6-67eae06a5d17");

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldNotContainValueForKey(GuidKey, GuidValue);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "_dictionary should not contain value for key fa1e5f58-578f-43d4-b4d6-67eae06a5d17 " +
                       "but was fa1e5f58-578f-43d4-b4d6-67eae06a5d17";
            }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldNotContainValueForKey(GuidKey, Guid.NewGuid());
        }
    }
}