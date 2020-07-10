using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Shouldly.Internals
{
    internal class ActualCodeTextGetter : ICodeTextGetter
    {
        bool _determinedOriginatingFrame;
        string? _shouldMethod;

        public int ShouldlyFrameOffset { get; private set; }
        public string? FileName { get; private set; }
        public int LineNumber { get; private set; }

        public string? GetCodeText(object? actual, StackTrace? stackTrace)
        {
            if (ShouldlyConfiguration.IsSourceDisabledInErrors())
                return actual.ToStringAwesomely();
            ParseStackTrace(stackTrace);
            return GetCodePart();
        }

        void ParseStackTrace(StackTrace? stackTrace)
        {
            stackTrace ??= new StackTrace(fNeedFileInfo: true);

            var frames =
                from index in Enumerable.Range(0, stackTrace.FrameCount)
                let frame = stackTrace.GetFrame(index)!
                let method = frame.GetMethod()
                where method is object && !method.IsSystemDynamicMachinery()
                select new { index, frame, method };

            var shouldlyFrame = frames
                .SkipWhile(f => !f.method.IsShouldlyMethod())
                .TakeWhile(f => f.method.IsShouldlyMethod())
                .LastOrDefault()
                ?? throw new InvalidOperationException("The stack trace did not contain a Shouldly method.");

            var originatingFrame = frames
                .FirstOrDefault(f => f.index > shouldlyFrame.index)
                ?? throw new InvalidOperationException("The stack trace did not contain the caller of the Shouldly method.");

            ShouldlyFrameOffset = originatingFrame.index;

            var fileName = originatingFrame.frame.GetFileName();
            _determinedOriginatingFrame = fileName != null && File.Exists(fileName);
            _shouldMethod = shouldlyFrame.method.Name;
            FileName = fileName;
            LineNumber = originatingFrame.frame.GetFileLineNumber() - 1;
        }

        string GetCodePart()
        {
            var codePart = "Shouldly uses your source code to generate its great error messages, build your test project with full debug information to get better error messages" +
                           "\nThe provided expression";

            if (_determinedOriginatingFrame)
            {
                var codeLines = string.Join("\n", File.ReadAllLines(FileName).Skip(LineNumber).ToArray());

                var indexOf = codeLines.IndexOf(_shouldMethod!);
                if (indexOf > 0)
                    codePart = codeLines.Substring(0, indexOf - 1).Trim();

                // When the static method is used instead of the extension method,
                // the code part will be "Should".
                // Using EndsWith to cater for being inside a lambda
                if (codePart.EndsWith("Should"))
                {
                    codePart = GetCodePartFromParameter(indexOf, codeLines, codePart);
                }
                else
                {
                    codePart = codePart.RemoveVariableAssignment().RemoveBlock();
                }
            }
            return codePart;
        }

        string GetCodePartFromParameter(int indexOfMethod, string codeLines, string codePart)
        {
            var indexOfParameters =
                indexOfMethod +
                _shouldMethod!.Length;

            var parameterString = codeLines.Substring(indexOfParameters);
            // Remove generic parameter if need be
            parameterString = parameterString.StartsWith("<")
                ? parameterString.Substring(parameterString.IndexOf(">", StringComparison.Ordinal) + 2)
                : parameterString.Substring(1);

            var parentheses = new Dictionary<char, char>
            {
                {'{', '}'},
                {'(', ')'},
                {'[', ']'}
            };

            var parameterFinishedKeys = new[] { ',', ')' };

            var openParentheses = new List<char>();

            var found = false;
            var i = 0;
            while (!found && parameterString.Length > i)
            {
                var currentChar = parameterString[i];

                if (openParentheses.Count == 0 && parameterFinishedKeys.Contains(currentChar))
                {
                    found = true;
                    continue;
                }

                if (parentheses.ContainsKey(currentChar))
                {
                    openParentheses.Add(parentheses[currentChar]);
                }
                else if (openParentheses.Count > 0 && openParentheses.Last() == currentChar)
                {
                    openParentheses.RemoveAt(openParentheses.Count - 1);
                }

                i++;
            }

            if (found)
            {
                codePart = parameterString.Substring(0, i);
            }
            return codePart
                .StripLambdaExpressionSyntax()
                .CollapseWhitespace()
                .RemoveBlock()
                .Trim();
        }
    }
}
