namespace Shouldly.DifferenceHighlighting
{
    /// <summary>
    /// Interface for classes which can highlight differences in things
    /// </summary>
    internal interface IHighlighter
    {
        bool CanProcess(ITestEnvironment testEnvironment);
        string HighlightDifferences(ITestEnvironment testEnvironment);
    }
}