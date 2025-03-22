#if(NETCOREAPP)
using System.Reflection;
using PublicApiGenerator;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [Fact]
        public void ShouldlyApi()
        {
            var publicApi = GenerateApi(typeof(Should).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }

        [Fact]
        public void DiffEngineApi()
        {
            var publicApi = GenerateApi(typeof(DiffEngine).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }

        private static string GenerateApi(Assembly assembly)
        {
            var options = new ApiGeneratorOptions
            {
                IncludeAssemblyAttributes = false,
                IncludeTypes = assembly.GetTypes()
                    .Where(x => !x.IsDefined(typeof(ObsoleteAttribute)))
                    .ToArray()
            };
            return assembly.GeneratePublicApi(options);
        }
    }
}
#endif