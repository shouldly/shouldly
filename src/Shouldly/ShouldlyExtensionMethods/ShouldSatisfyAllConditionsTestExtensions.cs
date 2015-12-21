using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using System.Text;
using JetBrains.Annotations;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions(this object actual, [InstantHandle] params Action[] conditions)
        {
            ShouldSatisfyAllConditions(actual, () => null, conditions);
        }
        public static void ShouldSatisfyAllConditions(this object actual, string customMessage, [InstantHandle] params Action[] conditions)
        {
            ShouldSatisfyAllConditions(actual, () => customMessage, conditions);
        }
        public static void ShouldSatisfyAllConditions(this object actual, [InstantHandle] Func<string> customMessage, [InstantHandle] params Action[] conditions)
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
                throw new ShouldAssertException(new ExpectedActualShouldlyMessage(errorMessageString, actual, customMessage).ToString());
            }
        }

        static string BuildErrorMessageString(IEnumerable<Exception> errorMessages)
        {
            var errorCount = 1;
            var sb = new StringBuilder();
            foreach (var errorMessage in errorMessages)
            {
                sb.AppendLine($"--------------- Error {errorCount} ---------------");
                sb.AppendLine(string.Join("\r\n", errorMessage.Message.Replace("\r\n", "\n").Split('\n').Select(l => string.IsNullOrEmpty(l) ? l : "    " + l).ToArray()));
                sb.AppendLine();
                errorCount++;
            }
            sb.AppendLine("-----------------------------------------");

            return sb.ToString().TrimEnd();
        }
    }
}
