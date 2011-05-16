using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Shouldly.DifferenceHighlighting;

namespace Shouldly
{
    public class TestEnvironment
    {
        public bool DeterminedOriginatingFrame { get; set; }
        public string ShouldMethod { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
    }

    public class ShouldlyMessage
    {
        private readonly object expected;
        private readonly object actual;
        private readonly bool hasActual;

        public ShouldlyMessage(object expected)
        {
            this.expected = expected;
        }

        public ShouldlyMessage(object expected, object actual)
        {
            this.actual = actual;
            this.expected = expected;
            hasActual = true;
        }

        public override string ToString()
        {
            return hasActual ? 
                GenerateShouldMessage(actual, expected) : 
                GenerateShouldMessage(expected);
        }

        private static string GenerateShouldMessage(object actual, object expected)
        {
            var environment = GetStackFrameForOriginatingTestMethod();
            var codePart = "The provided expression";

            if (environment.DeterminedOriginatingFrame)
            {
                var possibleCodeLines = File.ReadAllLines(environment.FileName)
                                            .Skip(environment.LineNumber).ToArray();
                var codeLines = possibleCodeLines.DelimitWith("\n");

                var shouldMethodIndex = codeLines.IndexOf(environment.ShouldMethod);
                codePart = shouldMethodIndex > -1 ?
                    codeLines.Substring(0, shouldMethodIndex - 1).Trim() :
                    possibleCodeLines[0];
            }

            return CreateActualVsExpectedMessage(actual, expected, environment, codePart);
        }

        private static string CreateActualVsExpectedMessage(object actual, object expected, TestEnvironment environment, string codePart) 
        {
            string message = string.Format(@"{0}
        {1}
    {2}
        but was
    {3}",
                codePart, environment.ShouldMethod.PascalToSpaced(), expected.Inspect(), actual.Inspect());

            if (actual.CanGenerateDifferencesBetween(expected)) {
                message += string.Format(@"
        difference
    {0}",
                actual.HighlightDifferencesBetween(expected));
            }
            return message;
        }

        private static string GenerateShouldMessage(object expected)
        {
            var environment = GetStackFrameForOriginatingTestMethod();
            var codePart = "The provided expression";

            if (environment.DeterminedOriginatingFrame)
            {
                var codeLines = string.Join("\n",
                                            File.ReadAllLines(environment.FileName)
                                                .Skip(environment.LineNumber).ToArray());

                codePart = codeLines.Substring(0, codeLines.IndexOf(environment.ShouldMethod) - 1).Trim();
            }

            var isNegatedAssertion = environment.ShouldMethod.Contains("Not");

            const string elementSatifyingTheConditionString = "an element satisfying the condition";
            return string.Format(
                @"{0}
        {1} {2}
    {3}
        but does {4}",
                     codePart, environment.ShouldMethod.PascalToSpaced(), expected is BinaryExpression ? elementSatifyingTheConditionString : "", expected.Inspect(), isNegatedAssertion ? "" : "not");
        }

        private static TestEnvironment GetStackFrameForOriginatingTestMethod()
        {
            var stackTrace = new StackTrace(true);
            var i = 0;
            var shouldlyFrame = stackTrace.GetFrame(i);
            if (shouldlyFrame == null) throw new Exception("Unable to find test method");

            while (!shouldlyFrame.GetMethod().DeclaringType.GetCustomAttributes(typeof(ShouldlyMethodsAttribute), true).Any())
            {
                shouldlyFrame = stackTrace.GetFrame(++i);
            }
            var originatingFrame = stackTrace.GetFrame(i+1);

            return new TestEnvironment
                       {
                           DeterminedOriginatingFrame = originatingFrame.GetFileName() != null,
                           ShouldMethod = shouldlyFrame.GetMethod().Name,
                           FileName = originatingFrame.GetFileName(),
                           LineNumber = originatingFrame.GetFileLineNumber() - 1
                       };
        }
    }
}