using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.Dictionaries
{
    public class ShouldContainKeyAndValue
    {
        [Fact]
        public void ShouldContainKeyAndValueWithClassesShouldFail()
        {
            Verify.ShouldFail(() =>
    _classDictionary.ShouldContainKeyAndValue(new MyThing(), new MyThing(), "Some additional context"),

errorWithSource:
@"_classDictionary
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but the key does not exist

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but the key does not exist

Additional Info:
    Some additional context");
        }

        [Fact]
        public void GuidScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    _guidDictionary.ShouldContainKeyAndValue(_missingGuidKey, _missingGuidValue, "Some additional context"),

errorWithSource:
@"_guidDictionary
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[efc7ee91-6b19-4dff-88a8-affae77ad870 => b951fb9f-07c3-4060-bd80-055e63946497]]
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context");
        }

        [Fact]
        public void OnlyKeyMatchesShouldFail()
        {
            Verify.ShouldFail(() =>
_classDictionary.ShouldContainKeyAndValue(ThingKey, new MyThing(), "Some additional context"),

errorWithSource:
@"_classDictionary
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but value was
Shouldly.Tests.TestHelpers.MyThing (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but value was
Shouldly.Tests.TestHelpers.MyThing (000000)

Additional Info:
    Some additional context");
        }

        [Fact]
        public void OnlyValueMatchesShouldFail()
        {
            Verify.ShouldFail(() =>
    _classDictionary.ShouldContainKeyAndValue(new MyThing(), ThingValue, "Some additional context"),

 errorWithSource:
@"_classDictionary
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but the key does not exist

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should contain key
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
_stringDictionary.ShouldContainKeyAndValue("bar", "baz", "Some additional context"),

errorWithSource:
@"_stringDictionary
    should contain key
""bar""
    with value
""baz""
    but the key does not exist

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[""Foo"" => ""Bar""]]
    should contain key
""bar""
    with value
""baz""
    but the key does not exist

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ValueIsNullShouldFail()
        {
            var dictionaryWithNullValue = new Dictionary<MyThing, MyThing?>
            {
                { ThingKey, null }
            };
            Verify.ShouldFail(() =>
dictionaryWithNullValue.ShouldContainKeyAndValue(ThingKey, new MyThing(), "Some additional context"),

errorWithSource:
@"dictionaryWithNullValue
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but value was
null

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => null]]
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    with value
Shouldly.Tests.TestHelpers.MyThing (000000)
    but value was
null

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            _classDictionary.ShouldContainKeyAndValue(ThingKey, ThingValue);
            _guidDictionary.ShouldContainKeyAndValue(GuidKey, GuidValue);
            _stringDictionary.ShouldContainKeyAndValue("Foo", "Bar");
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

        private static readonly MyThing ThingKey = new();
        private static readonly MyThing ThingValue = new();

        private static readonly Guid GuidKey = new("efc7ee91-6b19-4dff-88a8-affae77ad870");
        private static readonly Guid GuidValue = new("b951fb9f-07c3-4060-bd80-055e63946497");
        private readonly Guid _missingGuidKey = new("1924e617-2fc2-47ae-ad38-b6f30ec2226b");
        private readonly Guid _missingGuidValue = new("f08a0b08-c9f4-49bb-a4d4-be06e88b69c8");
    }
}