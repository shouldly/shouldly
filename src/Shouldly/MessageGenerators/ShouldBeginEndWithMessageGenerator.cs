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
            var expectedValue = context.Expected.ToStringAwesomely();
            var actualValue = context.Actual.ToStringAwesomely();
            string actual;
            if (codePart == actualValue)
            {
                if(context.IsNegatedAssertion) actual = " did";
                else actual = " did not";
            }
            else actual = $@" was
{actualValue}";

            var message =
                $@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}
{expectedValue}
    but{actual}";

            return message;
        }
    }
}