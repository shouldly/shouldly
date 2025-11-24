using System.Reflection;
using System.Runtime.CompilerServices;

namespace Shouldly.Tests.ConventionTests;

public static class TypeExtensions
{
    extension(Type type)
    {
        public bool HasAttribute<TAttribute>() =>
            type.GetCustomAttributes(typeof(TAttribute), true).Any();

        public bool HasAttribute(string attributeName)
        {
            return type.GetCustomAttributes(true).Cast<Attribute>().Any(a => a.GetType().FullName == attributeName);
        }
    }

    public static string FormatMethod(this MethodInfo shouldlyMethod, bool removeCustomMessage = false)
    {
        var parameters = shouldlyMethod.GetParameters();
        var maybeFilteredParameters = removeCustomMessage ? parameters.Where(p => p.Name != "customMessage") : parameters;
        var argList = string.Join(", ", maybeFilteredParameters.Select(p => $"{p.ParameterType.FormatType()} {p.Name}"));
        var extensionMethodText = shouldlyMethod.IsDefined(typeof(ExtensionAttribute), true)
            ? "this "
            : string.Empty;
        if (shouldlyMethod.IsGenericMethod)
        {
            var genericArgs = string.Join(", ", shouldlyMethod.GetGenericArguments().Select(a => a.FormatType()));
            return $"{shouldlyMethod.Name}<{genericArgs}>({extensionMethodText}{argList})";
        }

        return $"{shouldlyMethod.Name}({extensionMethodText}{argList})";
    }

    public static string FormatType(this Type type)
    {
        if (type.IsGenericType)
        {
            var genericTypeParams = type.GetGenericArguments();
            return $"{type.Name.Trim('<', '>')}<{string.Join("", genericTypeParams.Select(FormatType))}>";
        }

        return type.ToString()
            .Replace("System.Object", "object")
            .Replace("System.Int32", "int");
    }
}