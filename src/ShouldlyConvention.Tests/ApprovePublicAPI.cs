using ApiApprover;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Shouldly;

namespace ShouldlyConvention.Tests
{
    public class ApprovePublicAPI
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void ShouldlyApi()
        {
            PublicApiApprover.ApprovePublicApi(typeof (Should).Assembly.Location);
        } 
    }
}