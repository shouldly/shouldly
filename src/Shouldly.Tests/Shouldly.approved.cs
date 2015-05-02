[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute(@"Shouldly.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100e3aa2a20de74f360af7742a2b21b409d570722b14ec3d14283a0d89e8147855d1097fbf084d7b3a8bbd1bfe50a589254ce50ee70bf530f23e280e13eeeff3813a6863a8dffa604f6a749628fc82f449e8a5717a4a70787a3f55547f1a2ad8fffafe8945f327dc7a66887b81c7bb5b8f06651f51a7e640e150a7c4cf1049041ca")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.0", FrameworkDisplayName=".NET Framework 4")]

namespace Shouldly
{
    
    public enum Case
    {
        Sensitive = 0,
        Insensitive = 1,
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static DynamicShould
    {
        public static void HaveProperty([System.Runtime.CompilerServices.DynamicAttribute()] object dynamicTestObject, string p) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static Should
    {
        public static void CompleteIn(System.Action action, System.TimeSpan timeout) { }
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout) { }
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout) { }
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout) { }
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout) { }
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action) { }
        public static void NotThrow(System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action) { }
        public static T NotThrow<T>(System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter) { }
        public static void NotThrow(System.Action action) { }
        public static T NotThrow<T>(System.Func<T> action) { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Action actual)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeDictionaryTestExtensions
    {
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key) { }
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string customMessage) { }
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, System.Func<string> customMessage) { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val) { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string customMessage) { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, System.Func<string> customMessage) { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key) { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string customMessage) { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, System.Func<string> customMessage) { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val) { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string customMessage) { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, System.Func<string> customMessage) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeEnumerableTestExtensions
    {
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate) { }
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string customMessage) { }
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string> customMessage) { }
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual) { }
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, string customMessage) { }
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Func<string> customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, string customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, System.Func<string> customMessage) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, string customMessage) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Func<string> customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Func<string> customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string> customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, string customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, System.Func<string> customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, string customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, System.Func<string> customMessage) { }
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual) { }
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, string customMessage) { }
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Func<string> customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, System.Func<string> customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, System.Func<string> customMessage) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeStringTestExtensions
    {
        public static void ShouldBe(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldBe(this string actual, string expected, Shouldly.Case caseSensitivity, string customMessage) { }
        public static void ShouldBe(this string actual, string expected, Shouldly.Case caseSensitivity, System.Func<string> customMessage) { }
        public static void ShouldBeNullOrEmpty(this string actual) { }
        public static void ShouldBeNullOrEmpty(this string actual, string customMessage) { }
        public static void ShouldBeNullOrEmpty(this string actual, System.Func<string> customMessage) { }
        public static void ShouldContain(this string actual, string expected) { }
        public static void ShouldContain(this string actual, string expected, string customMessage) { }
        public static void ShouldContain(this string actual, string expected, System.Func<string> customMessage) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object expected) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object expected, string customMessage) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object expected, System.Func<string> customMessage) { }
        public static void ShouldEndWith(this string actual, string expected) { }
        public static void ShouldEndWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldEndWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldEndWith(this string actual, string expected, System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldMatch(this string actual, string regexPattern) { }
        public static void ShouldMatch(this string actual, string regexPattern, string customMessage) { }
        public static void ShouldMatch(this string actual, string regexPattern, System.Func<string> customMessage) { }
        public static void ShouldNotBeNullOrEmpty(this string actual) { }
        public static void ShouldNotBeNullOrEmpty(this string actual, string customMessage) { }
        public static void ShouldNotBeNullOrEmpty(this string actual, System.Func<string> customMessage) { }
        public static void ShouldNotContain(this string actual, string expected) { }
        public static void ShouldNotContain(this string actual, string expected, string customMessage) { }
        public static void ShouldNotContain(this string actual, string expected, System.Func<string> customMessage) { }
        public static void ShouldNotEndWith(this string actual, string expected) { }
        public static void ShouldNotEndWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotEndWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotEndWith(this string actual, string expected, System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotStartWith(this string actual, string expected) { }
        public static void ShouldNotStartWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotStartWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotStartWith(this string actual, string expected, System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith(this string actual, string expected) { }
        public static void ShouldStartWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldStartWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith(this string actual, string expected, System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeTestExtensions
    {
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance) { }
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe(this float actual, float expected, double tolerance) { }
        public static void ShouldBe(this float actual, float expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this float actual, float expected, double tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe(this double actual, double expected, double tolerance) { }
        public static void ShouldBe(this double actual, double expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this double actual, double expected, double tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, string customMessage) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, System.Func<string> customMessage) { }
        public static void ShouldBe<T>(this T actual, T expected) { }
        public static void ShouldBe<T>(this T actual, T expected, string customMessage) { }
        public static void ShouldBe<T>(this T actual, T expected, System.Func<string> customMessage) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, bool ignoreOrder = False) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, bool ignoreOrder, string customMessage) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, bool ignoreOrder, System.Func<string> customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, string customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, System.Func<string> customMessage) { }
        public static T ShouldBeAssignableTo<T>(this object actual) { }
        public static T ShouldBeAssignableTo<T>(this object actual, string customMessage) { }
        public static T ShouldBeAssignableTo<T>(this object actual, System.Func<string> customMessage) { }
        public static void ShouldBeAssignableTo(this object actual, System.Type expected) { }
        public static void ShouldBeAssignableTo(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldBeAssignableTo(this object actual, System.Type expected, System.Func<string> customMessage) { }
        public static void ShouldBeGreaterThan<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeInRange<T>(this T actual, T from, T to)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThan<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThan<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThan<T>(this T actual, T expected, System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static T ShouldBeOfType<T>(this object actual) { }
        public static T ShouldBeOfType<T>(this object actual, string customMessage) { }
        public static T ShouldBeOfType<T>(this object actual, System.Func<string> customMessage) { }
        public static void ShouldBeOfType(this object actual, System.Type expected) { }
        public static void ShouldBeOfType(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldBeOfType(this object actual, System.Type expected, System.Func<string> customMessage) { }
        public static void ShouldBeOneOf<T>(this T actual, params T[] expected) { }
        public static void ShouldBeSameAs(this object actual, object expected) { }
        public static void ShouldBeSameAs(this object actual, object expected, string customMessage) { }
        public static void ShouldBeSameAs(this object actual, object expected, System.Func<string> customMessage) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, System.Func<string> customMessage) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, System.Func<string> customMessage) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, System.Func<string> customMessage) { }
        [JetBrains.Annotations.ContractAnnotationAttribute("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected) { }
        [JetBrains.Annotations.ContractAnnotationAttribute("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, string customMessage) { }
        [JetBrains.Annotations.ContractAnnotationAttribute("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, System.Func<string> customMessage) { }
        public static void ShouldNotBeAssignableTo<T>(this object actual) { }
        public static void ShouldNotBeAssignableTo<T>(this object actual, string customMessage) { }
        public static void ShouldNotBeAssignableTo<T>(this object actual, System.Func<string> customMessage) { }
        public static void ShouldNotBeAssignableTo(this object actual, System.Type expected) { }
        public static void ShouldNotBeAssignableTo(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldNotBeAssignableTo(this object actual, System.Type expected, System.Func<string> customMessage) { }
        public static void ShouldNotBeInRange<T>(this T actual, T from, T to)
            where T : System.IComparable<> { }
        public static void ShouldNotBeOfType<T>(this object actual) { }
        public static void ShouldNotBeOfType<T>(this object actual, string customMessage) { }
        public static void ShouldNotBeOfType<T>(this object actual, System.Func<string> customMessage) { }
        public static void ShouldNotBeOfType(this object actual, System.Type expected) { }
        public static void ShouldNotBeOfType(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldNotBeOfType(this object actual, System.Type expected, System.Func<string> customMessage) { }
        public static void ShouldNotBeOneOf<T>(this T actual, params T[] expected) { }
        public static void ShouldNotBeSameAs(this object actual, object expected) { }
        public static void ShouldNotBeSameAs(this object actual, object expected, string customMessage) { }
        public static void ShouldNotBeSameAs(this object actual, object expected, System.Func<string> customMessage) { }
    }
    public class static ShouldlyConfiguration
    {
        public static double DefaultFloatingPointTolerance;
        public static System.TimeSpan DefaultTaskTimeout;
        public static System.Collections.Generic.List<string> CompareAsObjectTypes { get; }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions(this object actual, params System.Action[] conditions) { }
    }
}