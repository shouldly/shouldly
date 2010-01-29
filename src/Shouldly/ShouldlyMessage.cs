using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Shouldly
{
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
            var frame = GetStackFrameForOriginatingTestMethod();

            var shouldMethod = frame.GetMethod().Name;
            var lineNumber = frame.GetFileLineNumber() - 1;
            var fileName = frame.GetFileName();

            var codeLines = string.Join("\n",
                                        File.ReadAllLines(fileName)
                                            .Skip(lineNumber).ToArray());

            var codePart = codeLines.Substring(0, codeLines.IndexOf(shouldMethod) - 1).Trim();

            return string.Format(
                @"{0}
        {1}
    {2}
        but was
    {3}",
                codePart, shouldMethod.PascalToSpaced(), expected.Inspect(), actual.Inspect());
        }

        private static string GenerateShouldMessage(object expected)
        {
            var frame = GetStackFrameForOriginatingTestMethod();

            var shouldMethod = frame.GetMethod().Name;
            var lineNumber = frame.GetFileLineNumber() - 1;
            var fileName = frame.GetFileName();

            var codeLines = string.Join("\n",
                                        File.ReadAllLines(fileName)
                                            .Skip(lineNumber).ToArray());

            var codePart = codeLines.Substring(0, codeLines.IndexOf(shouldMethod) - 1).Trim();

            return string.Format(
                @"{0}
        {1}
    {2}
        but does not",
                codePart, shouldMethod.PascalToSpaced(), expected.Inspect());
        }

        private static StackFrame GetStackFrameForOriginatingTestMethod()
        {
            var stackTrace = new StackTrace(true);
            var i = 0;
            var frame = stackTrace.GetFrame(i);
            while (!frame.GetMethod().GetCustomAttributes(typeof(TestAttribute), true).Any())
            {
                frame = stackTrace.GetFrame(++i);
            }
            return frame;
        }
    }
}