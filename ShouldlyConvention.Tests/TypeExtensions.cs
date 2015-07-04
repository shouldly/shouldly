using System;
using System.Linq;

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
    }
}