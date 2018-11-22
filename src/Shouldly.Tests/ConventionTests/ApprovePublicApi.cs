namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        #if NETCOREAPP
        [IgnoreOnAppVeyorLinuxFact]
        public void ShouldlyApi()
        {
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
        #endif
    }
}