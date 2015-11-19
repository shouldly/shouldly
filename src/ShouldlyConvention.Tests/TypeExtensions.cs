using System;
using System.Linq;
using System.Reflection;

namespace ShouldlyConvention.Tests
{
    public static class TypeExtensions
    {
        public static bool HasAttribute<TAttribute>(this Type type)
        {
            return type.GetCustomAttributes(typeof(TAttribute), true).Any();
        }

        public static bool HasAttribute(this Type type, string attributeName)
        {
            return type.GetCustomAttributes(true).Cast<Attribute>().Any(a => a.GetType().FullName == attributeName);
        }

        public static string FormatMethod(this MethodInfo shouldlyMethods, bool onlyIncludeFirstParam = false)
        {
            var parameters = shouldlyMethods.GetParameters();
            var parameterType = FormatType(parameters[0].ParameterType);
            if (shouldlyMethods.IsGenericMethod)
            {
                string otherArgs = string.Empty;
                if (!onlyIncludeFirstParam)
                    otherArgs = string.Join(", ", parameters.Skip(1).Select(p => string.Format("{0} {1}", p.ParameterType.FormatType(), p.Name)));
                if (!string.IsNullOrEmpty(otherArgs))
                    otherArgs = ", " + otherArgs;
                return string.Format("{0}(this {1} {2}{3})", shouldlyMethods.Name, parameterType, parameters[0].Name, otherArgs);
            }
            return string.Format("{0}(this {1})", shouldlyMethods.Name, parameterType);
        }

        public static string FormatType(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericTypeParams = type.GetGenericArguments();
                return string.Format(
                    "{0}<{1}>",
                    type.Name.Trim('<', '>'),
                    string.Join("", genericTypeParams.Select(FormatType)));
            }

            return type.ToString();
        }
    }
}