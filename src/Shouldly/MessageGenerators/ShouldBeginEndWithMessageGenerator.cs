namespace Shouldly.MessageGenerators
{
    internal class ShouldBeginEndWithMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod is
                "ShouldBeginWith" or
                "ShouldNotBeginWith" or
                "ShouldEndWith" or
                "ShouldNotEndWith";
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            var actual = codePart == actualValue
                ? context.IsNegatedAssertion ? " did" : " did not" : $@" was {actualValue}";
            return $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}
{expected}
    but{actual}";
        }
    }
}
