namespace Shouldly;

/// <summary>
/// Controls the character set used for string difference markers in assertion messages
/// </summary>
public enum DiffStyle
{
    /// <summary>
    /// Use Unicode markers (▼/▲) for difference highlighting
    /// </summary>
    Unicode,

    /// <summary>
    /// Use ASCII markers (v/^) for difference highlighting
    /// </summary>
    Ascii
}
