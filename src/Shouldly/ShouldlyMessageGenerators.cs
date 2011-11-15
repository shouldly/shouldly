using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Shouldly
{
    internal class ShouldBeEmptyMessageGenerator : ShouldlyMessageGenerator
    {
        private static readonly Regex Validator = new Regex("Should(Not)?BeEmpty", RegexOptions.Compiled);

        public override bool CanProcess(TestEnvironment environment)
        {
            return Validator.IsMatch(environment.ShouldMethod);
        }

        public override string GenerateErrorMessage(TestEnvironment environment, object actual)
        {
            const string format = @"
    {0}
            {1}
        but was {2}";

            var codePart = GetCodePart(environment);
            var actualValue = actual.Inspect();

            var isNegatedAssertion = environment.ShouldMethod.Contains("Not");
            if (isNegatedAssertion)
                return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), actual == null ? "null" : "");

            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), actualValue);
        }
    }

    internal class DefaultMessageGenerator : ShouldlyMessageGenerator
    {
        public override bool CanProcess(TestEnvironment environment)
        {
            return true;
        }

        public override string GenerateErrorMessage(TestEnvironment environment, object expected)
        {
            var format = @"
    {0}
        {1} {2}
    {3}
        but does {4}";

            var codePart = GetCodePart(environment);
            var isNegatedAssertion = environment.ShouldMethod.Contains("Not");

            const string elementSatifyingTheConditionString = "an element satisfying the condition";
            return String.Format(format, codePart, environment.ShouldMethod.PascalToSpaced(), expected is BinaryExpression ? elementSatifyingTheConditionString : "", expected.Inspect(), isNegatedAssertion ? "" : "not");
        }
    }

    internal abstract class ShouldlyMessageGenerator
    {
        public abstract bool CanProcess(TestEnvironment environment);
        public abstract string GenerateErrorMessage(TestEnvironment environment, object expected);


        protected static string GetCodePart(TestEnvironment environment)
        {
            var codePart = "The provided expression";

            if (environment.DeterminedOriginatingFrame)
            {
                var codeLines = String.Join("\n",
                                            File.ReadAllLines(environment.FileName)
                                                .Skip(environment.LineNumber).ToArray());

                codePart = codeLines.Substring(0, codeLines.IndexOf(environment.ShouldMethod) - 1).Trim();
            }
            return codePart;
        }
    }
}