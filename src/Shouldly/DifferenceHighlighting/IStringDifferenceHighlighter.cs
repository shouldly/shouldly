namespace Shouldly.DifferenceHighlighting
{
    interface IStringDifferenceHighlighter
    {
        string? HighlightDifferences(string? expected, string? actual);
    }
}
