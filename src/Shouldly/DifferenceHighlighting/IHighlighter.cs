namespace Shouldly.DifferenceHighlighting
{
    /// <summary>
    /// Interface for classes which can highlight differences in things
    /// </summary>
    internal interface IHighlighter
    {
        bool CanProcess<T1, T2>(T1 expected, T2 actual);
        string HighlightDifferences<T1, T2>(T1 expected, T2 actual);
    }
}