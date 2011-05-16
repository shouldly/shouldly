// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NUnit.Core
{
    public class ExpectedExceptionProcessor
    {
        #region Fields
        /// <summary>
        /// The TestMethod to which this exception processor applies
        /// </summary>
        internal TestMethod testMethod;

        /// <summary>
        /// The exception handler method
        /// </summary>
        internal MethodInfo exceptionHandler;

        /// <summary>
        /// The type of any expected exception
        /// </summary>
        internal Type expectedExceptionType;

        /// <summary>
        /// The full name of any expected exception type
        /// </summary>
        internal string expectedExceptionName;

        /// <summary>
        /// The value of any message associated with an expected exception
        /// </summary>
        internal string expectedMessage;

        /// <summary>
        /// A string indicating how to match the expected message
        /// </summary>
        internal string matchType;

        /// <summary>
        /// A string containing any user message specified for the expected exception
        /// </summary>
        internal string userMessage;
        #endregion

        #region Constructor
        public ExpectedExceptionProcessor( TestMethod testMethod )
        {
            this.testMethod = testMethod;
        }

        public ExpectedExceptionProcessor(TestMethod testMethod, object source)
        {
            this.testMethod = testMethod;

            this.expectedExceptionType = GetExceptionType(source);
            this.expectedExceptionName = GetExceptionName(source);
            this.expectedMessage = GetExpectedMessage(source);
            this.matchType = GetMatchType(source);
            this.userMessage = GetUserMessage(source);

            string handlerName = GetHandler(source);
            if (handlerName == null)
                this.exceptionHandler = GetDefaultExceptionHandler(testMethod.FixtureType);
            else
            {
                MethodInfo handler = GetExceptionHandler(testMethod.FixtureType, handlerName);
                if (handler != null)
                    this.exceptionHandler = handler;
                else
                {
                    testMethod.RunState = RunState.NotRunnable;
                    testMethod.IgnoreReason = string.Format(
                        "The specified exception handler {0} was not found", handlerName);
                }
            }
        }
        #endregion

        #region Public Methods
        public void ProcessNoException(TestResult testResult)
        {
            testResult.Failure(NoExceptionMessage(), null);
        }

        public void ProcessException(Exception exception, TestResult testResult)
        {
            if (exception is NUnitException)
                exception = exception.InnerException;

            if (IsExpectedExceptionType(exception))
            {
                if (IsExpectedMessageMatch(exception))
                {
                    if (exceptionHandler != null)
                        Reflect.InvokeMethod(exceptionHandler, testMethod.Fixture, exception);

                    testResult.Success();
                }
                else
                {
                    testResult.Failure(WrongTextMessage(exception), GetStackTrace(exception));
                }
            }
            else
            {
                switch (NUnitFramework.GetResultState(exception))
                {
                    case ResultState.Failure:
                        testResult.Failure(exception.Message, exception.StackTrace);
                        break;
                    case ResultState.Ignored:
                        testResult.Ignore(exception);
                        break;
                    case ResultState.Inconclusive:
                        testResult.SetResult(ResultState.Inconclusive, exception, FailureSite.Test);
                        break;
                    case ResultState.Success:
                        testResult.Success(exception.Message);
                        break;
                    default:
                        testResult.Failure(WrongTypeMessage(exception), GetStackTrace(exception));
                        break;
                }
            }
		}
        #endregion

        #region Helper Methods
        private bool IsExpectedExceptionType(Exception exception)
        {
            return expectedExceptionName == null ||
                expectedExceptionName.Equals(exception.GetType().FullName);
        }

        private bool IsExpectedMessageMatch(Exception exception)
        {
            if (expectedMessage == null)
                return true;

            switch (matchType)
            {
                case "Exact":
                default:
                    return expectedMessage.Equals(exception.Message);
                case "Contains":
                    return exception.Message.IndexOf(expectedMessage) >= 0;
                case "Regex":
                    return Regex.IsMatch(exception.Message, expectedMessage);
                case "StartsWith":
                    return exception.Message.StartsWith(expectedMessage);
            }
        }

        private string NoExceptionMessage()
        {
            string expectedType = expectedExceptionName == null ? "An Exception" : expectedExceptionName;
            return CombineWithUserMessage(expectedType + " was expected");
        }

        private string WrongTypeMessage(Exception exception)
        {
            return CombineWithUserMessage(
                "An unexpected exception type was thrown" + Environment.NewLine +
                "Expected: " + expectedExceptionName + Environment.NewLine +
                " but was: " + exception.GetType().FullName + " : " + exception.Message);
        }

        private string WrongTextMessage(Exception exception)
        {
            string expectedText;
            switch (matchType)
            {
                default:
                case "Exact":
                    expectedText = "Expected: ";
                    break;
                case "Contains":
                    expectedText = "Expected message containing: ";
                    break;
                case "Regex":
                    expectedText = "Expected message matching: ";
                    break;
                case "StartsWith":
                    expectedText = "Expected message starting: ";
                    break;
            }

            return CombineWithUserMessage(
                "The exception message text was incorrect" + Environment.NewLine +
                expectedText + expectedMessage + Environment.NewLine +
                " but was: " + exception.Message);
        }

        private string CombineWithUserMessage(string message)
        {
            if (userMessage == null)
                return message;
            return userMessage + Environment.NewLine + message;
        }

        private string GetStackTrace(Exception exception)
        {
            try
            {
                return exception.StackTrace;
            }
            catch (Exception)
            {
                return "No stack trace available";
            }
        }

        private static MethodInfo GetDefaultExceptionHandler(Type fixtureType)
        {
            return Reflect.HasInterface(fixtureType, NUnitFramework.ExpectExceptionInterface)
                ? GetExceptionHandler(fixtureType, "HandleException")
                : null;
        }

        private static MethodInfo GetExceptionHandler(Type fixtureType, string name)
        {
            return Reflect.GetNamedMethod(
                fixtureType,
                name,
                new string[] { "System.Exception" });
        }

        private static string GetHandler(object source)
        {
            return Reflect.GetPropertyValue(source, "Handler") as string;
        }

        private static Type GetExceptionType(object source)
        {
            Type type = Reflect.GetPropertyValue(source, PropertyNames.ExpectedException) as Type;

            // Prior to 2.5, NUnit used an 'ExceptionType' property
            if (type == null)
                type = Reflect.GetPropertyValue(source, PropertyNames.LegacyExceptionType) as Type;

            return type;
        }

        private static string GetExceptionName(object source)
        {
            string name = Reflect.GetPropertyValue(source, PropertyNames.ExpectedExceptionName) as string;

            // Prior to 2.5, NUnit used an 'ExceptionName' property
            if (name == null)
                name = Reflect.GetPropertyValue(source, PropertyNames.LegacyExceptionName) as string;

            return name;
        }

        private static string GetExpectedMessage(object source)
        {
            return Reflect.GetPropertyValue(source, PropertyNames.ExpectedMessage) as string;
        }

        private static string GetMatchType(object source)
        {
            object matchEnum = Reflect.GetPropertyValue(source, "MatchType");
            return matchEnum != null ? matchEnum.ToString() : null;
        }

        private static string GetUserMessage(object source)
        {
            return Reflect.GetPropertyValue(source, "UserMessage") as string;
        }
        #endregion
    }
}
