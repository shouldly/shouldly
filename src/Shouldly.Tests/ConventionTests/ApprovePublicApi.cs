#if(NETCOREAPP)
using PublicApiGenerator;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [IgnoreOnAppVeyorLinuxFact]
        public void ShouldlyApi()
        {
            var options = new ApiGeneratorOptions
            {
                IncludeAssemblyAttributes = false
            };
            var publicApi = typeof(Should).Assembly.GeneratePublicApi(options);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
    }
}
#endif