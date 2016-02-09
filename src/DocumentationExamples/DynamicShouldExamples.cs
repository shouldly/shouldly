using System.Dynamic;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class DynamicShouldExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public DynamicShouldExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void HaveProperty()
        {
            DocExampleWriter.Document(() =>
            {
                dynamic theFuture = new ExpandoObject();
                DynamicShould.HaveProperty(theFuture,"RobotTeachers");
            }, _testOutputHelper);
        }

    }
}
