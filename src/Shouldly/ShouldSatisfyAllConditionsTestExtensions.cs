using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using System.Text;
using JetBrains.Annotations;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions(this object actual, params Action[] conditions)
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
                throw new ChuckedAWobbly(new ExpectedShouldlyMessage(errorMessageString).ToString());
            }
        }

        private static string BuildErrorMessageString(IEnumerable<Exception> errorMessages)
        {
            var errorCount = 1;
            var sb = new StringBuilder();
            foreach (var errorMessage in errorMessages)
            {
                sb.AppendLine(string.Format("--------------- Error {0} ---------------", errorCount));
                sb.AppendLine(errorMessage.Message.StripLambdaExpressionSyntax());
                sb.AppendLine();
                errorCount++;
            }
            sb.AppendLine("-----------------------------------------");

            return sb.ToString();
        }
    }
}
