using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shouldly.Configuration;

namespace Shouldly
{
    class ObsoleteMessages
    {
        public const string FuncCustomMessage = "Func based customMessage overloads have been removed. Pass in a string for the cusomMessage.";
    }

    public static partial class DynamicShould
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void HaveProperty(dynamic dynamicTestObject, string propertyName, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ObjectGraphTestExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeEquivalentTo(this object? actual, object? expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class Should
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void CompleteIn(Action action, TimeSpan timeout, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void CompleteIn(Func<Task> actual, TimeSpan timeout, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void CompleteIn(Task actual, TimeSpan timeout, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T CompleteIn<T>(Func<Task<T>> actual, TimeSpan timeout, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T CompleteIn<T>(Func<T> function, TimeSpan timeout, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T CompleteIn<T>(Task<T> actual, TimeSpan timeout, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void NotThrow(Action action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void NotThrow(Func<Task> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void NotThrow(Task action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void NotThrow(Func<Task> action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void NotThrow(Task action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T NotThrow<T>(Func<Task<T>> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T NotThrow<T>(Func<T> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T NotThrow<T>(Task<T> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T NotThrow<T>(Func<Task<T>> action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T NotThrow<T>(Task<T> action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task NotThrowAsync(Func<Task> actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task NotThrowAsync(Task task, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception Throw(Action actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception Throw(Func<Task> actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception Throw(Func<object?> actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception Throw(Task actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception Throw(Func<Task> actual, TimeSpan timeoutAfter, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception Throw(Task actual, TimeSpan timeoutAfter, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException Throw<TException>(Action actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException Throw<TException>(Func<Task> actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException Throw<TException>(Func<object?> actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException Throw<TException>(Task actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException Throw<TException>(Func<Task> actual, TimeSpan timeoutAfter, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException Throw<TException>(Task actual, TimeSpan timeoutAfter, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<Exception> ThrowAsync(Func<Task> actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<Exception> ThrowAsync(Task task, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<TException> ThrowAsync<TException>(Func<Task> actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<TException> ThrowAsync<TException>(Task task, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldBeBooleanExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeFalse(this bool actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeTrue(this bool actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldBeDecoratedWithExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeDecoratedWith<T>(this Type actual, Func<string?>? customMessage)
            where T : Attribute
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldBeDictionaryTestExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<string?>? customMessage)
            where TKey : notnull
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContainKeyAndValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, Func<string?>? customMessage)
            where TKey : notnull
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<string?>? customMessage)
            where TKey : notnull
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotContainValueForKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue val, Func<string?>? customMessage)
            where TKey : notnull
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldBeEnumerableTestExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldAllBe<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this IEnumerable<string> actual, IEnumerable<string> expected, Case caseSensitivity, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeEmpty<T>(this IEnumerable<T>? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeInOrder<T>(this IEnumerable<T> actual, SortDirection expectedSortDirection, IComparer<T>? customComparer, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeOfTypes<T>(this IEnumerable<T> actual, Type[] expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeSubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeUnique<T>(this IEnumerable<T> actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContain(this IEnumerable<double> actual, double expected, double tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContain(this IEnumerable<float> actual, float expected, double tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContain<T>(this IEnumerable<T> actual, T expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate, int expectedCount, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldHaveSingleItem<T>(this IEnumerable<T>? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeEmpty<T>(this IEnumerable<T>? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotContain<T>(this IEnumerable<T> actual, T expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldBeNullExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNull<T>(this T? actual, Func<string?>? customMessage)
            where T : class
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeNull<T>(this T? actual, Func<string?>? customMessage)
            where T : class
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldBeStringTestExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this string? actual, string? expected, Func<string?> customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this string? actual, string? expected, Func<string?> customMessage, StringCompareShould options)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNullOrEmpty(this string? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNullOrWhiteSpace(this string? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContain(this string actual, string expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContain(this string actual, string expected, Func<string?>? customMessage, Case caseSensitivity)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldContainWithoutWhitespace(this string actual, object? expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldEndWith(this string? actual, string expected, Func<string?>? customMessage, Case caseSensitivity = Case.Insensitive)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldMatch(this string actual, string regexPattern, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeNullOrEmpty(this string? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeNullOrWhiteSpace(this string? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotContain(this string actual, string expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotContain(this string actual, string expected, Func<string?>? customMessage, Case caseSensitivity)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotEndWith(this string? actual, string expected, Func<string?>? customMessage, Case caseSensitivity = Case.Insensitive)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotMatch(this string actual, string regexPattern, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotStartWith(this string? actual, string expected, Func<string?>? customMessage, Case caseSensitivity = Case.Insensitive)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldStartWith(this string? actual, string expected, Func<string?>? customMessage, Case caseSensitivity = Case.Insensitive)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldBeTestExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this IEnumerable<double> actual, IEnumerable<double> expected, double tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this IEnumerable<float> actual, IEnumerable<float> expected, double tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this double actual, double expected, double tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe(this float actual, float expected, double tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe<T>(this T actual, T expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBe<T>(this IEnumerable<T>? actual, IEnumerable<T>? expected, bool ignoreOrder, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeAssignableTo(this object? actual, Type expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldBeAssignableTo<T>(this object? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, Func<string?>? customMessage)
            where T : IComparable<T>?
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, Func<string?>? customMessage)
            where T : IComparable<T>?
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeInRange<T>(this T actual, T from, T to, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeLessThan<T>(this T actual, T expected, Func<string?>? customMessage)
            where T : IComparable<T>?
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeLessThan<T>(this T actual, T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, Func<string?>? customMessage)
            where T : IComparable<T>?
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, IComparer<T> comparer, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNegative(this decimal actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNegative(this double actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNegative(this float actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNegative(this int actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNegative(this long actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeNegative(this short actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeOfType(this object? actual, Type expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldBeOfType<T>(this object? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBePositive(this decimal actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBePositive(this double actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBePositive(this float actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBePositive(this int actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBePositive(this long actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBePositive(this short actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldBeSameAs(this object? actual, object? expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBe<T>(this T actual, T expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeAssignableTo(this object? actual, Type expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeAssignableTo<T>(this object? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeInRange<T>(this T actual, T from, T to, Func<string?>? customMessage)
            where T : IComparable<T>
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeOfType(this object? actual, Type expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeOfType<T>(this object? actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotBeSameAs(this object? actual, object? expected, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }
    public static partial class ShouldMatchApprovedTestExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldMatchApproved(this string actual, Func<string?> customMessage, Action<ShouldMatchConfigurationBuilder> configureOptions)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldNotThrowTaskAsyncExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task ShouldNotThrowAsync(this Func<Task> actual, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task ShouldNotThrowAsync(this Task task, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldSatisfyAllConditionsTestExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldSatisfyAllConditions(this object? actual, Func<string?>? customMessage, params Action[] conditions)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldSatisfyAllConditions<T>(this T actual, Func<string?>? customMessage, params Action<T>[] conditions)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldThrowAsyncExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<Exception> ShouldThrowAsync(this Func<Task> actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<Exception> ShouldThrowAsync(this Task task, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<TException> ShouldThrowAsync<TException>(this Func<Task> actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Task<TException> ShouldThrowAsync<TException>(this Task task, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldThrowExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotThrow(this Action action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldNotThrow<T>(this Func<T> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception ShouldThrow(this Action actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception ShouldThrow(this Func<object?> actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException ShouldThrow<TException>(this Action actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException ShouldThrow<TException>(this Func<object?> actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }
    }

    public static partial class ShouldThrowTaskExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotThrow(this Func<Task> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotThrow(this Task action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotThrow(this Func<Task> action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotThrow(this Task action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldNotThrow<T>(this Task<T> action, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldNotThrow<T>(this Func<Task<T>> action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static T ShouldNotThrow<T>(this Task<T> action, TimeSpan timeoutAfter, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception ShouldThrow(this Func<Task> actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception ShouldThrow(this Task actual, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception ShouldThrow(this Func<Task> actual, TimeSpan timeoutAfter, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static Exception ShouldThrow(this Task actual, TimeSpan timeoutAfter, Func<string?>? customMessage, Type exceptionType)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException ShouldThrow<TException>(this Func<Task> actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException ShouldThrow<TException>(this Task actual, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException ShouldThrow<TException>(this Func<Task> actual, TimeSpan timeoutAfter, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static TException ShouldThrow<TException>(this Task actual, TimeSpan timeoutAfter, Func<string?>? customMessage)
            where TException : Exception
        {
            throw new NotImplementedException();
        }
    }
}

namespace Shouldly.ShouldlyExtensionMethods
{
    public static partial class ShouldHaveEnumExtensions
    {
        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldHaveFlag(this Enum actual, Enum expectedFlag, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }

        [Obsolete(ObsoleteMessages.FuncCustomMessage, true)]
        public static void ShouldNotHaveFlag(this Enum actual, Enum expectedFlag, Func<string?>? customMessage)
        {
            throw new NotImplementedException();
        }
    }
}