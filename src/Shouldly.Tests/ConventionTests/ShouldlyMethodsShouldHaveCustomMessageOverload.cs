using System;
using System.Linq;
using System.Reflection;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace Shouldly.Tests.ConventionTests
{
    public class ShouldlyMethodsShouldHaveCustomMessageOverload : IConvention<Types>
    {
        public void Execute(Types data, IConventionResultContext result)
        {
            var failingTypes =
                from shouldlyClasses in data
                from shouldlyMethods in shouldlyClasses.GetMethods()
                where typeof (object).GetMethods().All(m => m.Name != shouldlyMethods.Name)
                group shouldlyMethods by FormatKey(shouldlyMethods) into shouldlyMethod
                where HasNoCustomMessageOverload(shouldlyMethod)
                select shouldlyMethod.Key;

            result.Is(
                "The following shouldly methods are missing one or more of the custom message overloads",
                failingTypes);
        }

        private string FormatKey(MethodInfo shouldlyMethods)
        {
            return shouldlyMethods.FormatMethod(true);
        }

        private bool HasNoCustomMessageOverload(IGrouping<string, MethodInfo> shouldlyMethod)
        {
            var hasFuncStringOverload = shouldlyMethod.Any(m => m.GetParameters().Any(IsCustomMessageParameter<Func<string>>));
            var hasStringOverload = shouldlyMethod.Any(m => m.GetParameters().Any(IsCustomMessageParameter<string>));

            return !hasFuncStringOverload || !hasStringOverload;
        }

        private static bool IsCustomMessageParameter<T>(ParameterInfo p)
        {
            return p.Name == "customMessage" && p.ParameterType == typeof(T);
        }

        public string ConventionReason
        {
            get { return "API Consistency"; }
        }
    }
}
