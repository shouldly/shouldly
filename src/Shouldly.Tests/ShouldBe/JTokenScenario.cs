namespace Shouldly.Tests.ShouldBe
{
    /// <summary>
    /// Strange emulates JToken, a class which can be implicitly cast to from a string which is IEnumerable, 
    /// but enumerable can be empty which means we get a false pass. 
    /// 
    /// To make this test pass, for types like JToken and Strange we have to use .Equals not compare them as Enumerables
    /// </summary>
    public class JTokenScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new Strange().ShouldBe("string");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new Strange() should be [] (string) but was [] (null) difference[]"; }
        }

        protected override void ShouldPass()
        {
            ((Strange) "string").ShouldBe("string");
        }
    }
}