namespace Shouldly.Internals.Assertions
{
    internal interface IAssertion
    {
        bool IsSatisfied();
        string? GenerateMessage(string? customMessage);
    }
}
