using System.Threading.Tasks;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldNotThrowExamples
    {
        readonly ITestOutputHelper _testOutputHelper;

        public ShouldNotThrowExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldNotThrowAction()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person() { Name = "Homer", Salary = 30000 };
                var denominator = 0;
                Should.NotThrow(() =>
                {
                    var y = homer.Salary / denominator;
                });
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotThrowFunc()
        {
            DocExampleWriter.Document(() =>
            {
                string? name = null;
                Should.NotThrow(() => new Person(name!));
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotThrowFuncOfTask()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person() { Name = "Homer", Salary = 30000 };
                var denominator = 0;
                Should.NotThrow(() =>
                {
                    var task = Task.Factory.StartNew(() => { var y = homer.Salary / denominator; });
                    return task;
                });
            }, _testOutputHelper);
        }
    }
}
