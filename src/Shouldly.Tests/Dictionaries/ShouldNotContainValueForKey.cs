namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldNotContainValueForKey
{
    [Fact]
    public void ClassScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassDictionary()
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
    public void GuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                GuidDictionary().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"),

            errorWithSource:
            @"GuidDictionary()
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
    public void KeyAndValueExistShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassDictionary()
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
    public void NoKeyExistsShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassDictionary()
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
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                StringDictionary().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"),

            errorWithSource:
            @"StringDictionary()
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
    public void ValueIsNullShouldFailForDictionary()
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
    public void ClassScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIDictionary()
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
    public void GuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIDictionary().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"),

            errorWithSource:
            @"GuidIDictionary()
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
    public void KeyAndValueExistShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIDictionary()
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
    public void NoKeyExistsShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIDictionary()
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
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                StringIDictionary().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"),

            errorWithSource:
            @"StringIDictionary()
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
    public void ValueIsNullShouldFailForIDictionary()
    {
        IDictionary<MyThing, MyThing?> dictionary = new Dictionary<MyThing, MyThing?>
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
    public void ClassScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                ClassKeyValuePairList().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassKeyValuePairList()
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
    public void GuidScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                GuidKeyValuePairList().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"),

            errorWithSource:
            @"GuidKeyValuePairList()
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
    public void KeyAndValueExistShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                ClassKeyValuePairList().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassKeyValuePairList()
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
    public void NoKeyExistsShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                ClassKeyValuePairList().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassKeyValuePairList()
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
    public void StringScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                StringKeyValuePairList().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"),

            errorWithSource:
            @"StringKeyValuePairList()
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
    public void ValueIsNullShouldFailForKeyValuePairList()
    {
        var dictionary = new List<KeyValuePair<MyThing, MyThing?>>
        {
            new(ThingKey, null )
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
    public void ClassScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                ClassIEnumerableOfKeyValuePair().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIEnumerableOfKeyValuePair()
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
    public void GuidScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                GuidIEnumerableOfKeyValuePair().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"),

            errorWithSource:
            @"GuidIEnumerableOfKeyValuePair()
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
    public void KeyAndValueExistShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                ClassIEnumerableOfKeyValuePair().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIEnumerableOfKeyValuePair()
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
    public void NoKeyExistsShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                ClassIEnumerableOfKeyValuePair().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIEnumerableOfKeyValuePair()
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
    public void StringScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                StringIEnumerableOfKeyValuePair().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"),

            errorWithSource:
            @"StringIEnumerableOfKeyValuePair()
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
    public void ValueIsNullShouldFailForIEnumerableOfKeyValuePair()
    {
        IEnumerable<KeyValuePair<MyThing, MyThing?>> dictionary = new List<KeyValuePair<MyThing, MyThing?>>
        {
            new(ThingKey, null)
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

#if NET9_0_OR_GREATER
    [Fact]
    public void ClassScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIReadOnlyDictionary()
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
    public void GuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIReadOnlyDictionary().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"),

            errorWithSource:
            @"GuidIReadOnlyDictionary()
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
    public void KeyAndValueExistShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIReadOnlyDictionary()
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
    public void NoKeyExistsShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIReadOnlyDictionary()
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
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                StringIReadOnlyDictionary().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"),

            errorWithSource:
            @"StringIReadOnlyDictionary()
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
    public void ValueIsNullShouldFailForIReadOnlyDictionary()
    {
        IReadOnlyDictionary<MyThing, MyThing?> dictionary = new Dictionary<MyThing, MyThing?>
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
#endif

    [Fact]
    public void ShouldPass()
    {
        ClassDictionary().ShouldNotContainValueForKey(ThingKey, new());
        GuidDictionary().ShouldNotContainValueForKey(GuidKey, Guid.NewGuid());
        StringDictionary().ShouldNotContainValueForKey("Foo", "baz");

        ClassIDictionary().ShouldNotContainValueForKey(ThingKey, new());
        GuidIDictionary().ShouldNotContainValueForKey(GuidKey, Guid.NewGuid());
        StringIDictionary().ShouldNotContainValueForKey("Foo", "baz");

#if NET9_0_OR_GREATER
        ClassIReadOnlyDictionary().ShouldNotContainValueForKey(ThingKey, new());
        GuidIReadOnlyDictionary().ShouldNotContainValueForKey(GuidKey, Guid.NewGuid());
        StringIReadOnlyDictionary().ShouldNotContainValueForKey("Foo", "baz");
#endif
    }
}
