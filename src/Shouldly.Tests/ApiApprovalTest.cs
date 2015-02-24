using ApiApprover;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ExampleApiApprovalTest
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void approve_public_api()
        {
            PublicApiApprover.ApprovePublicApi(typeof(Should).Assembly.Location);
        }
    }
}