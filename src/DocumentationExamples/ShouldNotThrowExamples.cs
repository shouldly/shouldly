using System;
using System.Threading.Tasks;
using Shouldly;
using Simpsons;
using Xunit;
using Xunit.Abstractions;

namespace DocumentationExamples
{
    public class ShouldNotThrowExamples
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ShouldNotThrowExamples(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldNotThrowAction()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person {Name = "Homer", Salary = 30000};
                var denominator = 0;
                Should.NotThrow(() =>
                {
                    var y = homer.Salary / denominator;
                });
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotThrowActionExtension()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person {Name = "Homer", Salary = 30000};
                var denominator = 0;
                Action action = () =>
                {
                    var y = homer.Salary / denominator;
                };
                action.ShouldNotThrow();
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
        public void ShouldNotThrowFuncExtension()
        {
            DocExampleWriter.Document(() =>
            {
                string? name = null;
                Func<Person> func = () => new Person(name!);
                func.ShouldNotThrow();
            }, _testOutputHelper);
        }

        [Fact]
        public void ShouldNotThrowFuncOfTask()
        {
            DocExampleWriter.Document(() =>
            {
                var homer = new Person {Name = "Homer", Salary = 30000};
                var denominator = 0;
                Should.NotThrow(() =>
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