#if(NETCOREAPP)
using System;
using System.Linq;
using System.Reflection;
using PublicApiGenerator;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [IgnoreOnAppVeyorLinuxFact]
        public void ShouldlyApi()
        {
            var assembly = typeof(Should).Assembly;
            var options = new ApiGeneratorOptions
            {
                IncludeAssemblyAttributes = false,
                IncludeTypes = assembly.GetTypes()
                    .Where(x => x.GetCustomAttribute(typeof(ObsoleteAttribute)) == null)
                    .ToArray()
            };
            var publicApi = assembly.GeneratePublicApi(options);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }
    }
}
#endif