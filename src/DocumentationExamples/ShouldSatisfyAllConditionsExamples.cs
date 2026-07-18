public class ShouldSatisfyAllConditionsExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldSatisfyAllConditionsExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldSatisfy()
    {
        DocExampleWriter.Document(
            () =>
            {
                var mrBurns = new Person { Name = null };
                mrBurns.ShouldSatisfy(
                [
                    p => p.Name.ShouldNotBeNullOrEmpty(),
                    p => p.Name.ShouldBe("Mr.Burns")
                ]);
            },
            _testOutputHelper);
    }

    [Fact]
    public void Satisfy()
    {
        DocExampleWriter.Document(
            () =>
            {
                var mrBurns = new Person { Name = null };
                var homer = new Person { Name = "Homer" };
                Should.Satisfy(
                [
                    () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                    () => homer.Name.ShouldBe("Mr.Burns")
                ]);
            },
            _testOutputHelper);
    }
}
