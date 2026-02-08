namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldNotContainKey
{
    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldNotContainKey(ThingKey, "Some additional context"));
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            GuidDictionary().ShouldNotContainKey(GuidKey, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            StringDictionary().ShouldNotContainKey("Foo", "Some additional context"));
    }

    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldNotContainKey(ThingKey, "Some additional context"));
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIDictionary().ShouldNotContainKey(GuidKey, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            StringIDictionary().ShouldNotContainKey("Foo", "Some additional context"));
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void ShouldNotContainKeyClassScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldNotContainKey(ThingKey, "Some additional context"));
    }

    [Fact]
    public void ShouldNotContainKeyGuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIReadOnlyDictionary().ShouldNotContainKey(GuidKey, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            StringIReadOnlyDictionary().ShouldNotContainKey("Foo", "Some additional context"));
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