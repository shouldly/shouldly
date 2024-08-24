namespace Shouldly.Tests.Dictionaries;

internal static class DictionaryTestData
{
    public static Guid GuidKey { get; } = new("edae0d73-8e4c-4251-85c8-e5497c7ccad1");
    public static Guid GuidValue { get; } = new("fa1e5f58-578f-43d4-b4d6-67eae06a5d17");
    public static Guid MissingGuidKey { get; } = new("1924e617-2fc2-47ae-ad38-b6f30ec2226b");
    public static Guid MissingGuidValue { get; } = new("f08a0b08-c9f4-49bb-a4d4-be06e88b69c8");
    public static MyThing ThingKey { get; } = new();
    public static MyThing ThingValue { get; } = new();

    public static Dictionary<MyThing, MyThing> ClassDictionary() => new(_classDictionary);
    public static Dictionary<Guid, Guid> GuidDictionary() => new(_guidDictionary);
    public static Dictionary<string, string> StringDictionary() => new(_stringDictionary);

    public static IDictionary<MyThing, MyThing> ClassIDictionary() => ClassDictionary();
    public static IDictionary<Guid, Guid> GuidIDictionary() => GuidDictionary();
    public static IDictionary<string, string> StringIDictionary() => StringDictionary();

    public static IReadOnlyDictionary<MyThing, MyThing> ClassIReadOnlyDictionary() => ClassDictionary();
    public static IReadOnlyDictionary<Guid, Guid> GuidIReadOnlyDictionary() => GuidDictionary();
    public static IReadOnlyDictionary<string, string> StringIReadOnlyDictionary() => StringDictionary();

    private static readonly Dictionary<MyThing, MyThing> _classDictionary = new()
    {
        { ThingKey, ThingValue }
    };
    private static readonly Dictionary<Guid, Guid> _guidDictionary = new()
    {
        { GuidKey, GuidValue }
    };
    private static readonly Dictionary<string, string> _stringDictionary = new()
    {
        { "Foo", "Bar" },
    };
}
