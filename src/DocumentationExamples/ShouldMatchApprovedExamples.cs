public class ShouldMatchApprovedExamples
{
    ITestOutputHelper _testOutputHelper;

    public ShouldMatchApprovedExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ApprovedFileDoesNotExist()
    {
        DocExampleWriter.Document(
            () =>
            {
                var simpsonsQuote = "Hi Super Nintendo Chalmers";
                simpsonsQuote.ShouldMatchApproved(c => c.NoDiff());
            },
            _testOutputHelper, c =>
            {
                c.WithScrubber(s => s.Replace("c => c.NoDiff()", string.Empty));
            });
    }

    [Fact]
    public void ApprovedFileIsDifferent()
    {
        DocExampleWriter.Document(
            () =>
            {
                var simpsonsQuote = "Me fail english? That's unpossible";
                simpsonsQuote.ShouldMatchApproved(c => c.NoDiff().WithDiscriminator("Different"));
            },
            _testOutputHelper, c =>
            {
                // Scrubbing the generated docs is easier than altering the infrastructure to support this scenario
                c.WithScrubber(s => s.Replace("c => c.NoDiff().WithDiscriminator(\"Different\")", string.Empty));
                c.WithScrubber(s => s.Replace("ShouldMatchApprovedExamples.ApprovedFileIsDifferent.Different.", "ShouldMatchApprovedExamples.ApprovedFileIsDifferent."));
            });
    }
}