using System.Text.RegularExpressions;

namespace Shouldly.MessageGenerators
{
    internal class ShouldAllBeMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("ShouldAllBe", RegexOptions.Compiled);

        public override bool CanProcess(ShouldlyAssertionContext context)
        {
            return Validator.IsMatch(context.ShouldMethod) && !context.HasActual;
        }

        public override string GenerateErrorMessage(ShouldlyAssertionContext context)
        {
            // todo: fixme
            return "new[] { 1, 2, 3 } should satisfy the condition (x < 2) but [2,3] does not";
        }
    }
}