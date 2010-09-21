using System;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace Tests {
    [TestFixture]
    public class ShouldBeDictionaryTests {
        
        [Test]
        public void ShouldContainKey_WhenTrue_ShouldNotThrow() {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("key", "value");
            dictionary.ShouldContainKey("key");
            dictionary.ShouldNotContainKey("rob");
        }
        [Test]
        public void ShouldNotContainKey_WhenTrue_ShouldNotThrow() {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("key", "value");
            dictionary.ShouldNotContainKey("rob");
        }
        [Test]
        public void ShouldContainKeyAndValue_WhenTrue_ShouldNotThrow() {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("key", "value");
            dictionary.ShouldContainKeyAndValue("key","value");

        }
        [Test]
        public void ShouldNotContainValueForKey_WhenTrue_ShouldNotThrow() {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("key", "value");
            dictionary.ShouldNotContainValueForKey("key", "slurpee");

        }
        [Test]
        public void ShouldContainKeyAndValue_WorkWithGuids() {
            var guiddy = Guid.NewGuid();
            var dictionary = new Dictionary<string, Guid>();
            dictionary.Add("key", guiddy);
            dictionary.ShouldContainKeyAndValue("key", guiddy);
        }
    }
}