namespace Shouldly.MessageGenerators;

class ShouldNotThrowMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context is ShouldThrowAssertionContext &&
        context.IsNegatedAssertion;

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var throwContext = (ShouldThrowAssertionContext)context;
        var isExtensionMethod = context.ShouldMethod == "ShouldNotThrow";
        var codePart = context.CodePart;

        string subject;
        if (codePart == "null")
        {
            subject = throwContext.IsAsync ? "Task" : "delegate";
        }
        else if (isExtensionMethod)
        {
            subject = throwContext.IsAsync ? $"Task `{codePart}`" : $"`{codePart}()`";
        }
        else
        {
            subject = $"`{codePart}`";
        }

        // NotThrow<TException> carries the disallowed type in Expected and the thrown
        // exception in Actual; the non-generic NotThrow stores the thrown type in Expected.
        if (context.Expected is Type expectedType && context.Actual is Exception thrown)
        {
            var thrownType = thrown.GetType();
            if (thrownType == expectedType)
            {
                return
                    $"""
                     {subject}
                         should not throw
                     {expectedType.ToStringAwesomely()}
                         but did, with message
                     "{thrown.Message}"
                     """;
            }

            return
                $"""
                 {subject}
                     should not throw
                 {expectedType.ToStringAwesomely()}
                     but threw
                 {thrownType.ToStringAwesomely()}
                     with message
                 "{thrown.Message}"
                 """;
        }

        return
            $"""
             {subject}
                 should not throw but threw
             {context.Expected.ToStringAwesomely()}
                 with message
             "{throwContext.ExceptionMessage}"
             """;
    }
}
