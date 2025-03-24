namespace Shouldly.Tests.ShouldBeEmpty;

public class GuidScenario
{
    [Fact]
    public void ShouldFail()
    {
        Guid myGuid = new Guid(1,2,3,4,5,6,7,8,9,10,11);

        Verify.ShouldFail(() =>
                myGuid.ShouldBeEmpty("Some additional context"),

            errorWithSource:
            """
            myGuid
                should be empty
            00000000-0000-0000-0000-000000000000
                but was
            00000001-0002-0003-0405-060708090a0b

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            00000001-0002-0003-0405-060708090a0b
                should be empty
            00000000-0000-0000-0000-000000000000
                but was not
            
            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        Guid.Empty.ShouldBeEmpty();
    }
}