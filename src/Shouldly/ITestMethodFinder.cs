namespace Shouldly;

/// <summary>
/// Interface for finding test methods in a stack trace
/// </summary>
public interface ITestMethodFinder
{
    /// <summary>
    /// Gets test method information from the stack trace
    /// </summary>
    /// <param name="stackTrace">The stack trace to analyze</param>
    /// <param name="startAt">The frame index to start searching from</param>
    /// <returns>Information about the found test method</returns>
    TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0);
}