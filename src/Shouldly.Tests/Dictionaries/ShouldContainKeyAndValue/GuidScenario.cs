using System;
using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries.ShouldContainKeyAndValue
{
    public class GuidScenario : ShouldlyShouldTestScenario
    {
        private readonly Dictionary<Guid, Guid> _dictionary = new Dictionary<Guid, Guid>
        {
            { GuidKey, GuidValue}
        };

        private static readonly Guid GuidKey = Guid.NewGuid();
        private static readonly Guid GuidValue = Guid.NewGuid();
        private readonly Guid _missingGuidKey = new Guid("1924e617-2fc2-47ae-ad38-b6f30ec2226b");
        private readonly Guid _missingGuidValue = new Guid("F08A0B08-C9F4-49BB-A4D4-BE06E88B69C8");

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKeyAndValue(_missingGuidKey, _missingGuidValue, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return
                    "Dictionary \"_dictionary\" should contain key \"1924e617-2fc2-47ae-ad38-b6f30ec2226b\" with value \"f08a0b08-c9f4-49bb-a4d4-be06e88b69c8\" but the key does not exist " +
                    "Additional Info: " +
                    "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKeyAndValue(GuidKey, GuidValue);
        }
    }
}