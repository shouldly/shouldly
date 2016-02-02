using Shouldly;
using Xunit;

namespace ShouldlyConvention.Tests
{
    public class ApprovePublicAPI
    {
        [Fact]
        public void ShouldlyApi()
        {
            var publicApi = PublicApiGenerator.PublicApiGenerator.GetPublicApi(typeof (Should).Assembly);

            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        } 
    }
}