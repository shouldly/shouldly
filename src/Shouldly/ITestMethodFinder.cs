namespace Shouldly;

public interface ITestMethodFinder
{
    TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0);
}