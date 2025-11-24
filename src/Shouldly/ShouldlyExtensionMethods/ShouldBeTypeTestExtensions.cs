using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    extension(object? actual)
    {
        /// <summary>
        /// Asserts that the actual object is assignable to the type <typeparamref name="T"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [return: NotNullIfNotNull(nameof(actual))]
        public T? ShouldBeAssignableTo<T>(string? customMessage = null)
        {
            ShouldBeAssignableTo(actual, typeof(T), customMessage);
            return (T?)actual;
        }

        /// <summary>
        /// Asserts that the actual object is assignable to the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeAssignableTo(Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v =>
            {
                if (v == null)
                {
                    return !expected.IsValueType ||
                           (expected.IsGenericType && expected.GetGenericTypeDefinition() == typeof(Nullable<>));
                }

                return expected.IsInstanceOfType(v);
            }, actual, expected, customMessage);
        }
    }

    extension([NotNull] object? actual)
    {
        /// <summary>
        /// Asserts that the actual object is exactly of the type <typeparamref name="T"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public T ShouldBeOfType<T>(string? customMessage = null)
        {
            ShouldBeOfType(actual, typeof(T), customMessage);
            return (T)actual;
        }

        /// <summary>
        /// Asserts that the actual object is exactly of the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBeOfType(Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v => v != null && v.GetType() == expected, actual, expected, customMessage);
            Debug.Assert(actual != null);
        }
    }

    extension(object? actual)
    {
        /// <summary>
        /// Asserts that the actual object is not assignable to the type <typeparamref name="T"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeAssignableTo<T>(string? customMessage = null)
        {
            ShouldNotBeAssignableTo(actual, typeof(T), customMessage);
        }

        /// <summary>
        /// Asserts that the actual object is not assignable to the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeAssignableTo(Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !expected.IsInstanceOfType(v), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that the actual object is not exactly of the type <typeparamref name="T"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeOfType<T>(string? customMessage = null)
        {
            ShouldNotBeOfType(actual, typeof(T), customMessage);
        }

        /// <summary>
        /// Asserts that the actual object is not exactly of the specified type.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldNotBeOfType(Type expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v => v == null || v.GetType() != expected, actual, expected, customMessage);
        }
    }
}
