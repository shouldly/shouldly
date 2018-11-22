using Xunit;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [Fact]
        public void ShouldlyApi()
        {
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly, null,false);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
    }
}