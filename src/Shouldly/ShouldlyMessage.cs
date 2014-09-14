﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Shouldly.DifferenceHighlighting;
using System.Reflection;
using Shouldly.Internals;

namespace Shouldly
{
    internal class ShouldlyMessage
    {
        private readonly TestContext _testContext;
        private readonly object _expected;
        private readonly object _actual;
        private readonly bool _hasActual;
        private static readonly IEnumerable<ShouldlyMessageGenerator> ShouldlyMessageGenerators = new ShouldlyMessageGenerator[] {new ShouldBeNullOrEmptyMessageGenerator(),  new ShouldBeEmptyMessageGenerator(), new DynamicShouldMessageGenerator(), new DefaultMessageGenerator() };

        public ShouldlyMessage(TestContext testContext)
        {
            _testContext = testContext;
        }

        public ShouldlyMessage(object expected)
        {
            _expected = expected;
        }

        public ShouldlyMessage(object expected, object actual)
        {
            _actual = actual;
            _expected = expected;
            _hasActual = true;
        }

        public override string ToString()
        {
            return _hasActual ?
                GenerateShouldMessage(_actual, _expected, _testContext) :
                GenerateShouldMessage(_expected, _testContext);
        }

        private static string GenerateShouldMessage(object actual, object expected, TestContext testContext)
        {
            var environment = GetStackFrameForOriginatingTestMethod();
            environment.TestContext = testContext;
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

        private static string GenerateShouldMessage(object expected, TestContext testContext)
        {
            var environment = GetStackFrameForOriginatingTestMethod();
            environment.TestContext = testContext;
            var message = ShouldlyMessageGenerators.First(x => x.CanProcess(environment)).GenerateErrorMessage(environment, expected);

            return message;
        }

        private static string CreateActualVsExpectedMessage(object actual, object expected, TestEnvironment environment, string codePart)
        {
            string message = string.Format(@"
    {0}
        {1}
    {2}
        but was
    {3}",
                codePart, environment.ShouldMethod.PascalToSpaced(), expected.Inspect(), actual.Inspect());

            if (actual.CanGenerateDifferencesBetween(expected))
            {
                message += string.Format(@"
        difference
    {0}",
                actual.HighlightDifferencesBetween(expected));
            }
            return message;
        }

        private static TestEnvironment GetStackFrameForOriginatingTestMethod()
        {
            var stackTrace = new StackTrace(true);
            var i = 0;
            var currentFrame = stackTrace.GetFrame(i);

            if (currentFrame == null) throw new Exception("Unable to find test method");

            var shouldlyFrame = default(StackFrame);
            while (shouldlyFrame == null || IsShouldlyMethod(currentFrame.GetMethod()))
            {
                if (IsShouldlyMethod(currentFrame.GetMethod()))
                    shouldlyFrame = currentFrame;

                currentFrame = stackTrace.GetFrame(++i);
            }

            var originatingFrame = currentFrame;

            var fileName = originatingFrame.GetFileName();
            return new TestEnvironment
                       {
                           DeterminedOriginatingFrame = fileName != null && File.Exists(fileName),
                           ShouldMethod = shouldlyFrame.GetMethod().Name,
                           FileName = fileName,
                           LineNumber = originatingFrame.GetFileLineNumber() - 1
                       };
        }

        private static bool IsShouldlyMethod(MethodBase method)
        {
            if (method.DeclaringType == null)
                return false;

            return method.DeclaringType.GetCustomAttributes(typeof(ShouldlyMethodsAttribute), true).Any();
        }
    }
}