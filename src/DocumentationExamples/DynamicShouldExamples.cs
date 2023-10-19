public class DynamicShouldExamples
{
    ITestOutputHelper _testOutputHelper;

    public DynamicShouldExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void HaveProperty()
    {
        DocExampleWriter.Document(
            () =>
            {
                dynamic theFuture = new ExpandoObject();
                DynamicShould.HaveProperty(theFuture, "RobotTeachers");
            },
            _testOutputHelper);
    }
}