public class EnumerableShouldHaveCountExamples
{
    ITestOutputHelper _testOutputHelper;

    public EnumerableShouldHaveCountExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldHaveCount()
    {
        DocExampleWriter.Document(
            () =>
            {
                var maggie = new Person { Name = "Maggie" };
                var homer = new Person { Name = "Homer" };
                var simpsonsBabies = new List<Person> { homer, maggie };
                simpsonsBabies.ShouldHaveCount(3);
            },
            _testOutputHelper);
    }
}