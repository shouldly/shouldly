using PublicApiGenerator;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [IgnoreOnAppVeyorLinuxFact]
        public void ShouldlyApi()
        {
            var publicApi = typeof(Should).Assembly.GeneratePublicApi(new ApiGeneratorOptions
            {
                IncludeAssemblyAttributes = false
            });

            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
    }
}