namespace Shouldly.DifferenceHighlighting
{
    /// <summary>
    /// Interface for classes which can highlight differences in things
    /// </summary>
    internal interface IHighlighter
    {
        bool CanProcess(IShouldlyAssertionContext context);
        string HighlightDifferences(IShouldlyAssertionContext context);
    }
}