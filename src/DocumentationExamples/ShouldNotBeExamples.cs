public class ShouldNotBeExamples
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ShouldNotBeExamples(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Objects()
    {
        DocExampleWriter.Document(
            () =>
            {
                var theSimpsonsCat = new Cat { Name = "Santas little helper" };
                theSimpsonsCat.Name.ShouldNotBe("Santas little helper");
            },
            _testOutputHelper);
    }

    [Fact]
    public void NumericInt()
    {
        DocExampleWriter.Document(
            () =>
            {
                const int one = 1;
                one.ShouldNotBe(1);
            },
            _testOutputHelper);
    }

    [Fact]
    public void NumericLong()
    {
        DocExampleWriter.Document(
            () =>
            {
                const long aLong = 1L;
                aLong.ShouldNotBe(1);
            },
            _testOutputHelper);
    }

    [Fact]
    public void DateTime()
    {
        Thread.CurrentThread.CurrentCulture = new("en-AU");
        DocExampleWriter.Document(
            () =>
            {
                var date = new DateTime(2000, 6, 1);
                date.ShouldNotBe(new(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5));
            },
            _testOutputHelper);
    }

    [Fact]
    public void TimeSpanExample()
    {
        DocExampleWriter.Document(
            () =>
            {
                var timeSpan = TimeSpan.FromHours(1);
                timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));
            },
            _testOutputHelper);
    }
}