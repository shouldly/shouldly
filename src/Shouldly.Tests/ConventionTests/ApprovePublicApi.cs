using PublicApiGenerator;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [IgnoreOnAppVeyorLinuxFact]
        public void ShouldlyApi()
        {
            var options = new ApiGeneratorOptions()
            {
                IncludeTypes = null,
                IncludeAssemblyAttributes = false
            };
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly, options);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
    }
}