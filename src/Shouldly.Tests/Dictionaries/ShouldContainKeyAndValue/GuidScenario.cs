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
        private readonly Guid _missingGuid = new Guid("1924e617-2fc2-47ae-ad38-b6f30ec2226b");

        protected override void ShouldThrowAWobbly()
        {
            _dictionary.ShouldContainKeyAndValue(_missingGuid, Guid.NewGuid());
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_dictionary should contain key and value 1924e617-2fc2-47ae-ad38-b6f30ec2226b but does not"; }
        }

        protected override void ShouldPass()
        {
            _dictionary.ShouldContainKeyAndValue(GuidKey, GuidValue);
        }
    }
}