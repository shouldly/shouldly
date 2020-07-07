using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using JetBrains.Annotations;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions<T>(this T actual, [InstantHandle] params Action<T>[] conditions)
        {
          ShouldSatisfyAllConditions(actual, null, CreateParameterlessActions(actual, conditions));
        }
        public static void ShouldSatisfyAllConditions<T>(this T actual, string? customMessage, [InstantHandle] params Action<T>[] conditions)
        {
          ShouldSatisfyAllConditions(actual, customMessage, CreateParameterlessActions(actual, conditions));
        }
        public static void ShouldSatisfyAllConditions(this object? actual, [InstantHandle] params Action[] conditions)
        {
            ShouldSatisfyAllConditions(actual, null, conditions);
        }
        public static void ShouldSatisfyAllConditions(this object? actual, string? customMessage, [InstantHandle] params Action[] conditions)
        {
            var errorMessages = new List<Exception>();
            foreach (var action in conditions)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception exc)
                {
                    errorMessages.Add(exc);
                }
            }

            if (errorMessages.Any())
            {
                var errorMessageString = BuildErrorMessageString(errorMessages);
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(errorMessageString, actual, () => customMessage).ToString());
            }
        }

        static Action[] CreateParameterlessActions<T>(T parameter, params Action<T>[] actions) {
          return actions.Select(a => new Action(() => a(parameter))).ToArray();
        }

        static string BuildErrorMessageString(IEnumerable<Exception> errorMessages)
        {
            var errorCount = 1;
            var sb = new StringBuilder();
            foreach (var errorMessage in errorMessages)
            {
                sb.AppendLine($"--------------- Error {errorCount} ---------------");
                var lines = errorMessage.Message.Replace("\r\n", "\n").Split('\n');
                var paddedLines = lines.Select(l => string.IsNullOrEmpty(l) ? l : "    " + l);
                var value = string.Join("\r\n", paddedLines.ToArray());
                sb.AppendLine(value);
                sb.AppendLine();
                errorCount++;
            }
            sb.AppendLine("-----------------------------------------");

            return sb.ToString().TrimEnd();
        }
    }
}
