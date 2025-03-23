public class ShouldCompleteInExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldCompleteInExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldCompleteIn()
    {
        DocExampleWriter.Document(
            () =>
            {
                Should.CompleteIn(
                    action: () => { Thread.Sleep(TimeSpan.FromSeconds(15)); },
                    timeout: TimeSpan.FromSeconds(0.5),
                    customMessage: "Some additional context");
            },
            _testOutputHelper);
    }
}