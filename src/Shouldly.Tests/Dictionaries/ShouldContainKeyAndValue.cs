namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldContainKeyAndValue
{
    [Fact]
    public void ShouldContainKeyAndValueWithClassesShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldContainKeyAndValue(new(), new(), "Some additional context"),

            errorWithSource:
            @"ClassDictionary()
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
    public void GuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                GuidDictionary().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"),

            errorWithSource:
            @"GuidDictionary()
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context");
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"),

            errorWithSource:
            @"ClassDictionary()
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
    public void OnlyValueMatchesShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassDictionary()
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
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                StringDictionary().ShouldContainKeyAndValue("bar", "baz", "Some additional context"),

            errorWithSource:
            @"StringDictionary()
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
    public void ValueIsNullShouldFailForDictionary()
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
    public void ShouldContainKeyAndValueWithClassesShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldContainKeyAndValue(new(), new(), "Some additional context"),

            errorWithSource:
            @"ClassIDictionary()
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
    public void GuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIDictionary().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"),

            errorWithSource:
            @"GuidIDictionary()
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context");
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"),

            errorWithSource:
            @"ClassIDictionary()
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
    public void OnlyValueMatchesShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIDictionary()
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
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                StringIDictionary().ShouldContainKeyAndValue("bar", "baz", "Some additional context"),

            errorWithSource:
            @"StringIDictionary()
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
    public void ValueIsNullShouldFailForIDictionary()
    {
        IDictionary<MyThing, MyThing?> dictionaryWithNullValue = new Dictionary<MyThing, MyThing?>
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
    public void ShouldContainKeyAndValueWithClassesShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                ClassKeyValuePairList().ShouldContainKeyAndValue(new(), new(), "Some additional context"),

            errorWithSource:
            @"ClassKeyValuePairList()
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
    public void GuidScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                GuidKeyValuePairList().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"),

            errorWithSource:
            @"GuidKeyValuePairList()
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context");
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                ClassKeyValuePairList().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"),

            errorWithSource:
            @"ClassKeyValuePairList()
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
    public void OnlyValueMatchesShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                ClassKeyValuePairList().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassKeyValuePairList()
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
    public void StringScenarioShouldFailForKeyValuePairList()
    {
        Verify.ShouldFail(() =>
                StringKeyValuePairList().ShouldContainKeyAndValue("bar", "baz", "Some additional context"),

            errorWithSource:
            @"StringKeyValuePairList()
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
    public void ValueIsNullShouldFailForKeyValuePairList()
    {
        var dictionaryWithNullValue = new List<KeyValuePair<MyThing, MyThing?>>
        {
            new(ThingKey, null)
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
    public void ShouldContainKeyAndValueWithClassesShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                ClassIEnumerableOfKeyValuePair().ShouldContainKeyAndValue(new(), new(), "Some additional context"),

            errorWithSource:
            @"ClassIEnumerableOfKeyValuePair()
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
    public void GuidScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                GuidIEnumerableOfKeyValuePair().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"),

            errorWithSource:
            @"GuidIEnumerableOfKeyValuePair()
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context");
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                ClassIEnumerableOfKeyValuePair().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"),

            errorWithSource:
            @"ClassIEnumerableOfKeyValuePair()
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
    public void OnlyValueMatchesShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                ClassIEnumerableOfKeyValuePair().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIEnumerableOfKeyValuePair()
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
    public void StringScenarioShouldFailForIEnumerableOfKeyValuePair()
    {
        Verify.ShouldFail(() =>
                StringIEnumerableOfKeyValuePair().ShouldContainKeyAndValue("bar", "baz", "Some additional context"),

            errorWithSource:
            @"StringIEnumerableOfKeyValuePair()
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
    public void ValueIsNullShouldFailForIEnumerableOfKeyValuePair()
    {
        IEnumerable<KeyValuePair<MyThing, MyThing?>> dictionaryWithNullValue = new List<KeyValuePair<MyThing, MyThing?>>
        {
           new( ThingKey, null )
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

#if NET9_0_OR_GREATER
    [Fact]
    public void ShouldContainKeyAndValueWithClassesShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldContainKeyAndValue(new(), new(), "Some additional context"),

            errorWithSource:
            @"ClassIReadOnlyDictionary()
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
    public void GuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIReadOnlyDictionary().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"),

            errorWithSource:
            @"GuidIReadOnlyDictionary()
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
    should contain key
1924e617-2fc2-47ae-ad38-b6f30ec2226b
    with value
f08a0b08-c9f4-49bb-a4d4-be06e88b69c8
    but the key does not exist

Additional Info:
    Some additional context");
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"),

            errorWithSource:
            @"ClassIReadOnlyDictionary()
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
    public void OnlyValueMatchesShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"),

            errorWithSource:
            @"ClassIReadOnlyDictionary()
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
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                StringIReadOnlyDictionary().ShouldContainKeyAndValue("bar", "baz", "Some additional context"),

            errorWithSource:
            @"StringIReadOnlyDictionary()
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
    public void ValueIsNullShouldFailForIReadOnlyDictionary()
    {
        IReadOnlyDictionary<MyThing, MyThing?> dictionaryWithNullValue = new Dictionary<MyThing, MyThing?>
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
#endif

    [Fact]
    public void ShouldPass()
    {
        ClassDictionary().ShouldContainKeyAndValue(ThingKey, ThingValue);
        GuidDictionary().ShouldContainKeyAndValue(GuidKey, GuidValue);
        StringDictionary().ShouldContainKeyAndValue("Foo", "Bar");

        ClassIDictionary().ShouldContainKeyAndValue(ThingKey, ThingValue);
        GuidIDictionary().ShouldContainKeyAndValue(GuidKey, GuidValue);
        StringIDictionary().ShouldContainKeyAndValue("Foo", "Bar");

#if NET9_0_OR_GREATER
        ClassIReadOnlyDictionary().ShouldContainKeyAndValue(ThingKey, ThingValue);
        GuidIReadOnlyDictionary().ShouldContainKeyAndValue(GuidKey, GuidValue);
        StringIReadOnlyDictionary().ShouldContainKeyAndValue("Foo", "Bar");
#endif
    }
}