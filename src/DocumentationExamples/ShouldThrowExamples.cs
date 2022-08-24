using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldThrowExamples
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ShouldThrowExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldThrowAction()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person { Name = "Homer", Salary = 30000 };
                var denominator = 1;
                Should.Throw<DivideByZeroException>(() =>
                {
                    var y = homer.Salary / denominator;
                });
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldThrowActionExtension()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person { Name = "Homer", Salary = 30000 };
                var denominator = 1;
                Action action = () =>
                {
                    var y = homer.Salary / denominator;
                };
                action.ShouldThrow<DivideByZeroException>();
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldThrowFunc()
        {
            DocExampleWriter.Document(() => { Should.Throw<ArgumentNullException>(() => new Person("Homer")); }, _testOutputHelper);
        }

        [Fact]
        public void ShouldThrowFuncExtension()
        {
            DocExampleWriter.Document(() =>
            {
                Func<Person> func = () => new Person("Homer");
                func.ShouldThrow<ArgumentNullException>();
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldThrowFuncOfTask()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person { Name = "Homer", Salary = 30000 };
                var denominator = 1;
                Should.Throw<DivideByZeroException>(() =>
                {
                    var task = Task.Factory.StartNew(
                        () =>
                        {
                            var y = homer.Salary / denominator;
                        });
                    return task;
                });
            }, _testOutputHelper);
        }
    }
}