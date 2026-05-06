public class AssertionScopeExamples
{
    ITestOutputHelper _testOutputHelper;

    public AssertionScopeExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void BasicUsage()
    {
        DocExampleWriter.Document(
            () =>
            {
                var homer = new Person { Name = "Homer", Salary = 30000 };
                using var scope = new AssertionScope();
                homer.Name.ShouldBe("Mr.Burns");
                homer.Salary.ShouldBeGreaterThan(1000000);
            },
            _testOutputHelper);
    }
}
