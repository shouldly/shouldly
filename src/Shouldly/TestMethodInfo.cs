namespace Shouldly;

/// <summary>
/// Contains information about the test method that invoked <c>ShouldMatchApproved</c>,
/// captured at compile time via <see cref="CallerMemberNameAttribute"/> and
/// <see cref="CallerFilePathAttribute"/>.
/// </summary>
public class TestMethodInfo
{
    /// <summary>
    /// Initializes a new instance from a test method name and the full path of the source
    /// file containing it
    /// </summary>
    /// <param name="methodName">The name of the test method</param>
    /// <param name="sourceFilePath">The full path of the source file containing the test method</param>
    public TestMethodInfo(string? methodName, string? sourceFilePath)
    {
        MethodName = methodName;
        SourceFileDirectory = string.IsNullOrEmpty(sourceFilePath) ? null : Path.GetDirectoryName(sourceFilePath);
        SourceFileName = string.IsNullOrEmpty(sourceFilePath) ? null : Path.GetFileNameWithoutExtension(sourceFilePath);
    }

    /// <summary>
    /// The directory containing the source file of the test method
    /// </summary>
    public string? SourceFileDirectory { get; }

    /// <summary>
    /// The name of the test method
    /// </summary>
    public string? MethodName { get; }

    /// <summary>
    /// The name of the source file containing the test method, without its extension.
    /// Under the usual one-class-per-file convention this matches the test class name.
    /// </summary>
    public string? SourceFileName { get; }

    /// <summary>
    /// The name of the type declaring the test method. Since Shouldly 5 this is derived from
    /// the source file name rather than the runtime stack, so it matches the declaring type
    /// only under the one-class-per-file naming convention.
    /// </summary>
    [Obsolete("Derived from the source file name since Shouldly 5. Use SourceFileName instead.")]
    public string? DeclaringTypeName => SourceFileName;
}
