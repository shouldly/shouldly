public class ShouldBeOfTypeExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldBeOfTypeExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldBeOfType()
    {
        DocExampleWriter.Document(
            () =>
            {
                var theSimpsonsDog = new Cat { Name = "Santas little helper" };
                theSimpsonsDog.ShouldBeOfType<Dog>();
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotBeOfType()
    {
        DocExampleWriter.Document(
            () =>
            {
                var theSimpsonsDog = new Cat { Name = "Santas little helper" };
                theSimpsonsDog.ShouldNotBeOfType<Cat>();
            },
            _testOutputHelper);
    }
}