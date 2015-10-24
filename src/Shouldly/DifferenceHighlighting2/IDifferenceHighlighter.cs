namespace Shouldly.DifferenceHighlighting2
{
    interface IStringDifferenceHighlighter
    {
        string HighlightDifferences(string expected, string actual);
    }
}
