using System.Collections.ObjectModel;

namespace Shouldly.Tests.Dictionaries;

public class ShouldContainKeyAndValue
{
    [Theory]
    [MemberData(nameof(ClassDictionaries))]
    private void ShouldContainKeyAndValueWithClassesShouldFail(IEnumerable<KeyValuePair<MyThing, MyThing>> classDictionary)
    {
        Verify.ShouldFail(() =>
                classDictionary.ShouldContainKeyAndValue(new(), new(), "Some additional context"),

            errorWithSource:
            @"classDictionary
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

    [Theory]
    [MemberData(nameof(GuidDictionaries))]
    public void GuidScenarioShouldFail(IEnumerable<KeyValuePair<Guid, Guid>> guidDictionary)
    {
        Verify.ShouldFail(() =>
                guidDictionary.ShouldContainKeyAndValue(_missingGuidKey, _missingGuidValue, "Some additional context"),

            errorWithSource:
            @"guidDictionary
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

    [Theory]
    [MemberData(nameof(ClassDictionaries))]
    private void OnlyKeyMatchesShouldFail(IEnumerable<KeyValuePair<MyThing, MyThing>> classDictionary)
    {
        Verify.ShouldFail(() =>
                classDictionary.ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"),

            errorWithSource:
            @"classDictionary
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

    [Theory]
    [MemberData(nameof(ClassDictionaries))]
    private void OnlyValueMatchesShouldFail(IEnumerable<KeyValuePair<MyThing, MyThing>> classDictionary)
    {
        Verify.ShouldFail(() =>
                classDictionary.ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"classDictionary
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

    [Theory]
    [MemberData(nameof(StringDictionaries))]
    public void StringScenarioShouldFail(IEnumerable<KeyValuePair<string, string>> stringDictionary)
    {
        Verify.ShouldFail(() =>
                stringDictionary.ShouldContainKeyAndValue("bar", "baz", "Some additional context"),

            errorWithSource:
            @"stringDictionary
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
                dictionaryWithNullValue.ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"),

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
        foreach (var classDictionary in ClassDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<MyThing, MyThing>>>())
        {
            classDictionary.ShouldContainKeyAndValue(ThingKey, ThingValue);
        }

        foreach (var guidDictionary in GuidDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<Guid, Guid>>>())
        {
            guidDictionary.ShouldContainKeyAndValue(GuidKey, GuidValue);
        }

        foreach (var stringDictionary in StringDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<string, string>>>())
        {
            stringDictionary.ShouldContainKeyAndValue("Foo", "Bar");
        }
    }

    private static readonly MyThing ThingKey = new MyThing();
    private static readonly MyThing ThingValue = new MyThing();
    private static readonly Dictionary<MyThing, MyThing> _classDictionary = new Dictionary<MyThing, MyThing>
    {
        { ThingKey, ThingValue }
    };

    private static readonly Guid GuidKey = new Guid("efc7ee91-6b19-4dff-88a8-affae77ad870");
    private static readonly Guid GuidValue = new Guid("b951fb9f-07c3-4060-bd80-055e63946497");
    private readonly Guid _missingGuidKey = new Guid("1924e617-2fc2-47ae-ad38-b6f30ec2226b");
    private readonly Guid _missingGuidValue = new Guid("f08a0b08-c9f4-49bb-a4d4-be06e88b69c8");
    private static readonly Dictionary<Guid, Guid> _guidDictionary = new Dictionary<Guid, Guid>
    {
        { GuidKey, GuidValue }
    };
    private static readonly Dictionary<string, string> _stringDictionary = new Dictionary<string, string>
    {
        { "Foo", "Bar" }
    };

    public static IEnumerable<object[]> ClassDictionaries()
    {
        yield return new[] { _classDictionary };
        yield return new[] { new ReadOnlyDictionary<MyThing, MyThing>(_classDictionary) };
        yield return new[] { _classDictionary.ToArray() };
        yield return new[] { _classDictionary.ToList() };
    }

    public static IEnumerable<object[]> StringDictionaries()
    {
        yield return new[] { _stringDictionary };
        yield return new[] { new ReadOnlyDictionary<string, string>(_stringDictionary) };
        yield return new[] { _stringDictionary.ToArray() };
        yield return new[] { _stringDictionary.ToList() };
    }

    public static IEnumerable<object[]> GuidDictionaries()
    {
        yield return new[] { _guidDictionary };
        yield return new[] { new ReadOnlyDictionary<Guid, Guid>(_guidDictionary) };
        yield return new[] { _guidDictionary.ToArray() };
        yield return new[] { _guidDictionary.ToList() };
    }
}