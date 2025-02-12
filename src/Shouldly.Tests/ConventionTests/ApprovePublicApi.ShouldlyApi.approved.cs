namespace Shouldly
{
    public class ActualFilteredWithPredicateShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ActualFilteredWithPredicateShouldlyMessage(System.Linq.Expressions.Expression filter, object? result, object? actual, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ActualShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ActualShouldlyMessage(object? actual, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class AsyncShouldlyNotThrowShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public AsyncShouldlyNotThrowShouldlyMessage(System.Type exception, string? customMessage, System.Diagnostics.StackTrace stackTrace, string exceptionMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class AsyncShouldlyThrowShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public AsyncShouldlyThrowShouldlyMessage(System.Type exception, string? customMessage, System.Diagnostics.StackTrace stackTrace, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public AsyncShouldlyThrowShouldlyMessage(System.Type expected, System.Type actual, string? customMessage, System.Diagnostics.StackTrace stackTrace) { }
    }
    public enum Case
    {
        Sensitive = 0,
        Insensitive = 1,
    }
    public class CompleteInShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public CompleteInShouldlyMessage(string what, System.TimeSpan timeout, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class DynamicShould
    {
        public static void HaveProperty([System.Runtime.CompilerServices.Dynamic] object dynamicTestObject, string propertyName, string? customMessage = null) { }
        public static TException Throw<TException>(System.Action actual, string? customMessage = null)
            where TException : System.Exception { }
    }
    public class ExpectedActualIgnoreOrderShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedActualIgnoreOrderShouldlyMessage(object? expected, object? actual, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ExpectedActualKeyShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedActualKeyShouldlyMessage(object? expected, object? actual, object key, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ExpectedActualShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedActualShouldlyMessage(object? expected, object? actual, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ExpectedActualToleranceShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedActualToleranceShouldlyMessage(object? expected, object? actual, object tolerance, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ExpectedActualWithCaseSensitivityShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedActualWithCaseSensitivityShouldlyMessage(object? expected, object? actual, Shouldly.Case? caseSensitivity, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ExpectedEquivalenceShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedEquivalenceShouldlyMessage(object? expected, object? actual, System.Collections.Generic.IEnumerable<string> path, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ExpectedOrderShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedOrderShouldlyMessage(object? actual, Shouldly.SortDirection expectedDirection, int outOfOrderIndex, object? outOfOrderObject, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ExpectedShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ExpectedShouldlyMessage(object? expected, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class GuidShouldBeTestExtensions
    {
        public static void ShouldBeEmpty(this System.Guid actual, string? customMessage = null) { }
    }
    public interface IShouldlyAssertionContext
    {
        object? Actual { get; set; }
        Shouldly.Case? CaseSensitivity { get; set; }
        string? CodePart { get; set; }
        bool CodePartMatchesActual { get; }
        string? CustomMessage { get; set; }
        object? Expected { get; set; }
        string? FileName { get; set; }
        System.Linq.Expressions.Expression? Filter { get; set; }
        bool HasRelevantActual { get; set; }
        bool HasRelevantKey { get; set; }
        bool IgnoreOrder { get; set; }
        bool IsNegatedAssertion { get; }
        object? Key { get; set; }
        int? LineNumber { get; set; }
        int? MatchCount { get; set; }
        int OutOfOrderIndex { get; set; }
        object? OutOfOrderObject { get; set; }
        System.Collections.Generic.IEnumerable<string>? Path { get; set; }
        string ShouldMethod { get; set; }
        Shouldly.SortDirection SortDirection { get; set; }
        System.TimeSpan? Timeout { get; set; }
        object? Tolerance { get; set; }
    }
    [Shouldly.ShouldlyMethods]
    public static class ObjectGraphTestExtensions
    {
        public static void ShouldBeEquivalentTo([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class Should
    {
        public static void CompleteIn(System.Action action, System.TimeSpan timeout, string? customMessage = null) { }
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout, string? customMessage = null) { }
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout, string? customMessage = null) { }
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout, string? customMessage = null) { }
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout, string? customMessage = null) { }
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout, string? customMessage = null) { }
        public static void NotThrow(System.Action action, string? customMessage = null) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, string? customMessage = null) { }
        public static void NotThrow(System.Threading.Tasks.Task action, string? customMessage = null) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, string? customMessage = null) { }
        public static T NotThrow<T>(System.Func<T> action, string? customMessage = null) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, string? customMessage = null) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static System.Threading.Tasks.Task NotThrowAsync(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null) { }
        public static System.Threading.Tasks.Task NotThrowAsync(System.Threading.Tasks.Task task, string? customMessage = null) { }
        public static System.Exception Throw(System.Func<object?> actual, System.Type exceptionType) { }
        public static System.Exception Throw(System.Action actual, System.Type exceptionType, string? customMessage = null) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType, string? customMessage = null) { }
        public static System.Exception Throw(System.Func<object?> actual, string? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.Type exceptionType, string? customMessage = null) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Type exceptionType, string? customMessage = null) { }
        public static TException Throw<TException>(System.Action actual, string? customMessage = null)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<object?> actual, string? customMessage = null)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, string? customMessage = null)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType, string? customMessage = null) { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Threading.Tasks.Task task, System.Type exceptionType, string? customMessage = null) { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Threading.Tasks.Task task, string? customMessage = null)
            where TException : System.Exception { }
    }
    [System.Serializable]
    public class ShouldAssertException : System.Exception
    {
        public ShouldAssertException(string? message) { }
        public ShouldAssertException(string? message, System.Exception? innerException) { }
        public override string StackTrace { get; }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeBooleanExtensions
    {
        public static void ShouldBeFalse([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(true)] this bool actual, string? customMessage = null) { }
        public static void ShouldBeFalse([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(true)] this bool? actual, string? customMessage = null) { }
        public static void ShouldBeTrue([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(false)] this bool actual, string? customMessage = null) { }
        public static void ShouldBeTrue([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(false)] this bool? actual, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeDecoratedWithExtensions
    {
        public static void ShouldBeDecoratedWith<T>(this System.Type actual, string? customMessage = null)
            where T : System.Attribute { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeDictionaryTestExtensions
    {
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        [System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
        [System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        [System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
        [System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity, string? customMessage = null) { }
        public static void ShouldBeEmpty<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, string? customMessage = null) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, string? customMessage = null) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, System.Collections.Generic.IComparer<T>? customComparer, string? customMessage = null) { }
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, params System.Type[] expected) { }
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Type[] expected, string? customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, string? customMessage = null) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, string? customMessage = null) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, string? customMessage = null) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, string? customMessage = null) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string? customMessage = null) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, int expectedCount, string? customMessage = null) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage = null) { }
        public static T ShouldHaveSingleItem<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        public static void ShouldNotBeEmpty<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string? customMessage = null) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeNullExtensions
    {
        public static void ShouldBeNull<T>(this T? actual, string? customMessage = null)
            where T :  class { }
        public static void ShouldBeNull<T>(this T? actual, string? customMessage = null)
            where T :  struct { }
        public static T ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
            where T :  class { }
        public static T ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
            where T :  struct { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeStringTestExtensions
    {
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, Shouldly.StringCompareShould options) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, string? customMessage = null) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, string? customMessage, Shouldly.StringCompareShould options) { }
        public static void ShouldBeNullOrEmpty(this string? actual, string? customMessage = null) { }
        public static void ShouldBeNullOrWhiteSpace(this string? actual, string? customMessage = null) { }
        public static void ShouldContain(this string actual, string expected, Shouldly.Case caseSensitivity = 1, string? customMessage = null) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object? expected, string? customMessage = null) { }
        public static void ShouldEndWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, Shouldly.Case caseSensitivity = 1, string? customMessage = null) { }
        public static void ShouldMatch(this string actual, string regexPattern, string? customMessage = null) { }
        public static void ShouldNotBeNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string? customMessage = null) { }
        public static void ShouldNotBeNullOrWhiteSpace([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string? customMessage = null) { }
        public static void ShouldNotContain(this string actual, string expected, Shouldly.Case caseSensitivity = 1, string? customMessage = null) { }
        public static void ShouldNotEndWith(this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotEndWith(this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotMatch(this string actual, string regexPattern, string? customMessage = null) { }
        public static void ShouldNotStartWith(this string? actual, string expected, Shouldly.Case caseSensitivity = 1, string? customMessage = null) { }
        public static void ShouldStartWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, Shouldly.Case caseSensitivity = 1, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeTestExtensions
    {
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, string? customMessage = null) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, string? customMessage = null) { }
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string? customMessage = null) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string? customMessage = null) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string? customMessage = null) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, string? customMessage = null) { }
        public static void ShouldBe(this double actual, double expected, double tolerance, string? customMessage = null) { }
        public static void ShouldBe(this float actual, float expected, double tolerance, string? customMessage = null) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this System.Collections.Generic.IEnumerable<T>? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder = false) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this T? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] T? expected, string? customMessage = null) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this System.Collections.Generic.IEnumerable<T>? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder, string? customMessage) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this T? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] T? expected, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this System.Collections.Generic.IEnumerable<T>? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] System.Collections.Generic.IEnumerable<T>? expected, System.Collections.Generic.IEqualityComparer<T> comparer, bool ignoreOrder = false, string? customMessage = null) { }
        public static void ShouldBeAssignableTo(this object? actual, System.Type expected, string? customMessage = null) { }
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")]
        public static T? ShouldBeAssignableTo<T>(this object? actual, string? customMessage = null) { }
        public static void ShouldBeGreaterThan<T>(this T? actual, T? expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        public static void ShouldBeGreaterThan<T>(this T? actual, T? expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T? actual, T? expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T? actual, T? expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBeInRange<T>([System.Diagnostics.CodeAnalysis.DisallowNull] this T actual, T? from, T? to, string? customMessage = null)
            where T : System.IComparable<T> { }
        public static void ShouldBeLessThan<T>(this T? actual, T? expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        public static void ShouldBeLessThan<T>(this T? actual, T? expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T? actual, T? expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T? actual, T? expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBeNegative(this decimal actual, string? customMessage = null) { }
        public static void ShouldBeNegative(this double actual, string? customMessage = null) { }
        public static void ShouldBeNegative(this float actual, string? customMessage = null) { }
        public static void ShouldBeNegative(this int actual, string? customMessage = null) { }
        public static void ShouldBeNegative(this long actual, string? customMessage = null) { }
        public static void ShouldBeNegative(this short actual, string? customMessage = null) { }
        public static void ShouldBeOfType([System.Diagnostics.CodeAnalysis.NotNull] this object? actual, System.Type expected, string? customMessage = null) { }
        public static T ShouldBeOfType<T>([System.Diagnostics.CodeAnalysis.NotNull] this object? actual, string? customMessage = null) { }
        public static void ShouldBeOneOf<T>(this T? actual, params T[] expected) { }
        public static void ShouldBeOneOf<T>(this T? actual, T[] expected, string? customMessage) { }
        public static void ShouldBeOneOf<T>(this T? actual, T[] expected, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBePositive(this decimal actual, string? customMessage = null) { }
        public static void ShouldBePositive(this double actual, string? customMessage = null) { }
        public static void ShouldBePositive(this float actual, string? customMessage = null) { }
        public static void ShouldBePositive(this int actual, string? customMessage = null) { }
        public static void ShouldBePositive(this long actual, string? customMessage = null) { }
        public static void ShouldBePositive(this short actual, string? customMessage = null) { }
        public static void ShouldBeSameAs([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected, string? customMessage = null) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string? customMessage = null) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string? customMessage = null) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string? customMessage = null) { }
        public static void ShouldNotBe<T>(this T? actual, T? expected, string? customMessage = null) { }
        public static void ShouldNotBe<T>(this T? actual, T? expected, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldNotBeAssignableTo(this object? actual, System.Type expected, string? customMessage = null) { }
        public static void ShouldNotBeAssignableTo<T>(this object? actual, string? customMessage = null) { }
        public static void ShouldNotBeInRange<T>([System.Diagnostics.CodeAnalysis.DisallowNull] this T actual, T? from, T? to, string? customMessage = null)
            where T : System.IComparable<T> { }
        public static void ShouldNotBeOfType(this object? actual, System.Type expected, string? customMessage = null) { }
        public static void ShouldNotBeOfType<T>(this object? actual, string? customMessage = null) { }
        public static void ShouldNotBeOneOf<T>(this T? actual, params T[] expected) { }
        public static void ShouldNotBeOneOf<T>(this T? actual, T[] expected, string? customMessage) { }
        public static void ShouldNotBeOneOf<T>(this T? actual, T[] expected, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null) { }
    }
    public class ShouldCompleteInException : Shouldly.ShouldlyTimeoutException
    {
        public ShouldCompleteInException(string? message, Shouldly.ShouldlyTimeoutException? inner) { }
    }
    public class ShouldContainWithCountShouldlyMessage : Shouldly.ShouldlyMessage
    {
        public ShouldContainWithCountShouldlyMessage(object? expected, object? actual, int matchCount, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ShouldMatchApprovedException : Shouldly.ShouldAssertException
    {
        public ShouldMatchApprovedException(string? message, string? receivedFile, string? approvedFile) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldNotThrowTaskAsyncExtensions
    {
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null) { }
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Threading.Tasks.Task task, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions(this object? actual, params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions(this object? actual, string? customMessage, params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions<T>(this T actual, params System.Action<T>[] conditions) { }
        public static void ShouldSatisfyAllConditions<T>(this T actual, string? customMessage, params System.Action<T>[] conditions) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowAsyncExtensions
    {
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType, string? customMessage = null) { }
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Threading.Tasks.Task task, System.Type exceptionType, string? customMessage = null) { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task, string? customMessage = null)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowExtensions
    {
        public static void ShouldNotThrow(this System.Action action, string? customMessage = null) { }
        public static T ShouldNotThrow<T>(this System.Func<T> action, string? customMessage = null) { }
        public static System.Exception ShouldThrow(this System.Action actual, System.Type exceptionType, string? customMessage = null) { }
        public static System.Exception ShouldThrow(this System.Func<object?> actual, System.Type exceptionType, string? customMessage = null) { }
        public static TException ShouldThrow<TException>(this System.Action actual, string? customMessage = null)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<object?> actual, string? customMessage = null)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowTaskExtensions
    {
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, string? customMessage = null) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, string? customMessage = null) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, string? customMessage = null) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, string? customMessage = null) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, string? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, string? customMessage = null)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
    }
    public class ShouldlyAssertionContext : Shouldly.IShouldlyAssertionContext
    {
        public ShouldlyAssertionContext(string shouldlyMethod, object? expected = null, object? actual = null, System.Diagnostics.StackTrace? stackTrace = null) { }
        public object? Actual { get; set; }
        public Shouldly.Case? CaseSensitivity { get; set; }
        public string? CodePart { get; set; }
        public bool CodePartMatchesActual { get; }
        public string? CustomMessage { get; set; }
        public object? Expected { get; set; }
        public string? FileName { get; set; }
        public System.Linq.Expressions.Expression? Filter { get; set; }
        public bool HasRelevantActual { get; set; }
        public bool HasRelevantKey { get; set; }
        public bool IgnoreOrder { get; set; }
        public bool IsNegatedAssertion { get; }
        public object? Key { get; set; }
        public int? LineNumber { get; set; }
        public int? MatchCount { get; set; }
        public int OutOfOrderIndex { get; set; }
        public object? OutOfOrderObject { get; set; }
        public System.Collections.Generic.IEnumerable<string>? Path { get; set; }
        public string ShouldMethod { get; set; }
        public Shouldly.SortDirection SortDirection { get; set; }
        public System.TimeSpan? Timeout { get; set; }
        public object? Tolerance { get; set; }
    }
    public static class ShouldlyConfiguration
    {
        public static double DefaultFloatingPointTolerance;
        public static System.TimeSpan DefaultTaskTimeout;
        public static System.Collections.Generic.List<string> CompareAsObjectTypes { get; }
        public static System.IDisposable DisableSourceInErrors() { }
        public static bool IsSourceDisabledInErrors() { }
    }
    public static class ShouldlyCoreExtensions
    {
        public static void AssertAwesomely<T>(this T actual, System.Func<T, bool> specifiedConstraint, object? originalActual, object? originalExpected, string? customMessage = null, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public static void AssertAwesomely<T>(this T actual, System.Func<T, bool> specifiedConstraint, object? originalActual, object? originalExpected, Shouldly.Case caseSensitivity, string? customMessage = null, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public static void AssertAwesomely<T>(this T actual, System.Func<T, bool> specifiedConstraint, object? originalActual, object? originalExpected, object tolerance, string? customMessage = null, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public static void AssertAwesomelyIgnoringOrder<T>(this T actual, System.Func<T, bool> specifiedConstraint, object? originalActual, object? originalExpected, string? customMessage = null, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public static void AssertAwesomelyWithCaseSensitivity<T>(this T actual, System.Func<T, bool> specifiedConstraint, object? originalActual, object? originalExpected, Shouldly.Case caseSensitivity, string? customMessage = null, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public abstract class ShouldlyMessage
    {
        protected ShouldlyMessage() { }
        protected Shouldly.IShouldlyAssertionContext ShouldlyAssertionContext { get; set; }
        public override string ToString() { }
    }
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited=false)]
    public class ShouldlyMethodsAttribute : System.Attribute
    {
        public ShouldlyMethodsAttribute() { }
    }
    public class ShouldlyThrowMessage : Shouldly.ShouldlyMessage
    {
        public ShouldlyThrowMessage(object? expected, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public ShouldlyThrowMessage(object? expected, string exceptionMessage, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public ShouldlyThrowMessage(object? expected, object? actual, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
    public class ShouldlyTimeoutException : System.TimeoutException
    {
        public ShouldlyTimeoutException() { }
        public ShouldlyTimeoutException(string? message, Shouldly.ShouldlyTimeoutException? inner) { }
        public override string StackTrace { get; }
    }
    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1,
    }
    [System.Flags]
    public enum StringCompareShould
    {
        IgnoreCase = 1,
        IgnoreLineEndings = 2,
    }
    public class TaskShouldlyThrowMessage : Shouldly.ShouldlyMessage
    {
        public TaskShouldlyThrowMessage(object? expected, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public TaskShouldlyThrowMessage(object? expected, System.Exception exception, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
        public TaskShouldlyThrowMessage(object? expected, object? actual, string? customMessage, [System.Runtime.CompilerServices.CallerMemberName] string shouldlyMethod = null) { }
    }
}
namespace Shouldly.Configuration
{
    public delegate string FilenameGenerator(Shouldly.Configuration.TestMethodInfo testMethodInfo, string? discriminator, string fileType, string fileExtension);
    public class FindMethodUsingAttribute<T> : Shouldly.Configuration.ITestMethodFinder
        where T : System.Attribute
    {
        public FindMethodUsingAttribute() { }
        public Shouldly.Configuration.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0) { }
    }
    public class FirstNonShouldlyMethodFinder : Shouldly.Configuration.ITestMethodFinder
    {
        public FirstNonShouldlyMethodFinder() { }
        public int Offset { get; set; }
        public Shouldly.Configuration.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0) { }
    }
    public interface ITestMethodFinder
    {
        Shouldly.Configuration.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0);
    }
    public class TestMethodInfo
    {
        public TestMethodInfo(System.Diagnostics.StackFrame callingFrame) { }
        public string? DeclaringTypeName { get; }
        public string? MethodName { get; }
        public string? SourceFileDirectory { get; }
    }
}
namespace Shouldly.ShouldlyExtensionMethods
{
    [Shouldly.ShouldlyMethods]
    public static class ShouldHaveEnumExtensions
    {
        public static void ShouldHaveFlag(this System.Enum actual, System.Enum expectedFlag, string? customMessage = null) { }
        public static void ShouldNotHaveFlag(this System.Enum actual, System.Enum expectedFlag, string? customMessage = null) { }
    }
}