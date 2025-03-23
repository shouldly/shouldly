namespace Shouldly;

/// <summary>
/// Specifies options for string comparison operations
/// </summary>
[Flags]
public enum StringCompareShould
{
    /// <summary>
    /// Ignore case differences when comparing strings
    /// </summary>
    IgnoreCase = 1,
    
    /// <summary>
    /// Ignore line ending differences when comparing strings
    /// </summary>
    IgnoreLineEndings = 2
}