public class EnumerableShouldNotContainExamples
{
    ITestOutputHelper _testOutputHelper;

    public EnumerableShouldNotContainExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldNotContain()
    {
        DocExampleWriter.Document(
            () =>
            {
                var homerSimpson = new Person { Name = "Homer" };
                var homerGlumplich = new Person { Name = "Homer" };
                var lenny = new Person { Name = "Lenny" };
                var carl = new Person { Name = "carl" };
                var clubOfNoHomers = new List<Person> { homerSimpson, homerGlumplich, lenny, carl };

                clubOfNoHomers.ShouldNotContain(homerSimpson);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldNotContain_Predicate()
    {
        DocExampleWriter.Document(
            () =>
            {
                var mrBurns = new Person { Name = "Mr.Burns", Salary = 3000000 };
                var kentBrockman = new Person { Name = "Homer", Salary = 3000000 };
                var homer = new Person { Name = "Homer", Salary = 30000 };
                var millionaires = new List<Person> { mrBurns, kentBrockman, homer };

                millionaires.ShouldNotContain(m => m.Salary < 1000000);
            },
            _testOutputHelper);
    }
}