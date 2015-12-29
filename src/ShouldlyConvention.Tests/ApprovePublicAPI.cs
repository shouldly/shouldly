#if net40
using ApiApprover;
using ApprovalTests.Reporters;
using Shouldly;
using Xunit;

namespace ShouldlyConvention.Tests
{
    public class ApprovePublicAPI
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void ShouldlyApi()
        {
            PublicApiApprover.ApprovePublicApi(typeof (Should).Assembly.Location);
        } 
    }
}
#endif