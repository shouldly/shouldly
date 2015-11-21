using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Shouldly.DifferenceHighlighting;
using Shouldly.MessageGenerators;

namespace Shouldly
{
    internal class ExpectedShouldlyMessage : ShouldlyMessage
    {
        public ExpectedShouldlyMessage(object expected, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldlyAssertionContext(expected);
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    internal class ExpectedActualShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualShouldlyMessage(object expected, object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldlyAssertionContext(expected, actual)
            {
                HasRelevantActual = true
            };
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    internal class ExpectedActualWithCaseSensitivityShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualWithCaseSensitivityShouldlyMessage(object expected, object actual, Case? caseSensitivity, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldlyAssertionContext(expected, actual)
            {
                HasRelevantActual = true,
                CaseSensitivity = caseSensitivity
            };
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    internal class ExpectedActualToleranceShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualToleranceShouldlyMessage(object expected, object actual, object tolerance, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldlyAssertionContext(expected, actual)
            {
                Tolerance = tolerance,
                HasRelevantActual = true
            };
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    internal class ExpectedActualIgnoreOrderShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualIgnoreOrderShouldlyMessage(object expected, object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldlyAssertionContext(expected, actual);
            ShouldlyAssertionContext.IgnoreOrder = true;
            ShouldlyAssertionContext.HasRelevantActual = true;
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    internal class ExpectedActualKeyShouldlyMessage : ShouldlyMessage
    {
        public ExpectedActualKeyShouldlyMessage(object expected, object actual, object key, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldlyAssertionContext(expected, actual)
            {
                Key = key,
                HasRelevantActual = true,
                HasRelevantKey = true
            };
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    internal class ShouldlyThrowMessage : ShouldlyMessage
    {
        public ShouldlyThrowMessage(object expected, string exceptionMessage, Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, null, exceptionMessage);
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }

        public ShouldlyThrowMessage(object expected, object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, actual)
            {
                HasRelevantActual = true
            };
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }

        public ShouldlyThrowMessage(object expected, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected);
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

#if net40

    internal class TaskShouldlyThrowMessage : ShouldlyMessage
    {
        public TaskShouldlyThrowMessage(object expected, string exceptionMessage, Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, null, exceptionMessage, isAsync: true);
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }

        public TaskShouldlyThrowMessage(object expected, object actual, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, actual, isAsync: true)
            {
                HasRelevantActual = true
            };
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }

        public TaskShouldlyThrowMessage(object expected, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldThrowAssertionContext(expected, isAsync: true);
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    internal class CompleteInShouldlyMessage : ShouldlyMessage
    {
        public CompleteInShouldlyMessage(string what, TimeSpan timeout, [InstantHandle] Func<string> customMessage)
        {
            ShouldlyAssertionContext = new ShouldlyAssertionContext(what)
            {
                Timeout = timeout
            };
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }

    /// <summary>
    /// Async methods need stacktrace before we get asynchronous
    /// </summary>
    internal class AsyncShouldlyThrowShouldlyMessage : ShouldlyMessage
    {
        public AsyncShouldlyThrowShouldlyMessage(Type exception, [InstantHandle] Func<string> customMessage, StackTrace stackTrace)
        {
            ShouldlyAssertionContext = new ShouldThrowAssertionContext(exception, stackTrace: stackTrace, isAsync: true);
            if (customMessage != null) ShouldlyAssertionContext.CustomMessage = customMessage();
        }
    }
    #endif

    internal abstract class ShouldlyMessage
    {
        private static readonly IEnumerable<ShouldlyMessageGenerator> ShouldlyMessageGenerators = new ShouldlyMessageGenerator[]
        {
            new ShouldBeNullOrEmptyMessageGenerator(),
            new ShouldBeEmptyMessageGenerator(),
            new ShouldAllBeMessageGenerator(),
    #if net40
            new DynamicShouldMessageGenerator(),
            new ShouldCompleteInMessageGenerator(),
            new ShouldBeNullOrWhiteSpaceMessageGenerator(),
    #endif
            new DictionaryShouldOrNotContainKeyMessageGenerator(),
            new DictionaryShouldContainKeyAndValueMessageGenerator(), 
            new DictionaryShouldNotContainValueForKeyMessageGenerator(),
            new ShouldBeWithinRangeMessageGenerator(), 
            new ShouldContainWithinRangeMessageGenerator(),
            new ShouldBeUniqueMessageGenerator(), 
            new ShouldBeEnumerableCaseSensitiveMessageGenerator(), 
            new ShouldContainMessageGenerator(), 
            new ShouldContainPredicateMessageGenerator(), 
            new ShouldBeIgnoringOrderMessageGenerator(), 
            new ShouldSatisfyAllConditionsMessageGenerator(),
            new ShouldBeSubsetOfMessageGenerator(),
            new ShouldBeBooleanMessageGenerator(),
            new ShouldNotThrowMessageGenerator(),
            new ShouldNotMatchMessageGenerator(),
            new ShouldThrowMessageGenerator(),
            new ShouldBeNullMessageGenerator(),
            new ShouldBeMessageGenerator(),
            new ShouldBePositiveMessageGenerator(),
            new ShouldBeNegativeMessageGenerator()
        };
        private IShouldlyAssertionContext _shouldlyAssertionContext;

        protected IShouldlyAssertionContext ShouldlyAssertionContext
        {
            get { return _shouldlyAssertionContext; }
            set { _shouldlyAssertionContext = value; }
        }

        public override string ToString()
        {
            var message = GenerateShouldMessage();
            if (_shouldlyAssertionContext.CustomMessage != null)
            {
                message += string.Format(@"
    Additional Info:
    {0}", _shouldlyAssertionContext.CustomMessage);
            }
            return message;
        }

        private string GenerateShouldMessage()
        {
            var messageGenerator = ShouldlyMessageGenerators.FirstOrDefault(x => x.CanProcess(_shouldlyAssertionContext));
            if (messageGenerator != null)
            {
                var message = messageGenerator.GenerateErrorMessage(_shouldlyAssertionContext);
                return message;
            }

            if (_shouldlyAssertionContext.HasRelevantActual)
            {
                return CreateActualVsExpectedMessage(_shouldlyAssertionContext);
            }

            return CreateExpectedErrorMessage();
        }

        public string CreateExpectedErrorMessage()
        {
            var format = @"
    {0}
        {1} {2}
    {3}
        but does{4}";

            var codePart = _shouldlyAssertionContext.CodePart;
            var isNegatedAssertion = _shouldlyAssertionContext.ShouldMethod.Contains("Not");

            const string elementSatifyingTheConditionString = "an element satisfying the condition";
            return string.Format(format, codePart, _shouldlyAssertionContext.ShouldMethod.PascalToSpaced(), _shouldlyAssertionContext.Expected is BinaryExpression ? elementSatifyingTheConditionString : "", _shouldlyAssertionContext.Expected.ToStringAwesomely(), isNegatedAssertion ? "" : " not");
        }

        private static string CreateActualVsExpectedMessage(IShouldlyAssertionContext context)
        {
            var codePart = context.CodePart;
            var message = string.Format(@"
    {0}
        {1}
    {2}
        but was
    {3}",
                codePart, context.ShouldMethod.PascalToSpaced(), context.Expected.ToStringAwesomely(),
                context.Actual.ToStringAwesomely());

            if (DifferenceHighlighter.CanHighlightDifferences(context))
            {
                message += string.Format(@"
        difference
    {0}",
                DifferenceHighlighter.HighlightDifferences(context));
            }
            return message;
        }
    }
}
