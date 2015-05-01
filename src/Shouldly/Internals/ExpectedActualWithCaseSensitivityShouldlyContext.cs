namespace Shouldly
{
    internal class ExpectedActualWithCaseSensitivityShouldlyContext : ShouldlyAssertionContext
    {
        internal ExpectedActualWithCaseSensitivityShouldlyContext(object expected, object actual, Case caseSensitivity) : base(expected, actual)
        {
            CaseSensitivity = caseSensitivity;
        }

        public Case CaseSensitivity { get; private set; }
    }
}