using System.Diagnostics;
using Xunit;

namespace Shouldly.Tests.ConventionTests
{
    public class ApprovePublicApi
    {
        [Fact]
        public void ShouldlyApi()
        {
            Trace.WriteLine("Joe - Hello World!");
            
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }  
    }
}
