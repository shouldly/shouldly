using ApiApprover;
using Shouldly;
using Xunit;

namespace ShouldlyConvention.Tests
{
    public class ApprovePublicAPI
    {
        [Fact]
        public void ShouldlyApi()
        {
            PublicApiApprover.ApprovePublicApi(typeof (Should).Assembly.Location);
        } 
    }
}