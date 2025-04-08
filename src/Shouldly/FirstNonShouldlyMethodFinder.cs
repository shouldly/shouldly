using Shouldly.Internals;

namespace Shouldly;

/// <summary>
/// Finds the first method in the stack trace that is not from the Shouldly library
/// </summary>
public class FirstNonShouldlyMethodFinder : ITestMethodFinder
{
    private static readonly Regex AnonMethod = new(@"<(\w|_)+>b_.+");

    /// <summary>
    /// Increasing the offset will move past the first non-shouldly method
    /// Anonymous methods are not counted in the offset.
    /// This is useful when you have created a reusable method which is calling ShouldMatchApproved
    /// </summary>
    public int Offset { get; set; }

    /// <summary>
    /// Gets test method information from the stack trace
    /// </summary>
    /// <param name="stackTrace">The stack trace to analyze</param>
    /// <param name="startAt">The frame index to start searching from</param>
    /// <returns>Information about the found test method</returns>
    public TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0)
    {
        foreach (var (i, frame) in stackTrace.GetFrames().AsIndexed().Skip(startAt))
        {
            if (frame.GetMethod() is { } method && !method.IsShouldlyMethod() && !IsCompilerGenerated(method))
            {
                var callingFrame = stackTrace.GetFrame(i + Offset)
                                   ?? throw new InvalidOperationException("There is no stack frame at the specified offset from the first non-Shouldly stack frame.");

                return new(callingFrame);
            }
        }

        throw new InvalidOperationException("Cannot find a non-Shouldly method in the stack trace.");
    }

    private static bool IsCompilerGenerated(MethodBase method) =>
        method.IsDefined(typeof(CompilerGeneratedAttribute), inherit: true) ||
        AnonMethod.IsMatch(method.Name);
}