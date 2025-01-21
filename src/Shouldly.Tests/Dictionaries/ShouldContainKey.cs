namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldContainKey
{
    [Fact]
    public void ClassScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                ClassDictionary().ShouldContainKey(new(), "Some additional context"),

            errorWithSource:
            """
            ClassDictionary()
                should contain key
            Shouldly.Tests.TestHelpers.MyThing (000000)
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
                should contain key
            Shouldly.Tests.TestHelpers.MyThing (000000)
                but does not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void GuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                GuidDictionary().ShouldContainKey(MissingGuidKey, "Some additional context"),

            errorWithSource:
            """
            GuidDictionary()
                should contain key
            1924e617-2fc2-47ae-ad38-b6f30ec2226b
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
                should contain key
            1924e617-2fc2-47ae-ad38-b6f30ec2226b
                but does not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
                StringDictionary().ShouldContainKey("bar", "Some additional context"),

            errorWithSource:
            """
            StringDictionary()
                should contain key
            "bar"
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [["Foo" => "Bar"]]
                should contain key
            "bar"
                but does not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ClassScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIDictionary().ShouldContainKey(new(), "Some additional context"),

            errorWithSource:
            """
            ClassIDictionary()
                should contain key
            Shouldly.Tests.TestHelpers.MyThing (000000)
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
                should contain key
            Shouldly.Tests.TestHelpers.MyThing (000000)
                but does not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void GuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIDictionary().ShouldContainKey(MissingGuidKey, "Some additional context"),

            errorWithSource:
            """
            GuidIDictionary()
                should contain key
            1924e617-2fc2-47ae-ad38-b6f30ec2226b
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
                should contain key
            1924e617-2fc2-47ae-ad38-b6f30ec2226b
                but does not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
                StringIDictionary().ShouldContainKey("bar", "Some additional context"),

            errorWithSource:
            """
            StringIDictionary()
                should contain key
            "bar"
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [["Foo" => "Bar"]]
                should contain key
            "bar"
                but does not

            Additional Info:
                Some additional context
            """);
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void ClassScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                ClassIReadOnlyDictionary().ShouldContainKey(new(), "Some additional context"),

            errorWithSource:
            """
            ClassIReadOnlyDictionary()
                should contain key
            Shouldly.Tests.TestHelpers.MyThing (000000)
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [[Shouldly.Tests.TestHelpers.MyThing (000000) => Shouldly.Tests.TestHelpers.MyThing (000000)]]
                should contain key
            Shouldly.Tests.TestHelpers.MyThing (000000)
                but does not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void GuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                GuidIReadOnlyDictionary().ShouldContainKey(MissingGuidKey, "Some additional context"),

            errorWithSource:
            """
            GuidIReadOnlyDictionary()
                should contain key
            1924e617-2fc2-47ae-ad38-b6f30ec2226b
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [[edae0d73-8e4c-4251-85c8-e5497c7ccad1 => fa1e5f58-578f-43d4-b4d6-67eae06a5d17]]
                should contain key
            1924e617-2fc2-47ae-ad38-b6f30ec2226b
                but does not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
                StringIReadOnlyDictionary().ShouldContainKey("bar", "Some additional context"),

            errorWithSource:
            """
            StringIReadOnlyDictionary()
                should contain key
            "bar"
                but does not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [["Foo" => "Bar"]]
                should contain key
            "bar"
                but does not

            Additional Info:
                Some additional context
            """);
    }
#endif

    [Fact]
    public void ShouldPass()
    {
        ClassDictionary().ShouldContainKey(ThingKey);
        GuidDictionary().ShouldContainKey(GuidKey);
        StringDictionary().ShouldContainKey("Foo");

        ClassIDictionary().ShouldContainKey(ThingKey);
        GuidIDictionary().ShouldContainKey(GuidKey);
        StringIDictionary().ShouldContainKey("Foo");

#if NET9_0_OR_GREATER
        ClassIReadOnlyDictionary().ShouldContainKey(ThingKey);
        GuidIReadOnlyDictionary().ShouldContainKey(GuidKey);
        StringIReadOnlyDictionary().ShouldContainKey("Foo");
#endif
    }
}