using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Shouldly
{
    public static class ShouldlyRhinoExtensions
    {
        public static void ShouldHaveBeenCalled<T>(this T mock, Expression<Action<T>> action)
        {
            try
            {
                mock.AssertWasCalled(action.Compile());
            }
            catch (ExpectationViolationException)
            {
                var methodCalls = mock.GetArgumentsForCallsMadeOn(action.Compile());

                var body = action.Body.As<MethodCallExpression>();

                var expectedCall = MethodCall(body.Method.Name, body.Arguments.Cast<object>());
                var recordedCalls = methodCalls.Select(args => MethodCall(body.Method.Name, args));

                throw new AssertionException(string.Format(
@"*Expecting*
    {0}
*Recorded*
{1}", expectedCall, recordedCalls.Select((c, i) => string.Format(@"{0: 0}: {1}", i, c)).DelimitWith("\n")));
            }
        }

        private static string MethodCall(string name, IEnumerable<object> arguments)
        {
            return string.Format("{0}({1})", 
                                 name, 
                                 arguments.Select(a => a.Inspect()).CommaDelimited());
        }
   }
}