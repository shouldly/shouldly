public class ShouldBeGreater_LessThanExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldBeGreater_LessThanExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldBeGreaterThan()
    {
        DocExampleWriter.Document(
            () =>
            {
                var mrBurns = new Person { Name = "Mr. Burns", Salary = 30000 };
                mrBurns.Salary.ShouldBeGreaterThan(300000000);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeLessThan()
    {
        DocExampleWriter.Document(
            () =>
            {
                var homer = new Person { Name = "Homer", Salary = 300000000 };
                homer.Salary.ShouldBeLessThan(30000);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeGreaterThanOrEqualTo()
    {
        DocExampleWriter.Document(
            () =>
            {
                var mrBurns = new Person { Name = "Mr. Burns", Salary = 299999999 };
                mrBurns.Salary.ShouldBeGreaterThanOrEqualTo(300000000);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeLessThanOrEqualTo()
    {
        DocExampleWriter.Document(
            () =>
            {
                var homer = new Person { Name = "Homer", Salary = 30001 };
                homer.Salary.ShouldBeLessThanOrEqualTo(30000);
            },
            _testOutputHelper);
    }
}