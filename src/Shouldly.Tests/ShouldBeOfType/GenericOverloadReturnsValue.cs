using NUnit.Framework;

namespace Shouldly.Tests.ShouldBeOfType
{
    public class GenericOverloadReturnsValue
    {
        [Test]
        public void ValueShouldBeReturnedWhenSuccessful()
        {
            object val = "Foo";
            // ReSharper disable once SuggestUseVarKeywordEvident
            string returnedVal = val.ShouldBeOfType<string>();
            returnedVal.ShouldBe("Foo");
        }
    }
}