namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldNotContainKey
{
    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldNotContainKey(ThingKey, "Some additional context"),

            errorWithSource:
            @"ClassDictionary()
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                GuidDictionary().ShouldNotContainKey(GuidKey, "Some additional context"),

            errorWithSource:
            @"GuidDictionary()
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                StringDictionary().ShouldNotContainKey("Foo", "Some additional context"),

            errorWithSource:
            @"StringDictionary()
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[""Foo"" => ""Bar""]]
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldNotContainKey(ThingKey, "Some additional context"),

            errorWithSource:
            @"ClassIDictionary()
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIDictionary().ShouldNotContainKey(GuidKey, "Some additional context"),

            errorWithSource:
            @"GuidIDictionary()
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                StringIDictionary().ShouldNotContainKey("Foo", "Some additional context"),

            errorWithSource:
            @"StringIDictionary()
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[""Foo"" => ""Bar""]]
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                ClassKeyValuePairList().ShouldNotContainKey(ThingKey, "Some additional context"),

            errorWithSource:
            @"ClassKeyValuePairList()
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                GuidKeyValuePairList().ShouldNotContainKey(GuidKey, "Some additional context"),

            errorWithSource:
            @"GuidKeyValuePairList()
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void StringScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                StringKeyValuePairList().ShouldNotContainKey("Foo", "Some additional context"),

            errorWithSource:
            @"StringKeyValuePairList()
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[""Foo"" => ""Bar""]]
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                ClassIEnumerableOfKeyValuePair().ShouldNotContainKey(ThingKey, "Some additional context"),

            errorWithSource:
            @"ClassIEnumerableOfKeyValuePair()
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                GuidIEnumerableOfKeyValuePair().ShouldNotContainKey(GuidKey, "Some additional context"),

            errorWithSource:
            @"GuidIEnumerableOfKeyValuePair()
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void StringScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                StringIEnumerableOfKeyValuePair().ShouldNotContainKey("Foo", "Some additional context"),

            errorWithSource:
            @"StringIEnumerableOfKeyValuePair()
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[""Foo"" => ""Bar""]]
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context");
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldNotContainKey(ThingKey, "Some additional context"),

            errorWithSource:
            @"ClassIReadOnlyDictionary()
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
    should not contain key
Shouldly.Tests.TestHelpers.MyThing (000000)
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIReadOnlyDictionary().ShouldNotContainKey(GuidKey, "Some additional context"),

            errorWithSource:
            @"GuidIReadOnlyDictionary()
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should not contain key
edae0d73-8e4c-4251-85c8-e5497c7ccad1
    but does

Additional Info:
    Some additional context");
    }

    [Fact]
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                StringIReadOnlyDictionary().ShouldNotContainKey("Foo", "Some additional context"),

            errorWithSource:
            @"StringIReadOnlyDictionary()
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[""Foo"" => ""Bar""]]
    should not contain key
""Foo""
    but does

Additional Info:
    Some additional context");
    }
#endif

    [Fact]
    public void ShouldPass()
    {
        ClassDictionary().ShouldNotContainKey(new());
        GuidDictionary().ShouldNotContainKey(Guid.NewGuid());
        StringDictionary().ShouldNotContainKey("bar");

        ClassIDictionary().ShouldNotContainKey(new());
        GuidIDictionary().ShouldNotContainKey(Guid.NewGuid());
        StringIDictionary().ShouldNotContainKey("bar");

#if NET9_0_OR_GREATER
        ClassIReadOnlyDictionary().ShouldNotContainKey(new());
        GuidIReadOnlyDictionary().ShouldNotContainKey(Guid.NewGuid());
        StringIReadOnlyDictionary().ShouldNotContainKey("bar");
#endif
    }
}