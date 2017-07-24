using System.Text;

namespace Shouldly.MessageGenerators
{
    internal class ShouldBeEquivalentToMessageGenerator : ShouldlyMessageGenerator
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
#if StackTrace
            var result = new StringBuilder(ShouldlyConfiguration.IsSourceDisabledInErrors() ? DefaultRootValue : context.CodePart);
#else
            var result = new StringBuilder(DefaultRootValue);
#endif
            if (context.Path != null)
            {
                for (var i = 0; i < context.Path.Count; i++)
                {
                    result.Append(new string(' ', i * IndentSize));
                    result.AppendLine(context.Path[i]);
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}
