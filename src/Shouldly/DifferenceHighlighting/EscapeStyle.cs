namespace Shouldly;

/// <summary>
/// Controls how control characters are displayed in string difference output
/// </summary>
public enum EscapeStyle
{
    /// <summary>
    /// Use C-style escape sequences: \r, \n, \t, etc.
    /// </summary>
    CStyle,

    /// <summary>
    /// Use Unicode control pictures: ␍, ␊, ␉, etc. (U+2400 block).
    /// Each control character renders as a single visible symbol,
    /// avoiding ambiguity with literal backslash text.
    /// </summary>
    ControlPictures,

    /// <summary>
    /// Use ASCII-safe descriptive names: &lt;CR&gt;, &lt;LF&gt;, &lt;TAB&gt;, etc.
    /// Avoids ambiguity with literal backslash text without requiring Unicode support.
    /// </summary>
    Descriptive
}
