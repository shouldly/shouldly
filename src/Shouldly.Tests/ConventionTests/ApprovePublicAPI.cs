using System;
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

            string env = "AppVeyor";
            
            var res1 = Environment.GetEnvironmentVariable(env);
            _output.WriteLine("res1 = " + res1);
            
            var res2 =  Environment.GetEnvironmentVariable(env, EnvironmentVariableTarget.User);
            _output.WriteLine("res2 = " + res2);
            
            var res3 = Environment.GetEnvironmentVariable(env, EnvironmentVariableTarget.Machine);
            _output.WriteLine("res3 = " + res3);
            
            string env2 = "APPVEYOR";
            
            var res4 = Environment.GetEnvironmentVariable(env2);
            _output.WriteLine("res4 = " + res4);
            
            var res5 =  Environment.GetEnvironmentVariable(env2, EnvironmentVariableTarget.User);
            _output.WriteLine("res5 = " + res5);
            
            var res6 = Environment.GetEnvironmentVariable(env2, EnvironmentVariableTarget.Machine);
            _output.WriteLine("res6 = " + res6);
            
            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(Should).Assembly);
            publicApi.ShouldMatchApproved(b => b.WithFileExtension("cs"));
        }  
    }
}
