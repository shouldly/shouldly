namespace Shouldly
{
    public enum Case
    {
        Sensitive = 0,
        Insensitive = 1,
    }
    [Shouldly.ShouldlyMethods]
    public static class DynamicShould
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void HaveProperty([System.Runtime.CompilerServices.Dynamic] object dynamicTestObject, string propertyName, System.Func<string?>? customMessage) { }
        public static void HaveProperty([System.Runtime.CompilerServices.Dynamic] object dynamicTestObject, string propertyName, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ObjectGraphTestExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeEquivalentTo(this object? actual, object? expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeEquivalentTo([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class Should
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void CompleteIn(System.Action action, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static void CompleteIn(System.Action action, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout, System.Func<string?>? customMessage) { }
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void NotThrow(System.Action action, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Action action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void NotThrow(System.Threading.Tasks.Task action, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T NotThrow<T>(System.Func<T> action, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Func<T> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task NotThrowAsync(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task NotThrowAsync(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task NotThrowAsync(System.Threading.Tasks.Task task, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task NotThrowAsync(System.Threading.Tasks.Task task, string? customMessage = null) { }
        public static System.Exception Throw(System.Action actual, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<object?> actual, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception Throw(System.Action actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Action actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception Throw(System.Func<object?> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<object?> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception Throw(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException Throw<TException>(System.Action actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Action actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException Throw<TException>(System.Func<object?> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<object?> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Threading.Tasks.Task task, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Threading.Tasks.Task task, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ThrowAsync(System.Threading.Tasks.Task task, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
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
            "somMessage.", true)]
        public static void ShouldBeFalse(this bool actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeFalse([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(true)] this bool actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeTrue(this bool actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeTrue([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(false)] this bool actual, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeDecoratedWithExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeDecoratedWith<T>(this System.Type actual, System.Func<string?>? customMessage)
            where T : System.Attribute { }
        public static void ShouldBeDecoratedWith<T>(this System.Type actual, string? customMessage = null)
            where T : System.Attribute { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeDictionaryTestExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string? customMessage = null)
            where TKey :  notnull { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, System.Func<string?>? customMessage)
            where TKey :  notnull { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string? customMessage = null)
            where TKey :  notnull { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string?>? customMessage) { }
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeEmpty<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, System.Func<string?>? customMessage) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, System.Collections.Generic.IComparer<T>? customComparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeInOrder<T>(this System.Collections.Generic.IEnumerable<T> actual, Shouldly.SortDirection expectedSortDirection, System.Collections.Generic.IComparer<T>? customComparer, string? customMessage = null) { }
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, params System.Type[] expected) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Type[] expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeOfTypes<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Type[] expected, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string?>? customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, int expectedCount, System.Func<string?>? customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, int expectedCount, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldHaveSingleItem<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Func<string?>? customMessage) { }
        public static T ShouldHaveSingleItem<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeEmpty<T>([System.Diagnostics.CodeAnalysis.NotNull] this System.Collections.Generic.IEnumerable<T>? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string?>? customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeNullExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNull<T>(this T? actual, System.Func<string?>? customMessage)
            where T :  class { }
        public static void ShouldBeNull<T>([System.Diagnostics.CodeAnalysis.MaybeNull] this T? actual, string? customMessage = null)
            where T :  class { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeNull<T>(this T? actual, System.Func<string?>? customMessage)
            where T :  class { }
        public static void ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
            where T :  class { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeStringTestExtensions
    {
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, Shouldly.StringCompareShould options) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this string? actual, string? expected, System.Func<string?> customMessage) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, string customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this string? actual, string? expected, System.Func<string?> customMessage, Shouldly.StringCompareShould options) { }
        public static void ShouldBe([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this string? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] string? expected, string? customMessage, Shouldly.StringCompareShould options) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNullOrEmpty(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNullOrEmpty(this string? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNullOrWhiteSpace(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNullOrWhiteSpace(this string? actual, string? customMessage = null) { }
        public static void ShouldContain(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContain(this string actual, string expected, System.Func<string?>? customMessage) { }
        public static void ShouldContain(this string actual, string expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContain(this string actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldContain(this string actual, string expected, string? customMessage, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldContainWithoutWhitespace(this string actual, object? expected, System.Func<string?>? customMessage) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object? expected, string? customMessage = null) { }
        public static void ShouldEndWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldEndWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldEndWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldMatch(this string actual, string regexPattern, System.Func<string?>? customMessage) { }
        public static void ShouldMatch(this string actual, string regexPattern, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeNullOrEmpty(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeNullOrWhiteSpace(this string? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeNullOrWhiteSpace([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string? customMessage = null) { }
        public static void ShouldNotContain(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotContain(this string actual, string expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotContain(this string actual, string expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotContain(this string actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotContain(this string actual, string expected, string? customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotEndWith(this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotEndWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotEndWith(this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotMatch(this string actual, string regexPattern, System.Func<string?>? customMessage) { }
        public static void ShouldNotMatch(this string actual, string regexPattern, string? customMessage = null) { }
        public static void ShouldNotStartWith(this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotStartWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotStartWith(this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldStartWith(this string? actual, string expected, System.Func<string?>? customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith([System.Diagnostics.CodeAnalysis.NotNull] this string? actual, string expected, string? customMessage = null, Shouldly.Case caseSensitivity = 1) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldBeTestExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this double actual, double expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this double actual, double expected, double tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe(this float actual, float expected, double tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldBe(this float actual, float expected, double tolerance, string? customMessage = null) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this System.Collections.Generic.IEnumerable<T>? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder = false) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe<T>(this T actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.AllowNull] [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] T expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T>? actual, System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder, System.Func<string?>? customMessage) { }
        public static void ShouldBe<T>([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this System.Collections.Generic.IEnumerable<T>? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] System.Collections.Generic.IEnumerable<T>? expected, bool ignoreOrder, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeAssignableTo(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeAssignableTo(this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldBeAssignableTo<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static T ShouldBeAssignableTo<T>(this object? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeGreaterThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeGreaterThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeGreaterThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeGreaterThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeInRange<T>(this T actual, T from, T to, System.Func<string?>? customMessage) { }
        public static void ShouldBeInRange<T>([System.Diagnostics.CodeAnalysis.DisallowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T from, [System.Diagnostics.CodeAnalysis.AllowNull] T to, string? customMessage = null)
            where T : System.IComparable<T> { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeLessThan<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeLessThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeLessThan<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeLessThan<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, System.Func<string?>? customMessage)
            where T : System.IComparable<T>? { }
        public static void ShouldBeLessThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null)
            where T : System.IComparable<T>? { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, System.Collections.Generic.IComparer<T> comparer, System.Func<string?>? customMessage) { }
        public static void ShouldBeLessThanOrEqualTo<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, System.Collections.Generic.IComparer<T> comparer, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNegative(this decimal actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this decimal actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNegative(this double actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this double actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNegative(this float actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this float actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNegative(this int actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this int actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNegative(this long actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this long actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeNegative(this short actual, System.Func<string?>? customMessage) { }
        public static void ShouldBeNegative(this short actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeOfType(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeOfType([System.Diagnostics.CodeAnalysis.NotNull] this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldBeOfType<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static T ShouldBeOfType<T>([System.Diagnostics.CodeAnalysis.NotNull] this object? actual, string? customMessage = null) { }
        public static void ShouldBeOneOf<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, params T[] expected) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeOneOf<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, T[] expected, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBePositive(this decimal actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this decimal actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBePositive(this double actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this double actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBePositive(this float actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this float actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBePositive(this int actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this int actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBePositive(this long actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this long actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBePositive(this short actual, System.Func<string?>? customMessage) { }
        public static void ShouldBePositive(this short actual, string? customMessage = null) { }
        public static void ShouldBeSameAs([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldBeSameAs(this object? actual, object? expected, System.Func<string?>? customMessage) { }
        public static void ShouldBeSameAs([System.Diagnostics.CodeAnalysis.NotNullIfNotNull("expected")] this object? actual, [System.Diagnostics.CodeAnalysis.NotNullIfNotNull("actual")] object? expected, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBe<T>(this T actual, T expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBe<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeAssignableTo(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeAssignableTo(this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeAssignableTo<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeAssignableTo<T>(this object? actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeInRange<T>(this T actual, T from, T to, System.Func<string?>? customMessage)
            where T : System.IComparable<T> { }
        public static void ShouldNotBeInRange<T>([System.Diagnostics.CodeAnalysis.DisallowNull] this T actual, [System.Diagnostics.CodeAnalysis.AllowNull] T from, [System.Diagnostics.CodeAnalysis.AllowNull] T to, string? customMessage = null)
            where T : System.IComparable<T> { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeOfType(this object? actual, System.Type expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeOfType(this object? actual, System.Type expected, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeOfType<T>(this object? actual, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeOfType<T>(this object? actual, string? customMessage = null) { }
        public static void ShouldNotBeOneOf<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, params T[] expected) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeOneOf<T>([System.Diagnostics.CodeAnalysis.AllowNull] this T actual, T[] expected, string? customMessage) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotBeSameAs(this object? actual, object? expected, System.Func<string?>? customMessage) { }
        public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null) { }
    }
    public class ShouldCompleteInException : Shouldly.ShouldlyTimeoutException
    {
        public ShouldCompleteInException(string? message, Shouldly.ShouldlyTimeoutException? inner) { }
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
            "somMessage.", true)]
        public static void ShouldMatchApproved(this string actual, System.Func<string?> customMessage, System.Action<Shouldly.Configuration.ShouldMatchConfigurationBuilder> configureOptions) { }
        public static void ShouldMatchApproved(this string actual, string? customMessage, System.Action<Shouldly.Configuration.ShouldMatchConfigurationBuilder> configureOptions) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldNotThrowTaskAsyncExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Threading.Tasks.Task task, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task ShouldNotThrowAsync(this System.Threading.Tasks.Task task, string? customMessage = null) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions(this object? actual, params System.Action[] conditions) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldSatisfyAllConditions(this object? actual, System.Func<string?>? customMessage, params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions(this object? actual, string? customMessage, params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions<T>(this T actual, params System.Action<>[] conditions) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldSatisfyAllConditions<T>(this T actual, System.Func<string?>? customMessage, params System.Action<>[] conditions) { }
        public static void ShouldSatisfyAllConditions<T>(this T actual, string? customMessage, params System.Action<>[] conditions) { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowAsyncExtensions
    {
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Threading.Tasks.Task task, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Threading.Tasks.Task task, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Threading.Tasks.Task<System.Exception> ShouldThrowAsync(this System.Threading.Tasks.Task task, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task, System.Func<string?>? customMessage) { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task, string? customMessage = null)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotThrow(this System.Action action, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Action action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Func<T> action, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<T> action, string? customMessage = null) { }
        public static System.Exception ShouldThrow(this System.Action actual, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<object?> actual, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception ShouldThrow(this System.Action actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Action actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception ShouldThrow(this System.Func<object?> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<object?> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Action actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Action actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Func<object?> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<object?> actual, string? customMessage = null)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethods]
    public static class ShouldThrowTaskExtensions
    {
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string? customMessage = null) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage, System.Type exceptionType) { }
        public static System.Exception ShouldThrow(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage, System.Type exceptionType) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, System.Func<string?>? customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string? customMessage = null)
            where TException : System.Exception { }
    }
    public static class ShouldlyConfiguration
    {
        public static double DefaultFloatingPointTolerance;
        public static System.TimeSpan DefaultTaskTimeout;
        public static System.Collections.Generic.List<string> CompareAsObjectTypes { get; }
        public static Shouldly.Configuration.DiffToolConfiguration DiffTools { get; }
        public static Shouldly.Configuration.ShouldMatchConfigurationBuilder ShouldMatchApprovedDefaults { get; }
        public static System.IDisposable DisableSourceInErrors() { }
        public static bool IsSourceDisabledInErrors() { }
    }
    public class ShouldlyMethodsAttribute : System.Attribute
    {
        public ShouldlyMethodsAttribute() { }
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
}
namespace Shouldly.Configuration
{
    public class DiffTool
    {
        public DiffTool(string name, Shouldly.Configuration.DiffToolConfig config, Shouldly.Configuration.DiffTool.ArgumentGenerator argGenerator) { }
        public string Name { get; }
        public bool Exists() { }
        public void Open(string receivedPath, string approvedPath) { }
        protected static void CreateEmptyFileIfNotExists(string path) { }
        public delegate string ArgumentGenerator(string received, string approved);
    }
    public class DiffToolConfig
    {
        public DiffToolConfig() { }
        public string? LinuxPath { get; set; }
        public string? MacPath { get; set; }
        public string? WindowsPath { get; set; }
        public string? ResolvePath() { }
    }
    public class DiffToolConfiguration
    {
        public DiffToolConfiguration() { }
        public Shouldly.Configuration.KnownDiffTools KnownDiffTools { get; }
        public Shouldly.Configuration.KnownDoNotLaunchStrategies KnownDoNotLaunchStrategies { get; }
        public void AddDoNotLaunchStrategy(Shouldly.Configuration.IShouldNotLaunchDiffTool shouldNotlaunchStrategy) { }
        public Shouldly.Configuration.DiffTool GetDiffTool() { }
        public void RegisterDiffTool(Shouldly.Configuration.DiffTool diffTool) { }
        public void SetDiffToolPriorities(params Shouldly.Configuration.DiffTool[] diffTools) { }
        public bool ShouldOpenDiffTool() { }
    }
    public class DoNotLaunchWhenEnvVariableIsPresent : Shouldly.Configuration.IShouldNotLaunchDiffTool
    {
        public DoNotLaunchWhenEnvVariableIsPresent(string environmentalVariable) { }
        public bool ShouldNotLaunch() { }
    }
    public class DoNotLaunchWhenPlatformIsNotWindows : Shouldly.Configuration.IShouldNotLaunchDiffTool
    {
        public DoNotLaunchWhenPlatformIsNotWindows() { }
        public bool ShouldNotLaunch() { }
    }
    public class DoNotLaunchWhenTypeIsLoaded : Shouldly.Configuration.IShouldNotLaunchDiffTool
    {
        public DoNotLaunchWhenTypeIsLoaded(string typeName) { }
        public bool ShouldNotLaunch() { }
    }
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
    public interface IShouldNotLaunchDiffTool
    {
        bool ShouldNotLaunch();
    }
    public interface ITestMethodFinder
    {
        Shouldly.Configuration.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0);
    }
    public class KnownDiffTools
    {
        public readonly Shouldly.Configuration.DiffTool BeyondCompare3;
        public readonly Shouldly.Configuration.DiffTool BeyondCompare4;
        public readonly Shouldly.Configuration.DiffTool CodeCompare;
        public readonly Shouldly.Configuration.DiffTool CurrentVisualStudio;
        public readonly Shouldly.Configuration.DiffTool KDiff3;
        public readonly Shouldly.Configuration.DiffTool P4Merge;
        public readonly Shouldly.Configuration.DiffTool TortoiseGitMerge;
        public readonly Shouldly.Configuration.DiffTool VimDiff;
        public readonly Shouldly.Configuration.DiffTool VisualStudioCode;
        public readonly Shouldly.Configuration.DiffTool WinMerge;
        public KnownDiffTools() { }
        public static Shouldly.Configuration.KnownDiffTools Instance { get; }
    }
    public class KnownDoNotLaunchStrategies
    {
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool AppVeyor;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool GitLabCI;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool Jenkins;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool MyGet;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool NCrunch;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool TeamCity;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool TravisCI;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool VSTS;
        public readonly Shouldly.Configuration.IShouldNotLaunchDiffTool VisualStudioLiveUnitTesting;
        public KnownDoNotLaunchStrategies() { }
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
            "somMessage.", true)]
        public static void ShouldHaveFlag(this System.Enum actual, System.Enum expectedFlag, System.Func<string?>? customMessage) { }
        public static void ShouldHaveFlag(this System.Enum actual, System.Enum expectedFlag, string? customMessage = null) { }
        [System.Obsolete("Func based customMessage overloads have been removed. Pass in a string for the cu" +
            "somMessage.", true)]
        public static void ShouldNotHaveFlag(this System.Enum actual, System.Enum expectedFlag, System.Func<string?>? customMessage) { }
        public static void ShouldNotHaveFlag(this System.Enum actual, System.Enum expectedFlag, string? customMessage = null) { }
    }
}