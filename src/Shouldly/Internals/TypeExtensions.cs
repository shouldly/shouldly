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

        public static bool IsMemory(this Type type, out Type elementType)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().FullName == "System.Memory`1")
            {
                elementType = type.GetGenericArguments()[0];
                return true;
            }

            elementType = null;
            return false;
        }

        public static bool IsReadOnlyMemory(this Type type, out Type elementType)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().FullName == "System.ReadOnlyMemory`1")
            {
                elementType = type.GetGenericArguments()[0];
                return true;
            }

            elementType = null;
            return false;
        }
    }
}
