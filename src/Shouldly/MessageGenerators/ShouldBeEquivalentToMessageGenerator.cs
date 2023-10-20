using System.Text;

namespace Shouldly.MessageGenerators;

class ShouldBeEquivalentToMessageGenerator : ShouldlyMessageGenerator
{
    private const string DefaultRootValue = "<root>";
    private const int IndentSize = 4;

    public override bool CanProcess(IShouldlyAssertionContext context)
    {
        return context.ShouldMethod == "ShouldBeEquivalentTo";
    }

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        return
            $@"Comparing object equivalence, at path:
{FormatPath(context)}

    Expected value to be
{context.Expected.ToStringAwesomely()}
    but was
{context.Actual.ToStringAwesomely()}";
    }

    private static string FormatPath(IShouldlyAssertionContext context)
    {
        var result = new StringBuilder(ShouldlyConfiguration.IsSourceDisabledInErrors() ? DefaultRootValue : context.CodePart);
        if (context.Path != null)
        {
            var i = 0;
            foreach (var part in context.Path)
            {
                result.Append(new string(' ', i++ * IndentSize));
                result.AppendLine(part);
            }
        }

        return result.ToString().TrimEnd();
    }
}