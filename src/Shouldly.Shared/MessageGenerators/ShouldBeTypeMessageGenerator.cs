using System.Text.RegularExpressions;
using Shouldly.MessageGenerators;

namespace Shouldly
{
    internal class ShouldBeTypeMessageGenerator : ShouldlyMessageGenerator
    {
        static readonly Regex Validator = new Regex("ShouldBe(Not)?(OfType|AssignableTo)");

        public override bool CanProcess(IShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod);
        }

        public override string GenerateErrorMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var actualType = context.Actual?.GetType().FullName;

            var actualString = codePart == actualType || codePart == "null" ? " not" : $@"
{actualType ?? "null"}";

            return
$@"{codePart}
    {context.ShouldMethod.PascalToSpaced()}
{context.Expected.ToStringAwesomely()}
    but was{actualString}";
        }
    }
}