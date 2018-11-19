using System.Diagnostics;
using Shouldly.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        private readonly ITestOutputHelper _output;

        public ApprovePublicApi(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void ShouldlyApi()
        {
            _output.WriteLine("Joe - Hello World!");
            var res = new DoNotLaunchWhenEnvVariableIsPresent("AppVeyor");
            var result = res.ShouldNotLaunch();
            _output.WriteLine("Joe ShouldNotLaunch: " + result);
            
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }  
    }
}
