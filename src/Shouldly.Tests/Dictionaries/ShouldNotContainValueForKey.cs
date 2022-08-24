using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Dictionaries
{
    public class ShouldNotContainValueForKey
    {
        [Fact]
        public void ClassScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
_classDictionary.ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

errorWithSource:
@"_classDictionary
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
        }

        [Fact]
        public void GuidScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
_guidDictionary.ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"),

errorWithSource:
@"_guidDictionary
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    with value
fa1e5f58-578f-43d4-b4d6-67eae06a5d17
    but does

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    with value
fa1e5f58-578f-43d4-b4d6-67eae06a5d17
    but does

Additional Info:
    Some additional context");
        }

        [Fact]
        public void KeyAndValueExistShouldFail()
        {
            Verify.ShouldFail(() =>
_classDictionary.ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

errorWithSource:
@"_classDictionary
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
        }

        [Fact]
        public void NoKeyExistsShouldFail()
        {
            Verify.ShouldFail(() =>
_classDictionary.ShouldNotContainValueForKey(new MyThing(), ThingValue, "Some additional context"),

errorWithSource:
@"_classDictionary
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but the key does not exist

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but the key does not exist

Additional Info:
    Some additional context");
        }

        [Fact]
        public void StringScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
_stringDictionary.ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"),

errorWithSource:
@"_stringDictionary
    should not contain key
""Foo""
    with value
""Bar""
    but does

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[""Foo"" => ""Bar""]]
    should not contain key
""Foo""
    with value
""Bar""
    but does

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ValueIsNullShouldFail()
        {
            var dictionary = new Dictionary<MyThing, MyThing?>
            {
                { ThingKey, null }
            };
            Verify.ShouldFail(() =>
dictionary.ShouldNotContainValueForKey(ThingKey, null, "Some additional context"),

errorWithSource:
@"dictionary
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
null
    but does

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => null]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
null
    but does

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            _classDictionary.ShouldNotContainValueForKey(ThingKey, new MyThing());
            _guidDictionary.ShouldNotContainValueForKey(GuidKey, Guid.NewGuid());
            _stringDictionary.ShouldNotContainValueForKey("Foo", "baz");
        }

        private readonly Dictionary<MyThing, MyThing> _classDictionary = new()
        {
            { ThingKey, ThingValue }
        };
        private readonly Dictionary<Guid, Guid> _guidDictionary = new()
        {
            { GuidKey, GuidValue }
        };
        private readonly Dictionary<string, string> _stringDictionary = new()
        {
            { "Foo", "Bar" }
        };

        private static readonly Guid GuidKey = new("edae0d73-8e4c-4251-85c8-e5497c7ccad1");
        private static readonly Guid GuidValue = new("fa1e5f58-578f-43d4-b4d6-67eae06a5d17");
        private static readonly MyThing ThingKey = new();
        private static readonly MyThing ThingValue = new();
    }
}