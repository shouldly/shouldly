using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Shouldly.Tests 
{
    [TestFixture]
    public class ShouldBeDictionaryTests 
    {
        [Test]
        public void ShouldContainKey_WhenTrue_ShouldNotThrow() 
        {
            var dictionary = new Dictionary<string, string> {{"key", "value"}};

            dictionary.ShouldContainKey("key");
            dictionary.ShouldNotContainKey("rob");
        }

        [Test]
        public void ShouldNotContainKey_WhenTrue_ShouldNotThrow() 
        {
            var dictionary = new Dictionary<string, string> {{"key", "value"}};

            dictionary.ShouldNotContainKey("rob");
        }

        [Test]
        public void ShouldContainKeyAndValue_WhenTrue_ShouldNotThrow() 
        {
            var dictionary = new Dictionary<string, string> {{"key", "value"}};

            dictionary.ShouldContainKeyAndValue("key","value");
        }

        [Test]
        public void ShouldNotContainValueForKey_WhenTrue_ShouldNotThrow() 
        {
            var dictionary = new Dictionary<string, string> {{"key", "value"}};

            dictionary.ShouldNotContainValueForKey("key", "slurpee");
        }

        [Test]
        public void ShouldContainKeyAndValue_WorkWithGuids() 
        {
            var guiddy = Guid.NewGuid();
            var dictionary = new Dictionary<string, Guid> {{"key", guiddy}};

            dictionary.ShouldContainKeyAndValue("key", guiddy);
        }
    }
}