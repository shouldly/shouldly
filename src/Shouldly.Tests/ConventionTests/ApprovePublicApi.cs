namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [IgnoreOnAppVeyorLinuxFact]
        public void ShouldlyApi()
        {
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly, null,false);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
    }
}