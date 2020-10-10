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
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void HaveProperty([System.Runtime.CompilerServices.Dynamic] object dynamicTestObject, string propertyName, System.Func<string?>? customMessage) { }
        public static void HaveProperty([System.Runtime.CompilerServices.Dynamic] object dynamicTestObject, string propertyName, string? customMessage = null) { }
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
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeEquivalentTo(this object? actual, object? expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeEquivalentTo([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class Should
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void CompleteIn(System.Action action, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static void CompleteIn(System.Action action, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void NotThrow(System.Action action, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Action action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void NotThrow(System.Threading.Tasks.Task action, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T NotThrow<T>(System.Func<T> action, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Func<T> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task NotThrowAsync(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task NotThrowAsync(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task NotThrowAsync(System.Threading.Tasks.Task task, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task NotThrowAsync(System.Threading.Tasks.Task task, string? customMessage = null) { }
        public static System.Exception Throw(System.Action actual, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<object?> actual, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception Throw(System.Action actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Action actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception Throw(System.Func<object?> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<object?> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException Throw<TException>(System.Action actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Action actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException Throw<TException>(System.Func<object?> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<object?> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Threading.Tasks.Task task, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Threading.Tasks.Task task, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Threading.Tasks.Task task, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Threading.Tasks.Task task, System.Func<string?>? customMessage)
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
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeFalse(this bool actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeFalse([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(true)] this bool actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeTrue(this bool actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeTrue([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(false)] this bool actual, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeDecoratedWithExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeDecoratedWith<T>(this System.Type actual, System.Func<string?>? customMessage)
            where T : System.Attribute { }
        public static void ShouldBeDecoratedWith<T>(this System.Type actual, string? customMessage = null)
            where T : System.Attribute { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeDictionaryTestExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string?>? customMessage) { }
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeEmpty<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, System.Func<string?>? customMessage) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, System.Collections.Generic.IComparer<T>? customComparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, System.Collections.Generic.IComparer<T>? customComparer, string? customMessage = null) { }
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, params System.Type[] expected) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Type[] expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Type[] expected, string? customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, string? customMessage = null) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Collections.Generic.IEqualityComparer<T> comparer, string customMessage) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, string? customMessage = null) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEqualityComparer<T> comparer, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string?>? customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, int expectedCount, System.Func<string?>? customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, int expectedCount, string? customMessage = null) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, string customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldHaveSingleItem<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Func<string?>? customMessage) { }
        public static T ShouldHaveSingleItem<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeEmpty<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string?>? customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string? customMessage = null) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, string customMessage) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeNullExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNull<T>(this T actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNull<T>([System.Diagnostics.CodeAnalysis.MaybeNull] this T? actual, string? customMessage = null)
            where T :  class { }
        public static void ShouldBeNull<T>([System.Diagnostics.CodeAnalysis.MaybeNull] this T? actual, string? customMessage = null)
            where T :  struct { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeNull<T>(this T actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
            where T :  class { }
        public static void ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
            where T :  struct { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeStringTestExtensions
    {
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, Shouldly.StringCompareShould options) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this string? actual, string? expected, System.Func<string?> customMessage) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, string customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this string? actual, string? expected, System.Func<string?> customMessage, Shouldly.StringCompareShould options) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, string? customMessage, Shouldly.StringCompareShould options) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNullOrEmpty(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNullOrEmpty(this string? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNullOrWhiteSpace(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNullOrWhiteSpace(this string? actual, string? customMessage = null) { }
        public static void ShouldContain(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContain(this string actual, string expected, System.Func<string?>? customMessage) { }
        public static void ShouldContain(this string actual, string expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContain(this string actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldContain(this string actual, string expected, string? customMessage, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldContainWithoutWhitespace(this string actual, object? expected, System.Func<string?>? customMessage) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object? expected, string? customMessage = null) { }
        public static void ShouldEndWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldEndWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldEndWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldMatch(this string actual, string regexPattern, System.Func<string?>? customMessage) { }
        public static void ShouldMatch(this string actual, string regexPattern, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeNullOrEmpty(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeNullOrWhiteSpace(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeNullOrWhiteSpace([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string? customMessage = null) { }
        public static void ShouldNotContain(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotContain(this string actual, string expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotContain(this string actual, string expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotContain(this string actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotContain(this string actual, string expected, string? customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotEndWith(this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotEndWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotEndWith(this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotMatch(this string actual, string regexPattern, System.Func<string?>? customMessage) { }
        public static void ShouldNotMatch(this string actual, string regexPattern, string? customMessage = null) { }
        public static void ShouldNotStartWith(this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotStartWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotStartWith(this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldStartWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeTestExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this double actual, double expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this double actual, double expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe(this float actual, float expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this float actual, float expected, double tolerance, string? customMessage = null) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this System.Collections.Generic.IEnumerable<T>? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder = false) { }
        public static void ShouldBe<T>(this T actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe<T>(this T actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.AllowNull] [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] T expected, string? customMessage = null) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Collections.Generic.IEqualityComparer<T> comparer, bool ignoreOrder = false) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder, System.Func<string?>? customMessage) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this System.Collections.Generic.IEnumerable<T>? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder, string? customMessage) { }
        public static void ShouldBe<T>(this T actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, System.Func<string> customMessage) { }
        public static void ShouldBe<T>(this T actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, string customMessage) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Collections.Generic.IEqualityComparer<T> comparer, bool ignoreOrder, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeAssignableTo(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeAssignableTo(this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldBeAssignableTo<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static T ShouldBeAssignableTo<T>(this object? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeGreaterThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeGreaterThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeGreaterThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeGreaterThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        public static void ShouldBeInRange<T>(this T actual, T from, T to)
            where T : System.IComparable<T> { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeInRange<T>(this T actual, T from, T to, System.Func<string?>? customMessage) { }
        public static void ShouldBeInRange<T>([System.Diagnostics.CodeAnalysis.DisallowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T from, [System.Diagnostics.CodeAnalysis.AllowNull] T to, string? customMessage)
            where T : System.IComparable<T> { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeLessThan<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeLessThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeLessThan<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeLessThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeLessThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeLessThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNegative(this decimal actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this decimal actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNegative(this double actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this double actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNegative(this float actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this float actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNegative(this int actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this int actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNegative(this long actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this long actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeNegative(this short actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this short actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeOfType(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeOfType([System.Diagnostics.CodeAnalysis.NotNull] this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldBeOfType<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static T ShouldBeOfType<T>([System.Diagnostics.CodeAnalysis.NotNull] this object? actual, string? customMessage = null) { }
        public static void ShouldBeOneOf<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, params T[] expected) { }
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeOneOf<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, T[] expected, string? customMessage) { }
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, System.Collections.Generic.IEqualityComparer<T> comparer, string customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBePositive(this decimal actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this decimal actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBePositive(this double actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this double actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBePositive(this float actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this float actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBePositive(this int actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this int actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBePositive(this long actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this long actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBePositive(this short actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this short actual, string? customMessage = null) { }
        public static void ShouldBeSameAs([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldBeSameAs(this object? actual, object? expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeSameAs([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string? customMessage = null) { }
        public static void ShouldNotBe<T>(this T actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBe<T>(this T actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null) { }
        public static void ShouldNotBe<T>(this T actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, System.Func<string> customMessage) { }
        public static void ShouldNotBe<T>(this T actual, T expected, System.Collections.Generic.IEqualityComparer<T> comparer, string customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeAssignableTo(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeAssignableTo(this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeAssignableTo<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeAssignableTo<T>(this object? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeInRange<T>(this T actual, T from, T to, System.Func<string?>? customMessage)
            where T : System.IComparable<T> { }
        public static void ShouldNotBeInRange<T>([System.Diagnostics.CodeAnalysis.DisallowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T from, [System.Diagnostics.CodeAnalysis.AllowNull] T to, string? customMessage = null)
            where T : System.IComparable<T> { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeOfType(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeOfType(this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeOfType<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeOfType<T>(this object? actual, string? customMessage = null) { }
        public static void ShouldNotBeOneOf<T>(this T actual, params T[] expected) { }
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, System.Collections.Generic.IEqualityComparer<T> comparer) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeOneOf<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, T[] expected, string? customMessage) { }
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, System.Collections.Generic.IEqualityComparer<T> comparer, string customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotBeSameAs(this object? actual, object? expected, System.Func<string?>? customMessage) { }
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
    public static class ShouldMatchApprovedTestExtensions
    {
        public static void ShouldMatchApproved(this string actual, System.Action<Shouldly.Configuration.ShouldMatchConfigurationBuilder> configureOptions) { }
        public static void ShouldMatchApproved(this string actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldMatchApproved(this string actual, System.Func<string?> customMessage, System.Action<Shouldly.Configuration.ShouldMatchConfigurationBuilder> configureOptions) { }
        public static void ShouldMatchApproved(this string actual, string? customMessage, System.Action<Shouldly.Configuration.ShouldMatchConfigurationBuilder> configureOptions) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldNotThrowTaskAsyncExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Threading.Tasks.Task task, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Threading.Tasks.Task task, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions(this object? actual, params System.Action[] conditions) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldSatisfyAllConditions(this object? actual, System.Func<string?>? customMessage, params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions(this object? actual, string? customMessage, params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions<T>(this T actual, params System.Action<>[] conditions) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldSatisfyAllConditions<T>(this T actual, System.Func<string?>? customMessage, params System.Action<>[] conditions) { }
        public static void ShouldSatisfyAllConditions<T>(this T actual, string? customMessage, params System.Action<>[] conditions) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowAsyncExtensions
    {
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Threading.Tasks.Task task, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Threading.Tasks.Task task, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Threading.Tasks.Task task, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task, string? customMessage = null)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotThrow(this System.Action action, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Action action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Func<T> action, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<T> action, string? customMessage = null) { }
        public static System.Exception ShouldThrow(this System.Action actual, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<object?> actual, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception ShouldThrow(this System.Action actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Action actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception ShouldThrow(this System.Func<object?> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<object?> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Action actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Action actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Func<object?> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<object?> actual, string? customMessage = null)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowTaskExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
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
        public static Shouldly.Configuration.ShouldMatchConfigurationBuilder ShouldMatchApprovedDefaults { get; }
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
    public class ShouldMatchConfiguration
    {
        public ShouldMatchConfiguration() { }
        public ShouldMatchConfiguration(Shouldly.Configuration.ShouldMatchConfiguration initialConfig) { }
        public string? ApprovalFileSubFolder { get; set; }
        public string FileExtension { get; set; }
        public string? FilenameDiscriminator { get; set; }
        public Shouldly.Configuration.FilenameGenerator FilenameGenerator { get; set; }
        public bool PreventDiff { get; set; }
        public System.Func<string, string>? Scrubber { get; set; }
        public Shouldly.StringCompareShould StringCompareOptions { get; set; }
        public Shouldly.Configuration.ITestMethodFinder TestMethodFinder { get; set; }
    }
    public class ShouldMatchConfigurationBuilder
    {
        public ShouldMatchConfigurationBuilder(Shouldly.Configuration.ShouldMatchConfiguration initialConfig) { }
        public Shouldly.Configuration.ShouldMatchConfiguration Build() { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder Configure(System.Action<Shouldly.Configuration.ShouldMatchConfiguration> configure) { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder DoNotIgnoreLineEndings() { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder LocateTestMethodUsingAttribute<T>()
            where T : System.Attribute { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder NoDiff() { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder SubFolder(string subfolder) { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder UseCallerLocation() { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder WithDiscriminator(string fileDiscriminator) { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder WithFileExtension(string fileExtension) { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder WithFilenameGenerator(Shouldly.Configuration.FilenameGenerator filenameGenerator) { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder WithScrubber(System.Func<string, string> scrubber) { }
        public Shouldly.Configuration.ShouldMatchConfigurationBuilder WithStringCompareOptions(Shouldly.StringCompareShould stringCompareOptions) { }
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
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldHaveFlag(this System.Enum actual, System.Enum expectedFlag, System.Func<string?>? customMessage) { }
        public static void ShouldHaveFlag(this System.Enum actual, System.Enum expectedFlag, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "stomMessage.", true)]
        public static void ShouldNotHaveFlag(this System.Enum actual, System.Enum expectedFlag, System.Func<string?>? customMessage) { }
        public static void ShouldNotHaveFlag(this System.Enum actual, System.Enum expectedFlag, string? customMessage = null) { }
    }
}