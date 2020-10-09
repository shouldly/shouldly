namespace Shouldly.MessageGenerators
{
    internal class ShouldBeginEndWithMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.ShouldMethod == "ShouldBeginWith" ||
                   context.ShouldMethod == "ShouldNotBeginWith" ||
                   context.ShouldMethod == "ShouldEndWith" ||
                   context.ShouldMethod == "ShouldNotEndWith";
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var expected = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            string actual;
            if (codePart == actualValue)
            {
                if (context.IsNegatedAssertion)
                {
                    actual = " did";
                }
                else
                {
                    actual = " did not";
                }
            }
            else
            {
                actual = $@" was
{actualValue}";
            }

            return $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}
{expected}
    but{actual}";
        }
    }
}