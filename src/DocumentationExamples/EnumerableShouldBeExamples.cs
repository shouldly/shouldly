public class EnumerableShouldBeExamples
{
    ITestOutputHelper _testOutputHelper;

    public EnumerableShouldBeExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldBe()
    {
        DocExampleWriter.Document(
            () =>
            {
                var apu = new Person { Name = "Apu" };
                var homer = new Person { Name = "Homer" };
                var skinner = new Person { Name = "Skinner" };
                var barney = new Person { Name = "Barney" };
                var theBeSharps = new List<Person> { homer, skinner, barney };

                theBeSharps.ShouldBe(new[] { apu, homer, skinner, barney });
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeDictionary()
    {
        DocExampleWriter.Document(
            () =>
            {
                var duffStock = new Dictionary<string, int> { ["Duff"] = 12, ["Duff Lite"] = 6, ["Duff Dry"] = 3 };

                duffStock.ShouldBe(new Dictionary<string, int> { ["Duff Dry"] = 3, ["Duff"] = 24, ["Raspberry Duff"] = 1 });
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldBeSet()
    {
        DocExampleWriter.Document(
            () =>
            {
                var bowlingTeam = new HashSet<string> { "Homer", "Moe", "Otto" };

                bowlingTeam.ShouldBe(new HashSet<string> { "Apu", "Homer", "Moe" });
            },
            _testOutputHelper);
    }
}
