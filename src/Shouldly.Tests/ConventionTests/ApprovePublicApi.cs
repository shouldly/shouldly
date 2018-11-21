using Xunit;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        #if IsWindows
        [Fact]
        public void ShouldlyApiWindows()
        {
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
        #else
        [Fact]
        public void ShouldlyApiMac()
        {
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
        #endif
    }
}
