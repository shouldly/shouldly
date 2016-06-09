#if HasStackTraceSupport
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
#endif

namespace Shouldly.Internals
{
#if !HasStackTraceSupport
    internal class ActualCodeTextGetter : ICodeTextGetter
    {
        public string GetCodeText(object actual)
        {
            return actual.ToStringAwesomely();
        }
    }
#else
    internal class ActualCodeTextGetter : ICodeTextGetter
    {
        bool _determinedOriginatingFrame;
        string _shouldMethod;

        public StackFrame ShouldlyFrame { get; private set; }
        public int ShouldlyFrameIndex { get; private set; }
        public string FileName { get; private set; }
        public int LineNumber { get; private set; }

        public string GetCodeText(object actual, StackTrace stackTrace)
        {
            if (ShouldlyConfiguration.IsSourceDisabledInErrors())
                return actual.ToStringAwesomely();
            ParseStackTrace(stackTrace);
            return GetCodePart();
        }

        void ParseStackTrace(StackTrace trace)
        {
            var stackTrace = trace ?? new StackTrace(true);
            var i = 0;
            var currentFrame = stackTrace.GetFrame(i);

            if (currentFrame == null) throw new Exception("Unable to find test method");

            ShouldlyFrame = default(StackFrame);
            while (ShouldlyFrame == null || currentFrame.GetMethod().IsShouldlyMethod())
            {
                if (currentFrame.GetMethod().IsShouldlyMethod())
                    ShouldlyFrame = currentFrame;

                currentFrame = stackTrace.GetFrame(++i);

                // Required to support the DynamicShould.HaveProperty method that takes in a dynamic as a parameter.
                // Having a method that takes a dynamic really stuffs up the stack trace because the runtime binder
                // has to inject a whole heap of methods. Our normal way of just taking the next frame doesn't work.
                // The following two lines seem to work for now, but this feels like a hack. The conditions to be able to 
                // walk up stack trace until we get to the calling method might have to be updated regularly as we find more
                // scanarios. Alternately, it could be replaced with a more robust implementation.
                while (currentFrame.GetMethod().DeclaringType == null ||
                        currentFrame.GetMethod().DeclaringType.FullName.StartsWith("System.Dynamic"))
                {
                    currentFrame = stackTrace.GetFrame(++i);
                }
            }

            var originatingFrame = currentFrame;
            ShouldlyFrameIndex = i - 1;

            var fileName = originatingFrame.GetFileName();
            _determinedOriginatingFrame = fileName != null && File.Exists(fileName);
            _shouldMethod = ShouldlyFrame.GetMethod().Name;
            FileName = fileName;
            LineNumber = originatingFrame.GetFileLineNumber() - 1;
        }


        string GetCodePart()
        {
            var codePart = "Shouldly uses your source code to generate its great error messages, build your test project with full debug information to get better error messages" +
                           "\nThe provided expression";

            if (_determinedOriginatingFrame)
            {
                var codeLines = string.Join("\n", File.ReadAllLines(FileName).Skip(LineNumber).ToArray());

                var indexOf = codeLines.IndexOf(_shouldMethod);
                if (indexOf > 0)
                    codePart = codeLines.Substring(0, indexOf - 1).Trim();

                // When the static method is used instead of the extension method,
                // the code part will be "Should".
                // Using Endswith to cater for being inside a lambda
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
                _shouldMethod.Length;

            var parameterString = codeLines.Substring(indexOfParameters);
            // Remove generic parameter if need be
            parameterString = parameterString.StartsWith("<")
                ? parameterString.Substring(parameterString.IndexOf(">", StringComparison.Ordinal) + 2)
                : parameterString.Substring(1);

            var parantheses = new Dictionary<char, char>
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

                if (parantheses.ContainsKey(currentChar))
                {
                    openParentheses.Add(parantheses[currentChar]);
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
#endif
}
