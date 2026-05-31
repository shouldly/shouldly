public class ShouldSatisfyAllConditionsExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldSatisfyAllConditionsExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldSatisfyAllConditions()
    {
        DocExampleWriter.Document(
            () =>
            {
                var mrBurns = new Person { Name = null };
                mrBurns.ShouldSatisfy(
                [
                    () => mrBurns.Name.ShouldNotBeNullOrEmpty(),
                    () => mrBurns.Name.ShouldBe("Mr.Burns")
                ]);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldSatisfyAllConditionsGeneric()
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
}