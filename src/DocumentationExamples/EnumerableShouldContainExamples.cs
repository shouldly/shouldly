public class EnumerableShouldContainExamples
{
    ITestOutputHelper _testOutputHelper;

    public EnumerableShouldContainExamples(ITestOutputHelper testOutputHelper) =>
        _testOutputHelper = testOutputHelper;

    [Fact]
    public void ShouldContain()
    {
        DocExampleWriter.Document(
            () =>
            {
                var mrBurns = new Person { Name = "Mr.Burns", Salary = 3000000 };
                var kentBrockman = new Person { Name = "Kent Brockman", Salary = 3000000 };
                var homer = new Person { Name = "Homer", Salary = 30000 };
                var millionaires = new List<Person> { kentBrockman, homer };

                millionaires.ShouldContain(mrBurns);
            },
            _testOutputHelper);
    }

    [Fact]
    public void ShouldContain_Predicate()
    {
        DocExampleWriter.Document(
            () =>
            {
                var homer = new Person { Name = "Homer", Salary = 30000 };
                var moe = new Person { Name = "Moe", Salary = 20000 };
                var barney = new Person { Name = "Barney", Salary = 0 };
                var millionaires = new List<Person> { homer, moe, barney };

                // Check if at least one element in the IEnumerable satisfies the predicate
                millionaires.ShouldContain(m => m.Salary > 1000000);
            },
            _testOutputHelper);
    }
}