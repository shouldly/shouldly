using System.Collections.ObjectModel;

namespace Shouldly.Tests.Dictionaries;

public class ShouldNotContainValueForKey
{
    [Theory]
    [MemberData(nameof(ClassDictionaries))]
    private void ClassScenarioShouldFail(IEnumerable<KeyValuePair<MyThing, MyThing>> classDictionary)
    {
        Verify.ShouldFail(() =>
                classDictionary.ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"classDictionary
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

    [Theory]
    [MemberData(nameof(GuidDictionaries))]
    public void GuidScenarioShouldFail(IEnumerable<KeyValuePair<Guid, Guid>> guidDictionary)
    {
        Verify.ShouldFail(() =>
                guidDictionary.ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"),

            errorWithSource:
            @"guidDictionary
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

    [Theory]
    [MemberData(nameof(StringDictionaries))]
    public void StringScenarioShouldFail(IEnumerable<KeyValuePair<string, string>> stringDictionary)
    {
        Verify.ShouldFail(() =>
                stringDictionary.ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"),

            errorWithSource:
            @"stringDictionary
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
        foreach (var classDictionary in ClassDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<MyThing, MyThing>>>())
        {
            classDictionary.ShouldNotContainValueForKey(ThingKey, new MyThing());
        }

        foreach (var guidDictionary in GuidDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<Guid, Guid>>>())
        {
            guidDictionary.ShouldNotContainValueForKey(GuidKey, Guid.NewGuid());
        }

        foreach (var stringDictionary in StringDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<string, string>>>())
        {
            stringDictionary.ShouldNotContainValueForKey("Foo", "baz");
        }
    }

    private static readonly MyThing ThingKey = new MyThing();
    private static readonly MyThing ThingValue = new MyThing();
    private static readonly Dictionary<MyThing, MyThing> _classDictionary = new Dictionary<MyThing, MyThing>
    {
        { ThingKey, ThingValue }
    };

    private static readonly Guid GuidKey = new Guid("edae0d73-8e4c-4251-85c8-e5497c7ccad1");
    private static readonly Guid GuidValue = new Guid("fa1e5f58-578f-43d4-b4d6-67eae06a5d17");
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