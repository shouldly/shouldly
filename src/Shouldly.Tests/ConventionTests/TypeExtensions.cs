using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Shouldly.Tests.ConventionTests
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

        public static string FormatMethod(this MethodInfo shouldlyMethod, bool removeCustomMessage = false)
        {
            var parameters = shouldlyMethod.GetParameters();
            var maybeFilteredParameters = removeCustomMessage ? parameters.Where(p => p.Name != "customMessage") : parameters;
            var argList = string.Join(", ", maybeFilteredParameters.Select(p => string.Format("{0} {1}", p.ParameterType.FormatType(), p.Name)));
            var extensionMethodText = shouldlyMethod.IsDefined(typeof (ExtensionAttribute), true)
                ? "this "
                : string.Empty;
            if (shouldlyMethod.IsGenericMethod)
            {
                var genericArgs = string.Join(", ", shouldlyMethod.GetGenericArguments().Select(a => a.FormatType()));
                return string.Format("{0}<{1}>({2}{3})", shouldlyMethod.Name, genericArgs, extensionMethodText, argList);
            }
            return string.Format("{0}({1}{2})", shouldlyMethod.Name, extensionMethodText, argList);
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

            return type.ToString()
                .Replace("System.Object", "object")
                .Replace("System.Int32", "int");
        }
    }
}