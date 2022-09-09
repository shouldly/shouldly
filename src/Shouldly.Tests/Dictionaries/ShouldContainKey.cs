using System.Collections.ObjectModel;

namespace Shouldly.Tests.Dictionaries;

public class ShouldContainKey
{
    [Theory]
    [MemberData(nameof(ClassDictionaries))]
    private void ClassScenarioShouldFail(IEnumerable<KeyValuePair<MyThing, MyThing>> classDictionary)
    {
        Verify.ShouldFail(() =>
classDictionary.ShouldContainKey(new MyThing(), "Some additional context"),

errorWithSource:
@"classDictionary
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does not

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does not

Additional Info:
    Some additional context");
    }

    [Theory]
    [MemberData(nameof(GuidDictionaries))]
    public void GuidScenarioShouldFail(IEnumerable<KeyValuePair<Guid, Guid>> guidDictionary)
    {
        Verify.ShouldFail(() =>
guidDictionary.ShouldContainKey(_missingGuid, "Some additional context"),

errorWithSource:
@"guidDictionary
    should contain key
5250646b-4c46-4b0e-86d8-e1421f2a0ea2
    but does not

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[468a57a7-ca19-4b76-a1e3-3040719392bc => a9db46cc-9d3c-4595-ae1b-8e33de4cc6e5]]
    should contain key
5250646b-4c46-4b0e-86d8-e1421f2a0ea2
    but does not

Additional Info:
    Some additional context");
    }

    [Theory]
    [MemberData(nameof(StringDictionaries))]
    public void StringScenarioShouldFail(IEnumerable<KeyValuePair<string, string>> stringDictionary)
    {
        Verify.ShouldFail(() =>
stringDictionary.ShouldContainKey("bar", "Some additional context"),

errorWithSource:
@"stringDictionary
    should contain key
""bar""
    but does not

Additional Info:
    Some additional context",

errorWithoutSource:
@"[[""Foo"" => """"]]
    should contain key
""bar""
    but does not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        foreach (var classDictionary in ClassDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<MyThing, MyThing>>>())
        {
            classDictionary.ShouldContainKey(ThingKey);
        }

        foreach (var guidDictionary in GuidDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<Guid, Guid>>>())
        {
            guidDictionary.ShouldContainKey(GuidKey);
        }

        foreach (var stringDictionary in StringDictionaries().SelectMany(x => x).OfType<IEnumerable<KeyValuePair<string, string>>>())
        {
            stringDictionary.ShouldContainKey("Foo");
        }
    }

    private static readonly MyThing ThingKey = new MyThing();
    private static readonly Dictionary<MyThing, MyThing> _classDictionary = new Dictionary<MyThing, MyThing>
    {
        { ThingKey, new MyThing() }
    };

    private static readonly Guid GuidKey = new Guid("468a57a7-ca19-4b76-a1e3-3040719392bc");
    private readonly Guid _missingGuid = new Guid("5250646b-4c46-4b0e-86d8-e1421f2a0ea2");
    private static readonly Dictionary<Guid, Guid> _guidDictionary = new Dictionary<Guid, Guid>
    {
        { GuidKey, new Guid("a9db46cc-9d3c-4595-ae1b-8e33de4cc6e5") }
    };

    private static readonly Dictionary<string, string> _stringDictionary = new Dictionary<string, string>
    {
        { "Foo", "" }
    };

    public static IEnumerable<object[]> ClassDictionaries()
    {
        yield return new[] { _classDictionary };
        yield return new[] { new ReadOnlyDictionary<MyThing, MyThing>(_classDictionary) };
        yield return new[] { _classDictionary.ToArray() };
    }

    public static IEnumerable<object[]> StringDictionaries()
    {
        yield return new[] { _stringDictionary };
        yield return new[] { new ReadOnlyDictionary<string, string>(_stringDictionary) };
        yield return new[] { _stringDictionary.ToArray() };
    }

    public static IEnumerable<object[]> GuidDictionaries()
    {
        yield return new[] { _guidDictionary };
        yield return new[] { new ReadOnlyDictionary<Guid, Guid>(_guidDictionary) };
        yield return new[] { _guidDictionary.ToArray() };
    }
}