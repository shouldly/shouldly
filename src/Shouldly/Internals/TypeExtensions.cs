namespace Shouldly;

static class TypeExtensions
{
    public static bool TryGetEnumerable(this object obj,  [NotNullWhen(true)] out IEnumerable? enumerable)
    {
        enumerable = obj as IEnumerable;

        if (enumerable == null && obj != null)
        {
            var objectType = obj.GetType();
            if (objectType.IsMemory(out var genericParameterType))
            {
                var readOnlyMemory = obj.ToReadOnlyMemory(objectType, genericParameterType);

                if (readOnlyMemory != null)
                {
                    enumerable = readOnlyMemory.ToEnumerable(genericParameterType);
                }
            }
            else if (objectType.IsReadOnlyMemory(out genericParameterType))
            {
                enumerable = obj.ToEnumerable(genericParameterType);
            }
        }

        return enumerable != null;
    }

    private static bool IsMemory(this Type type, [NotNullWhen(true)] out Type? elementType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition().FullName == "System.Memory`1")
        {
            elementType = type.GetGenericArguments()[0];
            return true;
        }

        elementType = null;
        return false;
    }

    private static bool IsReadOnlyMemory(this Type type, [NotNullWhen(true)] out Type? elementType)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition().FullName == "System.ReadOnlyMemory`1")
        {
            elementType = type.GetGenericArguments()[0];
            return true;
        }

        elementType = null;
        return false;
    }

    private static IEnumerable ToEnumerable(this object readOnlyMemory, Type elementType)
    {
        return (IEnumerable)Type.GetType("System.Runtime.InteropServices.MemoryMarshal, System.Memory")
            ?.GetMethod("ToEnumerable", BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            ?.MakeGenericMethod(elementType)
            .Invoke(null, [readOnlyMemory])!;
    }

    private static object ToReadOnlyMemory(this object obj, Type objectType, Type genericParameterType)
    {
        return objectType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .SingleOrDefault(method =>
                method.Name == "op_Implicit"
                && method.GetParameters()[0].ParameterType == objectType
                && method.ReturnType.IsReadOnlyMemory(out var returnElementType)
                && returnElementType == genericParameterType)
            ?.Invoke(null, [obj])!;
    }
}