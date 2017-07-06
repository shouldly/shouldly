using System;
using System.Reflection;

namespace Shouldly
{
    internal static class TypeExtensions
    {
        public static bool IsValueType(this Type type) =>
#if NewReflection
            type.GetTypeInfo().IsValueType;
#else
            type.IsValueType;
#endif
        public static bool IsGenericType(this Type type) =>
#if NewReflection
            type.GetTypeInfo().IsGenericType;
#else
            type.IsGenericType;
#endif

#if NewReflection
        public static bool IsDefined(this Type type, Type attributeType, bool inherit) =>
            type.GetTypeInfo().IsDefined(attributeType, inherit);
#endif
    }
}
