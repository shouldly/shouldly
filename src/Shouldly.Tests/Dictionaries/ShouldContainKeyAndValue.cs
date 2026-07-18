namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

public class ShouldContainKeyAndValue
{
    [Fact]
    public void ShouldContainKeyAndValueWithClassesShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldContainKeyAndValue(new(), new(), "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            GuidDictionary().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"));
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"));
    }

    [Fact]
    public void OnlyValueMatchesShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            ClassDictionary().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForDictionary()
    {
        Verify.ShouldFail(() =>
            StringDictionary().ShouldContainKeyAndValue("bar", "baz", "Some additional context"));
    }

    [Fact]
    public void ValueIsNullShouldFailForDictionary()
    {
        var dictionaryWithNullValue = new Dictionary<MyThing, MyThing?>
        {
            { ThingKey, null }
        };
        Verify.ShouldFail(() =>
            dictionaryWithNullValue.ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"));
    }

    [Fact]
    public void ShouldContainKeyAndValueWithClassesShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldContainKeyAndValue(new(), new(), "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIDictionary().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"));
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"));
    }

    [Fact]
    public void OnlyValueMatchesShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIDictionary().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIDictionary()
    {
        Verify.ShouldFail(() =>
            StringIDictionary().ShouldContainKeyAndValue("bar", "baz", "Some additional context"));
    }

    [Fact]
    public void ValueIsNullShouldFailForIDictionary()
    {
        IDictionary<MyThing, MyThing?> dictionaryWithNullValue = new Dictionary<MyThing, MyThing?>
        {
            { ThingKey, null }
        };
        Verify.ShouldFail(() =>
            dictionaryWithNullValue.ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"));
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void ShouldContainKeyAndValueWithClassesShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldContainKeyAndValue(new(), new(), "Some additional context"));
    }

    [Fact]
    public void GuidScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            GuidIReadOnlyDictionary().ShouldContainKeyAndValue(MissingGuidKey, MissingGuidValue, "Some additional context"));
    }

    [Fact]
    public void OnlyKeyMatchesShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"));
    }

    [Fact]
    public void OnlyValueMatchesShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            ClassIReadOnlyDictionary().ShouldContainKeyAndValue(new(), ThingValue, "Some additional context"));
    }

    [Fact]
    public void StringScenarioShouldFailForIReadOnlyDictionary()
    {
        Verify.ShouldFail(() =>
            StringIReadOnlyDictionary().ShouldContainKeyAndValue("bar", "baz", "Some additional context"));
    }

    [Fact]
    public void ValueIsNullShouldFailForIReadOnlyDictionary()
    {
        IReadOnlyDictionary<MyThing, MyThing?> dictionaryWithNullValue = new Dictionary<MyThing, MyThing?>
        {
            { ThingKey, null }
        };
        Verify.ShouldFail(() =>
            dictionaryWithNullValue.ShouldContainKeyAndValue(ThingKey, new(), "Some additional context"));
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