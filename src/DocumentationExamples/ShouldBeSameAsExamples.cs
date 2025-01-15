public class ShouldBeSameAsExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldBeSameAsExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldBeSameAs()
    {
        DocExampleWriter.Document(
            () =>
            {
                var principleSkinner = new Person { Name = "Armin Tamzarian" };
                var seymourSkinner = new Person { Name = "Seymour Skinner" };

                principleSkinner.ShouldBeSameAs(seymourSkinner);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeSameAs()
    {
        DocExampleWriter.Document(
            () =>
            {
                var person = new Person { Name = "Armin Tamzarian" };
                person.ShouldNotBeSameAs(person);
            },
            _testOutputHelper);
    }
}