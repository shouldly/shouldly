using System;
using System.Collections;
using System.Linq;
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
            if (type.IsGenericType() && type.GetGenericTypeDefinition().FullName == "System.Memory`1")
            {
                elementType = type.GetGenericArguments()[0];
                return true;
            }

            elementType = null;
            return false;
        }

        public static bool IsReadOnlyMemory(this Type type, out Type elementType)
        {
            if (type.IsGenericType() && type.GetGenericTypeDefinition().FullName == "System.ReadOnlyMemory`1")
            {
                elementType = type.GetGenericArguments()[0];
                return true;
            }

            elementType = null;
            return false;
        }

        public static IEnumerable ToEnumerable(this object readOnlyMemory, Type elementType)
        {
            return (IEnumerable)Type.GetType("System.Runtime.InteropServices.MemoryMarshal, System.Memory")
                ?.GetMethod("ToEnumerable", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                ?.MakeGenericMethod(elementType)
                .Invoke(null, new[] { readOnlyMemory });
        }

        public static object ToReadOnlyMemory(this object obj, Type objectType, Type genericParameterType)
        {
            var readOnlyMemory = objectType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .SingleOrDefault(method =>
                    method.Name == "op_Implicit"
                    && method.GetParameters()[0].ParameterType == objectType
                    && method.ReturnType.IsReadOnlyMemory(out var returnElementType)
                    && returnElementType == genericParameterType)
                ?.Invoke(null, new[] { obj });
            return readOnlyMemory;
        }


    }
}
