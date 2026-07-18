namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldNotContainValueForKey
{
    [Fact]
    public void ClassScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            GuidDictionary().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"));
    }

    [Fact]
    public void KeyAndValueExistShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"));
    }

    [Fact]
    public void NoKeyExistsShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            StringDictionary().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"));
    }

    [Fact]
    public void ValueIsNullShouldFailForDictionary()
    {
        var dictionary = new Dictionary<MyThing, MyThing?>
        {
            { ThingKey, null }
        };
        Verify.ShouldFail(() =>
            dictionary.ShouldNotContainValueForKey(ThingKey, null, "Some additional context"));
    }


    [Fact]
    public void ClassScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIDictionary().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"));
    }

    [Fact]
    public void KeyAndValueExistShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"));
    }

    [Fact]
    public void NoKeyExistsShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            StringIDictionary().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"));
    }

    [Fact]
    public void ValueIsNullShouldFailForIDictionary()
    {
        IDictionary<MyThing, MyThing?> dictionary = new Dictionary<MyThing, MyThing?>
        {
            { ThingKey, null }
        };
        Verify.ShouldFail(() =>
            dictionary.ShouldNotContainValueForKey(ThingKey, null, "Some additional context"));
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void ClassScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIReadOnlyDictionary().ShouldNotContainValueForKey(GuidKey, GuidValue, "Some additional context"));
    }

    [Fact]
    public void KeyAndValueExistShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldNotContainValueForKey(ThingKey, ThingValue, "Some additional context"));
    }

    [Fact]
    public void NoKeyExistsShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldNotContainValueForKey(new(), ThingValue, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            StringIReadOnlyDictionary().ShouldNotContainValueForKey("Foo", "Bar", "Some additional context"));
    }

    [Fact]
    public void ValueIsNullShouldFailForIReadOnlyDictionary()
    {
        IReadOnlyDictionary<MyThing, MyThing?> dictionary = new Dictionary<MyThing, MyThing?>
        {
            { ThingKey, null }
        };
        Verify.ShouldFail(() =>
            dictionary.ShouldNotContainValueForKey(ThingKey, null, "Some additional context"));
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
