using System.ComponentModel;
using JetBrains.Annotations;
using Shouldly.Internals;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeTestExtensions
{
    extension<T>([NotNullIfNotNull("expected")] T? actual)
    {
        /// <summary>
        /// Asserts that an actual value is equal to the expected value
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] T? expected,
            string? customMessage = null)
        {
            if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!) || typeof(T) == typeof(string))
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<T>()), actual, expected, customMessage);
            else
                actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that an actual value is equal to the expected value using the specified comparer
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] T? expected,
            IEqualityComparer<T> comparer,
            string? customMessage = null)
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage);
        }
    }

    extension<T>(T? actual)
    {
        /// <summary>
        /// Asserts that an actual value is not equal to the expected value
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ContractAnnotation("actual:null,expected:null => halt")]
        public void ShouldNotBe(T? expected, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected, customMessage);
        }

        /// <summary>
        /// Asserts that an actual value is not equal to the expected value using the specified comparer
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ContractAnnotation("actual:null,expected:null => halt")]
        public void ShouldNotBe(T? expected, IEqualityComparer<T> comparer, string? customMessage = null)
        {
            actual.AssertAwesomely(v => !Is.Equal(v, expected, comparer), actual, expected, customMessage);
        }
    }

    extension<T>([NotNullIfNotNull("expected")] IEnumerable<T>? actual)
    {
        /// <summary>
        /// Asserts that an enumerable is equal to another enumerable, optionally ignoring order
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
            bool ignoreOrder = false)
        {
            ShouldBe(actual, expected, ignoreOrder, null);
        }

        /// <summary>
        /// Asserts that an enumerable is equal to another enumerable, optionally ignoring order
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
            bool ignoreOrder,
            string? customMessage)
        {
            actual = EnumerableProxy<T>.WrapNonCollection(actual);
            expected = EnumerableProxy<T>.WrapNonCollection(expected);

            if (!ignoreOrder && ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!))
            {
                actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<IEnumerable<T>?>()), actual, expected, customMessage);
            }
            else
            {
                if (ignoreOrder)
                {
                    actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected), actual, expected, customMessage);
                }
                else
                {
                    actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
                }
            }
        }

        /// <summary>
        /// Asserts that an enumerable is equal to another enumerable using the specified comparer, optionally ignoring order
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ShouldBe([NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
            IEqualityComparer<T> comparer,
            bool ignoreOrder = false,
            string? customMessage = null)
        {
            if (ignoreOrder)
            {
                actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected, comparer), actual, expected, customMessage);
            }
            else
            {
                actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage);
            }
        }
    }

    /// <summary>
    /// Asserts that a decimal enumerable is equal to another decimal enumerable within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }

    /// <summary>
    /// Asserts that an object is the same instance as another object
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeSameAs(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that an object is not the same instance as another object
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected, customMessage);
    }
}