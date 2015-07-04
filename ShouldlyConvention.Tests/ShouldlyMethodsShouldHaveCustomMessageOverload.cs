using System;
using System.Linq;
using System.Reflection;
using TestStack.ConventionTests;
using TestStack.ConventionTests.ConventionData;

namespace ShouldlyConvention.Tests
{
    public class ShouldlyMethodsShouldHaveCustomMessageOverload : IConvention<Types>
    {
        public void Execute(Types data, IConventionResultContext result)
        {
            var failingTypes =
                from shouldlyClasses in data
                from shouldlyMethods in shouldlyClasses.GetMethods()
                where typeof (object).GetMethods().All(m => m.Name != shouldlyMethods.Name)
                group shouldlyMethods by CreateKey(shouldlyMethods)
                into shouldlyMethod
                where HasNoCustomMessageOverload(shouldlyMethod)
                select shouldlyMethod.Key;

            result.Is(
                "The following shouldly methods are missing one or more of the custom message overloads",
                failingTypes);
        }

        private static string CreateKey(MethodInfo shouldlyMethods)
        {
            var parameters = shouldlyMethods.GetParameters();
            var parameterType = FormatParameter(parameters[0].ParameterType);
            if (shouldlyMethods.IsGenericMethod)
            {
                return string.Format("{0}(this {1})", shouldlyMethods.Name, parameterType);
            }
            return string.Format("{0}(this {1})", shouldlyMethods.Name, parameterType);
        }

        private static string FormatParameter(Type parameterType)
        {
            if (parameterType.IsGenericType)
            {
                var genericTypeParams = parameterType.GetGenericArguments();
                return string.Format(
                    "{0}<{1}>", 
                    parameterType.Name.Trim('<', '>'),
                    string.Join("", genericTypeParams.Select(FormatParameter)));
            }

            return parameterType.ToString();
        }

        private bool HasNoCustomMessageOverload(IGrouping<string, MethodInfo> shouldlyMethod)
        {
            var hasFuncStringOverload = shouldlyMethod.Any(m => m.GetParameters().Any(IsCustomMessageParameter<Func<string>>));
            var hasStringOverload = shouldlyMethod.Any(m => m.GetParameters().Any(IsCustomMessageParameter<Func<string>>));

            return !hasFuncStringOverload && !hasStringOverload;
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
