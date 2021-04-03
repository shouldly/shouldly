namespace Shouldly.DifferenceHighlighting
{
    internal interface IStringDifferenceHighlighter
    {
        string? HighlightDifferences(string? expected, string? actual);
    }
}
