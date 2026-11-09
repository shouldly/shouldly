public class ShouldBeCompletedSuccessfullyExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldBeCompletedSuccessfullyExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldBeCompletedSuccessfully()
    {
        DocExampleWriter.Document(
            () =>
            {
                var lookup = Task.FromException<int>(new InvalidOperationException("Cache miss"));
                var value = lookup.ShouldBeCompletedSuccessfully();
            },
            _testOutputHelper);
    }
}
