using Shouldly;
using Shouldly.Tests.ConventionTests;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldMatchApprovedExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldMatchApprovedExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [IgnoreOnAppVeyorLinuxFact]
        public void ApprovedFileDoesNotExist()
        {
            DocExampleWriter.Document(() =>
            {
                var simpsonsQuote = "Hi Super Nintendo Chalmers";
                simpsonsQuote.ShouldMatchApproved(c => c.NoDiff());
            }, _testOutputHelper, c =>
            {
                c.WithScrubber(s => s.Replace("DocExampleWriter.Document.approved.txt", "ShouldMatchApprovedExamples.ApprovedFileDoesNotExist.approved.txt"));
                c.WithScrubber(s => s.Replace("DocExampleWriter.Document.received.txt", "ShouldMatchApprovedExamples.ApprovedFileDoesNotExist.received.txt"));
                c.WithScrubber(s => s.Replace("c => c.NoDiff()", string.Empty));
            });
        }

        [IgnoreOnAppVeyorLinuxFact]
        public void ApprovedFileIsDifferent()
        {
            DocExampleWriter.Document(() =>
            {
                var simpsonsQuote = "Me fail english? That's unpossible";
                simpsonsQuote.ShouldMatchApproved(c => c.NoDiff().WithDiscriminator("Different"));
            }, _testOutputHelper, c =>
            {
                // Scrubbing the generated docs is easier than altering the infrastructure to support this scenario
                c.WithScrubber(s => s.Replace("c => c.NoDiff().WithDiscriminator(\"Different\")", string.Empty));
                c.WithScrubber(s => s.Replace("DocExampleWriter.Document.Different.approved.txt", "ShouldMatchApprovedExamples.ApprovedFileIsDifferent.approved.txt"));
                c.WithScrubber(s => s.Replace("DocExampleWriter.Document.Different.received.txt", "ShouldMatchApprovedExamples.ApprovedFileIsDifferent.received.txt"));
            });
        }
    }
}