namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldContainKey
{
    [Fact]
    public void ClassScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldContainKey(new(), "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            GuidDictionary().ShouldContainKey(MissingGuidKey, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            StringDictionary().ShouldContainKey("bar", "Some additional context"));
    }

    [Fact]
    public void ClassScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldContainKey(new(), "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIDictionary().ShouldContainKey(MissingGuidKey, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            StringIDictionary().ShouldContainKey("bar", "Some additional context"));
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void ClassScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldContainKey(new(), "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIReadOnlyDictionary().ShouldContainKey(MissingGuidKey, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            StringIReadOnlyDictionary().ShouldContainKey("bar", "Some additional context"));
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