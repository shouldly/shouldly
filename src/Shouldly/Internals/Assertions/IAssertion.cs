namespace Shouldly.Internals.Assertions;

interface IAssertion
{
    bool IsSatisfied();
    string? GenerateMessage(string? customMessage);
}