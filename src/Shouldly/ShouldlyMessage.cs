using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Shouldly.DifferenceHighlighting;
using System.Reflection;
using Shouldly.MessageGenerators;

namespace Shouldly
{
    internal class ExpectedShouldlyMessage : ShouldlyMessage
    {
        public ExpectedShouldlyMessage(object expected)
        {
            TestEnvironment = new TestEnvironment(expected);
        }
    }

    internal class ExpectedActualShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualShouldlyMessage(object expected, object actual)
        {
            TestEnvironment = new TestEnvironment(expected, actual);
            TestEnvironment.HasActual = true;
        }
    }

    internal class ExpectedActualToleranceShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualToleranceShouldlyMessage(object expected, object actual, object tolerance)
        {
            TestEnvironment = new TestEnvironment(expected, actual);
            TestEnvironment.Tolerance = tolerance;
            TestEnvironment.HasActual = true;
        }
    }

    internal class ExpectedActualIgnoreOrderShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualIgnoreOrderShouldlyMessage(object expected, object actual)
        {
            TestEnvironment = new TestEnvironment(expected, actual);
            TestEnvironment.IgnoreOrder = true;
            TestEnvironment.HasActual = true;
        }
    }

    internal class ExpectedActualKeyShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualKeyShouldlyMessage(object expected, object actual, object key)
        {
            TestEnvironment = new TestEnvironment(expected, actual);
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
    #if net40
            new DynamicShouldMessageGenerator(), 
    #endif
            new DictionaryShouldOrNotContainKeyMessageGenerator(), 
            new DictionaryShouldContainKeyAndValueMessageGenerator(), 
            new DictionaryShouldNotContainValueForKeyMessageGenerator(),
            new ShouldBeWithinRangeMessageGenerator(), 
            new ShouldContainWithinRangeMessageGenerator(),
            new ShouldBeUniqueMessageGenerator(), 
            new ShouldBeIgnoringOrderMessageGenerator(), 
            new ShouldSatisfyAllConditionsMessageGenerator(),
        };
        private TestEnvironment _testEnvironment;

        protected TestEnvironment TestEnvironment
        {
            get { return _testEnvironment; }
            set { _testEnvironment = value; }
        }

        public override string ToString()
        {
            return GenerateShouldMessage();
        }

        private string GenerateShouldMessage()
        {
            var messageGenerator = ShouldlyMessageGenerators.FirstOrDefault(x => x.CanProcess(_testEnvironment));
            if (messageGenerator != null)
            {
                var message = messageGenerator.GenerateErrorMessage(_testEnvironment);
                return message;
            }

            if (_testEnvironment.HasActual)
            {
                return CreateActualVsExpectedMessage(_testEnvironment);
            }

            return CreateExpectedErrorMessage();
        }

        public string CreateExpectedErrorMessage()
        {
            var format = @"
    {0}
        {1} {2}
    {3}
        but does {4}";

            var codePart = _testEnvironment.CodePart;
            var isNegatedAssertion = _testEnvironment.ShouldMethod.Contains("Not");

            const string elementSatifyingTheConditionString = "an element satisfying the condition";
            return String.Format(format, codePart, _testEnvironment.ShouldMethod.PascalToSpaced(), _testEnvironment.Expected is BinaryExpression ? elementSatifyingTheConditionString : "", _testEnvironment.Expected.Inspect(), isNegatedAssertion ? "" : "not");
        }

        private string CreateActualVsExpectedMessage(TestEnvironment environment)
        {
            var codePart = environment.CodePart;
            string message = string.Format(@"
    {0}
        {1}
    {2}
        but was
    {3}",
                codePart, environment.ShouldMethod.PascalToSpaced(), environment.Expected.Inspect(), environment.Actual.Inspect());

            if (DifferenceHighlighterExtensions.CanGenerateDifferencesBetween(environment))
            {
                message += string.Format(@"
        difference
    {0}",
                DifferenceHighlighterExtensions.HighlightDifferencesBetween(environment));
            }
            return message;
        }
    }
}