using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Shouldly.DifferenceHighlighting;
using System.Reflection;

namespace Shouldly
{
    internal class ExpectedShouldlyMessage : ShouldlyMessage
    {
        public ExpectedShouldlyMessage(object expected)
        {
            TestEnvironment = GetStackFrameForOriginatingTestMethod(expected);
        }
    }

    internal class ExpectedActualShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualShouldlyMessage(object expected, object actual)
        {
            TestEnvironment = GetStackFrameForOriginatingTestMethod(expected, actual);
            TestEnvironment.HasActual = true;
        }
    }

    internal class ExpectedActualToleranceShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualToleranceShouldlyMessage(object expected, object actual, object tolerance)
        {
            TestEnvironment = GetStackFrameForOriginatingTestMethod(expected, actual);
            TestEnvironment.Tolerance = tolerance;
            TestEnvironment.HasActual = true;
        }
    }

    internal class ExpectedActualKeyShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualKeyShouldlyMessage(object expected, object actual, object key)
        {
            TestEnvironment = GetStackFrameForOriginatingTestMethod(expected, actual);
            TestEnvironment.Key = key;
            TestEnvironment.HasActual = true;
            TestEnvironment.HasKey = true; 
        }
    }

    internal abstract class ShouldlyMessage
    {
        private static readonly IEnumerable<ShouldlyMessageGenerator> ShouldlyMessageGenerators = new ShouldlyMessageGenerator[]
        {
            new ShouldBeNullOrEmptyMessageGenerator(),  
			new ShouldBeEmptyMessageGenerator(), 
			new DynamicShouldMessageGenerator(), 
			new DictionaryShouldOrNotContainKeyMessageGenerator(), 
			new DictionaryShouldContainKeyAndValueMessageGenerator(), 
			new DictionaryShouldNotContainValueForKeyMessageGenerator(),
            new ShouldBeWithinRangeMessageGenerator(), 
            new ShouldContainWithinRangeMessageGenerator(),
            new ShouldBeUniqueMessageGenerator(), 
        };
        private TestEnvironment _testEnvironment;

        protected TestEnvironment TestEnvironment
        {
            get { return _testEnvironment; }
            set { _testEnvironment = value; }
        }
        
        public override string ToString()
        {
            return   GenerateShouldMessage();
        }

        private string GenerateShouldMessage()
        {
            var messageGenerator = ShouldlyMessageGenerators.FirstOrDefault(x => x.CanProcess(_testEnvironment));
            if (messageGenerator != null)
            {
                var message = messageGenerator.GenerateErrorMessage(_testEnvironment);
                return message;
            }
            else
            {
                if (_testEnvironment.HasActual)
                {
                    return CreateActualVsExpectedMessage(_testEnvironment.Actual, _testEnvironment.Expected, _testEnvironment, _testEnvironment.GetCodePart());
                }
                else
                {
                    return CreateExpectedErrorMessage();
                }
            }
        }

        public string CreateExpectedErrorMessage()
        {
            var format = @"
    {0}
        {1} {2}
    {3}
        but does {4}";

            var codePart = _testEnvironment.GetCodePart();
            var isNegatedAssertion = _testEnvironment.ShouldMethod.Contains("Not");

            const string elementSatifyingTheConditionString = "an element satisfying the condition";
            return String.Format(format, codePart, _testEnvironment.ShouldMethod.PascalToSpaced(), _testEnvironment.Expected is BinaryExpression ? elementSatifyingTheConditionString : "", _testEnvironment.Expected.Inspect(), isNegatedAssertion ? "" : "not");
        }

        private string CreateActualVsExpectedMessage(object actual, object expected, TestEnvironment environment, string codePart)
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

        // TODO: Move all this logic into the TestEnvironment class itself. Perhaps as part of it's constructor
        protected static TestEnvironment GetStackFrameForOriginatingTestMethod(object expected, object actual = null)
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

                // Required to support the DynamicShould.HaveProperty method that takes in a dynamic as a parameter.
                // Having a method that takes a dynamic really stuffs up the stack trace because the runtime binder
                // has to inject a whole heap of methods. Our normal way of just taking the next frame doesn't work.
                // The following two lines seem to work for now, but this feels like a hack. The conditions to be able to 
                // walk up stack trace until we get to the calling method might have to be updated regularly as we find more
                // scanarios. Alternately, it could be replaced with a more robust implementation.
                while ( currentFrame.GetMethod().DeclaringType == null ||
                        currentFrame.GetMethod().DeclaringType.FullName.StartsWith("System.Dynamic"))
                {
                    currentFrame = stackTrace.GetFrame(++i);
                }
            }

            var originatingFrame = currentFrame;

            var fileName = originatingFrame.GetFileName();
            return new TestEnvironment
                       {
                           DeterminedOriginatingFrame = fileName != null && File.Exists(fileName),
                           ShouldMethod = shouldlyFrame.GetMethod().Name,
                           UnderlyingShouldMethod = shouldlyFrame.GetMethod(),
                           FileName = fileName,
                           LineNumber = originatingFrame.GetFileLineNumber() - 1,
                           OriginatingFrame = originatingFrame,
                           Expected = expected,
                           Actual = actual
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