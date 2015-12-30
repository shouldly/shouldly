[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute(@"Shouldly.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100e3aa2a20de74f360af7742a2b21b409d570722b14ec3d14283a0d89e8147855d1097fbf084d7b3a8bbd1bfe50a589254ce50ee70bf530f23e280e13eeeff3813a6863a8dffa604f6a749628fc82f449e8a5717a4a70787a3f55547f1a2ad8fffafe8945f327dc7a66887b81c7bb5b8f06651f51a7e640e150a7c4cf1049041ca")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.0", FrameworkDisplayName=".NET Framework 4")]

namespace Shouldly
{
    
    public enum Case
    {
        Sensitive = 0,
        Insensitive = 1,
    }
    [System.ObsoleteAttribute("This class is only kept here for backwards compatibility. Please use ShouldAssert" +
        "Exception instead.")]
    public class ChuckedAWobbly : System.Exception
    {
        public ChuckedAWobbly(string message) { }
        public ChuckedAWobbly(string message, System.Exception innerException) { }
    }
    public class DiffTool
    {
        public DiffTool(string name, string path, System.Func<string, string, string> argGenerator) { }
        public string Name { get; }
        public void Open(string receivedPath, string approvedPath) { }
    }
    public class DiffToolConfiguration
    {
        public DiffToolConfiguration() { }
        public void AddDoNotLaunchStrategy(Shouldly.IShouldNotLaunchDiffTool shouldNotlaunchStrategy) { }
        public Shouldly.DiffTool GetDiffTool() { }
        public void RegisterDiffTool(Shouldly.DiffTool diffTool) { }
        public void SetDiffToolPriorities(params Shouldly.DiffTool[] diffTools) { }
        public bool ShouldOpenDiffTool() { }
        public class static KnownDiffTools
        {
            public static readonly Shouldly.DiffTool KDiff3;
        }
        public class static KnownDoNotLaunchStrategies
        {
            public static readonly Shouldly.IShouldNotLaunchDiffTool AppVeyor;
            public static readonly Shouldly.IShouldNotLaunchDiffTool NCrunch;
            public static readonly Shouldly.IShouldNotLaunchDiffTool TeamCity;
        }
    }
    public class DoNotLaunchEnvVariable : Shouldly.IShouldNotLaunchDiffTool
    {
        public DoNotLaunchEnvVariable(string environmentalVariable) { }
        public bool ShouldNotLaunch() { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static DynamicShould
    {
        public static void HaveProperty([System.Runtime.CompilerServices.DynamicAttribute()] object dynamicTestObject, string propertyName) { }
        public static void HaveProperty([System.Runtime.CompilerServices.DynamicAttribute()] object dynamicTestObject, string propertyName, string customMessage) { }
        public static void HaveProperty([System.Runtime.CompilerServices.DynamicAttribute()] object dynamicTestObject, string propertyName, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
    }
    public class FirstNonShouldlyMethodFinder : Shouldly.ITestMethodFinder
    {
        public FirstNonShouldlyMethodFinder() { }
        public int Offset { get; set; }
        public Shouldly.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0) { }
    }
    public interface IShouldNotLaunchDiffTool
    {
        bool ShouldNotLaunch();
    }
    public interface ITestMethodFinder
    {
        Shouldly.TestMethodInfo GetTestMethodInfo(System.Diagnostics.StackTrace stackTrace, int startAt = 0);
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static Should
    {
        public static void CompleteIn(System.Action action, System.TimeSpan timeout) { }
        public static void CompleteIn(System.Action action, System.TimeSpan timeout, string customMessage) { }
        public static void CompleteIn(System.Action action, System.TimeSpan timeout, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout) { }
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout, string customMessage) { }
        public static T CompleteIn<T>(System.Func<T> function, System.TimeSpan timeout, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout) { }
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout, string customMessage) { }
        public static void CompleteIn(System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeout, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout) { }
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout, string customMessage) { }
        public static T CompleteIn<T>(System.Func<System.Threading.Tasks.Task<T>> actual, System.TimeSpan timeout, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout) { }
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout, string customMessage) { }
        public static void CompleteIn(System.Threading.Tasks.Task actual, System.TimeSpan timeout, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout) { }
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout, string customMessage) { }
        public static T CompleteIn<T>(System.Threading.Tasks.Task<T> actual, System.TimeSpan timeout, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Action action) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Action action, string customMessage) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Action action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<T> action) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<T> action, string customMessage) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<T> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action) { }
        public static void NotThrow(System.Threading.Tasks.Task action, string customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, string customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> action) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> action, string customMessage) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter) { }
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static void NotThrow(System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static void NotThrow([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task<T>> action) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task<T>> action, string customMessage) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task<T>> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static T NotThrow<T>(System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static T NotThrow<T>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Action actual)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Action actual, string customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Action actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, string customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> actual)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> actual, string customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>(System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string customMessage)
            where TException : System.Exception { }
        public static TException Throw<TException>([JetBrains.Annotations.InstantHandleAttribute()] System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Threading.Tasks.Task task)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Threading.Tasks.Task task, string customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Threading.Tasks.Task task, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual, string customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ThrowAsync<TException>(System.Func<System.Threading.Tasks.Task> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
    }
    public class ShouldAssertException : Shouldly.ChuckedAWobbly
    {
        public ShouldAssertException(string message) { }
        public ShouldAssertException(string message, System.Exception innerException) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeBooleanExtensions
    {
        public static void ShouldBeFalse(this bool actual) { }
        public static void ShouldBeFalse(this bool actual, string customMessage) { }
        public static void ShouldBeFalse(this bool actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeTrue(this bool actual) { }
        public static void ShouldBeTrue(this bool actual, string customMessage) { }
        public static void ShouldBeTrue(this bool actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeDictionaryTestExtensions
    {
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key) { }
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string customMessage) { }
        public static void ShouldContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val) { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string customMessage) { }
        public static void ShouldContainKeyAndValue<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key) { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, string customMessage) { }
        public static void ShouldNotContainKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val) { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, string customMessage) { }
        public static void ShouldNotContainValueForKey<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> dictionary, TKey key, TValue val, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeEnumerableTestExtensions
    {
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate) { }
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string customMessage) { }
        public static void ShouldAllBe<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity, string customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<string> actual, System.Collections.Generic.IEnumerable<string> expected, Shouldly.Case caseSensitivity, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual) { }
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, string customMessage) { }
        public static void ShouldBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, string customMessage) { }
        public static void ShouldBeSubsetOf<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, string customMessage) { }
        public static void ShouldBeUnique<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string customMessage) { }
        public static void ShouldContain<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, string customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<float> actual, float expected, double tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, string customMessage) { }
        public static void ShouldContain(this System.Collections.Generic.IEnumerable<double> actual, double expected, double tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual) { }
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, string customMessage) { }
        public static void ShouldNotBeEmpty<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, string customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, string customMessage) { }
        public static void ShouldNotContain<T>(this System.Collections.Generic.IEnumerable<T> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Linq.Expressions.Expression<System.Func<T, bool>> elementPredicate, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeNullExtensions
    {
        public static void ShouldBeNull<T>(this T actual) { }
        public static void ShouldBeNull<T>(this T actual, string customMessage) { }
        public static void ShouldBeNull<T>(this T actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeNull<T>(this T actual) { }
        public static void ShouldNotBeNull<T>(this T actual, string customMessage) { }
        public static void ShouldNotBeNull<T>(this T actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeStringTestExtensions
    {
        [System.ObsoleteAttribute("Use the StringCompareShould enum instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        [System.ObsoleteAttribute("Use the StringCompareShould enum instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Shouldly.Case caseSensitivity, string customMessage) { }
        [System.ObsoleteAttribute("Use the StringCompareShould enum instead of the Case enum")]
        public static void ShouldBe(this string actual, string expected, Shouldly.Case caseSensitivity, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this string actual, string expected) { }
        public static void ShouldBe(this string actual, string expected, string customMessage) { }
        public static void ShouldBe(this string actual, string expected, System.Func<string> customMessage) { }
        public static void ShouldBe(this string actual, string expected, Shouldly.StringCompareShould options) { }
        public static void ShouldBe(this string actual, string expected, string customMessage, Shouldly.StringCompareShould option) { }
        public static void ShouldBe(this string actual, string expected, System.Func<string> customMessage, Shouldly.StringCompareShould options) { }
        public static void ShouldBeNullOrEmpty(this string actual) { }
        public static void ShouldBeNullOrEmpty(this string actual, string customMessage) { }
        public static void ShouldBeNullOrEmpty(this string actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeNullOrWhiteSpace(this string actual) { }
        public static void ShouldBeNullOrWhiteSpace(this string actual, string customMessage) { }
        public static void ShouldBeNullOrWhiteSpace(this string actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldContain(this string actual, string expected) { }
        public static void ShouldContain(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldContain(this string actual, string expected, string customMessage) { }
        public static void ShouldContain(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldContain(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldContain(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object expected) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object expected, string customMessage) { }
        public static void ShouldContainWithoutWhitespace(this string actual, object expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldEndWith(this string actual, string expected) { }
        public static void ShouldEndWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldEndWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldEndWith(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldMatch(this string actual, string regexPattern) { }
        public static void ShouldMatch(this string actual, string regexPattern, string customMessage) { }
        public static void ShouldMatch(this string actual, string regexPattern, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeNullOrEmpty(this string actual) { }
        public static void ShouldNotBeNullOrEmpty(this string actual, string customMessage) { }
        public static void ShouldNotBeNullOrEmpty(this string actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeNullOrWhiteSpace(this string actual) { }
        public static void ShouldNotBeNullOrWhiteSpace(this string actual, string customMessage) { }
        public static void ShouldNotBeNullOrWhiteSpace(this string actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotContain(this string actual, string expected) { }
        public static void ShouldNotContain(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotContain(this string actual, string expected, string customMessage) { }
        public static void ShouldNotContain(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotContain(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotContain(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotEndWith(this string actual, string expected) { }
        public static void ShouldNotEndWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotEndWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotEndWith(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotMatch(this string actual, string regexPattern) { }
        public static void ShouldNotMatch(this string actual, string regexPattern, string customMessage) { }
        public static void ShouldNotMatch(this string actual, string regexPattern, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotStartWith(this string actual, string expected) { }
        public static void ShouldNotStartWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldNotStartWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldNotStartWith(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith(this string actual, string expected) { }
        public static void ShouldStartWith(this string actual, string expected, Shouldly.Case caseSensitivity) { }
        public static void ShouldStartWith(this string actual, string expected, string customMessage, Shouldly.Case caseSensitivity = 1) { }
        public static void ShouldStartWith(this string actual, string expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage, Shouldly.Case caseSensitivity = 1) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldBeTestExtensions
    {
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance) { }
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe<T>(this T actual, T expected) { }
        public static void ShouldBe<T>(this T actual, T expected, string customMessage) { }
        public static void ShouldBe<T>(this T actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, bool ignoreOrder = False) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, bool ignoreOrder, string customMessage) { }
        public static void ShouldBe<T>(this System.Collections.Generic.IEnumerable<T> actual, System.Collections.Generic.IEnumerable<T> expected, bool ignoreOrder, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, string customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<decimal> actual, System.Collections.Generic.IEnumerable<decimal> expected, decimal tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this float actual, float expected, double tolerance) { }
        public static void ShouldBe(this float actual, float expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this float actual, float expected, double tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<double> actual, System.Collections.Generic.IEnumerable<double> expected, double tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this System.Collections.Generic.IEnumerable<float> actual, System.Collections.Generic.IEnumerable<float> expected, double tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this double actual, double expected, double tolerance) { }
        public static void ShouldBe(this double actual, double expected, double tolerance, string customMessage) { }
        public static void ShouldBe(this double actual, double expected, double tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, string customMessage) { }
        public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T ShouldBeAssignableTo<T>(this object actual) { }
        public static T ShouldBeAssignableTo<T>(this object actual, string customMessage) { }
        public static T ShouldBeAssignableTo<T>(this object actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeAssignableTo(this object actual, System.Type expected) { }
        public static void ShouldBeAssignableTo(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldBeAssignableTo(this object actual, System.Type expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeGreaterThan<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThan<T>(this T actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeInRange<T>(this T actual, T from, T to)
            where T : System.IComparable<> { }
        public static void ShouldBeInRange<T>(this T actual, T from, T to, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeInRange<T>(this T actual, T from, T to, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThan<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThan<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThan<T>(this T actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeLessThanOrEqualTo<T>(this T actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldBeNegative(this decimal actual) { }
        public static void ShouldBeNegative(this decimal actual, string customMessage) { }
        public static void ShouldBeNegative(this decimal actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeNegative(this double actual) { }
        public static void ShouldBeNegative(this double actual, string customMessage) { }
        public static void ShouldBeNegative(this double actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeNegative(this float actual) { }
        public static void ShouldBeNegative(this float actual, string customMessage) { }
        public static void ShouldBeNegative(this float actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeNegative(this int actual) { }
        public static void ShouldBeNegative(this int actual, string customMessage) { }
        public static void ShouldBeNegative(this int actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeNegative(this long actual) { }
        public static void ShouldBeNegative(this long actual, string customMessage) { }
        public static void ShouldBeNegative(this long actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeNegative(this short actual) { }
        public static void ShouldBeNegative(this short actual, string customMessage) { }
        public static void ShouldBeNegative(this short actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T ShouldBeOfType<T>(this object actual) { }
        public static T ShouldBeOfType<T>(this object actual, string customMessage) { }
        public static T ShouldBeOfType<T>(this object actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeOfType(this object actual, System.Type expected) { }
        public static void ShouldBeOfType(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldBeOfType(this object actual, System.Type expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeOneOf<T>(this T actual, params T[] expected) { }
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, string customMessage) { }
        public static void ShouldBeOneOf<T>(this T actual, T[] expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBePositive(this decimal actual) { }
        public static void ShouldBePositive(this decimal actual, string customMessage) { }
        public static void ShouldBePositive(this decimal actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBePositive(this double actual) { }
        public static void ShouldBePositive(this double actual, string customMessage) { }
        public static void ShouldBePositive(this double actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBePositive(this float actual) { }
        public static void ShouldBePositive(this float actual, string customMessage) { }
        public static void ShouldBePositive(this float actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBePositive(this int actual) { }
        public static void ShouldBePositive(this int actual, string customMessage) { }
        public static void ShouldBePositive(this int actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBePositive(this long actual) { }
        public static void ShouldBePositive(this long actual, string customMessage) { }
        public static void ShouldBePositive(this long actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBePositive(this short actual) { }
        public static void ShouldBePositive(this short actual, string customMessage) { }
        public static void ShouldBePositive(this short actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldBeSameAs(this object actual, object expected) { }
        public static void ShouldBeSameAs(this object actual, object expected, string customMessage) { }
        public static void ShouldBeSameAs(this object actual, object expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldNotBe(this System.DateTime actual, System.DateTime expected, System.TimeSpan tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldNotBe(this System.DateTimeOffset actual, System.DateTimeOffset expected, System.TimeSpan tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, string customMessage) { }
        public static void ShouldNotBe(this System.TimeSpan actual, System.TimeSpan expected, System.TimeSpan tolerance, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        [JetBrains.Annotations.ContractAnnotationAttribute("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected) { }
        [JetBrains.Annotations.ContractAnnotationAttribute("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, string customMessage) { }
        [JetBrains.Annotations.ContractAnnotationAttribute("actual:null,expected:null => halt")]
        public static void ShouldNotBe<T>(this T actual, T expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeAssignableTo<T>(this object actual) { }
        public static void ShouldNotBeAssignableTo<T>(this object actual, string customMessage) { }
        public static void ShouldNotBeAssignableTo<T>(this object actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeAssignableTo(this object actual, System.Type expected) { }
        public static void ShouldNotBeAssignableTo(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldNotBeAssignableTo(this object actual, System.Type expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeInRange<T>(this T actual, T from, T to)
            where T : System.IComparable<> { }
        public static void ShouldNotBeInRange<T>(this T actual, T from, T to, string customMessage)
            where T : System.IComparable<> { }
        public static void ShouldNotBeInRange<T>(this T actual, T from, T to, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where T : System.IComparable<> { }
        public static void ShouldNotBeOfType<T>(this object actual) { }
        public static void ShouldNotBeOfType<T>(this object actual, string customMessage) { }
        public static void ShouldNotBeOfType<T>(this object actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeOfType(this object actual, System.Type expected) { }
        public static void ShouldNotBeOfType(this object actual, System.Type expected, string customMessage) { }
        public static void ShouldNotBeOfType(this object actual, System.Type expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeOneOf<T>(this T actual, params T[] expected) { }
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, string customMessage) { }
        public static void ShouldNotBeOneOf<T>(this T actual, T[] expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotBeSameAs(this object actual, object expected) { }
        public static void ShouldNotBeSameAs(this object actual, object expected, string customMessage) { }
        public static void ShouldNotBeSameAs(this object actual, object expected, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
    }
    public class ShouldCompleteInException : System.TimeoutException
    {
        public ShouldCompleteInException(string message, System.TimeoutException inner) { }
    }
    public class static ShouldlyConfiguration
    {
        public static double DefaultFloatingPointTolerance;
        public static System.TimeSpan DefaultTaskTimeout;
        public static System.Collections.Generic.List<string> CompareAsObjectTypes { get; }
        public static Shouldly.DiffToolConfiguration DiffTools { get; }
        public static System.IDisposable DisableSourceInErrors() { }
        public static bool IsSourceDisabledInErrors() { }
    }
    public class ShouldlyMethodsAttribute : System.Attribute
    {
        public ShouldlyMethodsAttribute() { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldMatchApprovedTestExtensions
    {
        public static void ShouldMatchApproved(this string actual) { }
        public static void ShouldMatchApproved(this string actual, string customMessage) { }
        public static void ShouldMatchApproved(this string actual, System.Action<Shouldly.ShouldMatchConfigurationBuilder> configureOptions) { }
        public static void ShouldMatchApproved(this string actual, string customMessage, System.Action<Shouldly.ShouldMatchConfigurationBuilder> configureOptions) { }
        public static void ShouldMatchApproved(this string actual, System.Func<string> customMessage, System.Action<Shouldly.ShouldMatchConfigurationBuilder> configureOptions) { }
    }
    public class ShouldMatchConfiguration
    {
        public ShouldMatchConfiguration() { }
        public string FileExtension { get; set; }
        public string FilenameDescriminator { get; set; }
        public bool PreventDiff { get; set; }
        public Shouldly.StringCompareShould StringCompareOptions { get; set; }
        public Shouldly.ITestMethodFinder TestMethodFinder { get; set; }
    }
    public class ShouldMatchConfigurationBuilder
    {
        public ShouldMatchConfigurationBuilder() { }
        public Shouldly.ShouldMatchConfiguration Build() { }
        public Shouldly.ShouldMatchConfigurationBuilder Configure(System.Action<Shouldly.ShouldMatchConfiguration> configure) { }
        public Shouldly.ShouldMatchConfigurationBuilder IgnoreLineEndings() { }
        public Shouldly.ShouldMatchConfigurationBuilder NoDiff() { }
        public Shouldly.ShouldMatchConfigurationBuilder UseCallerLocation() { }
        public Shouldly.ShouldMatchConfigurationBuilder WithDescriminator(string fileDescriminator) { }
        public Shouldly.ShouldMatchConfigurationBuilder WithFileExtension(string fileExtension) { }
        public Shouldly.ShouldMatchConfigurationBuilder WithStringCompareOptions(Shouldly.StringCompareShould stringCompareOptions) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldSatisfyAllConditionsTestExtensions
    {
        public static void ShouldSatisfyAllConditions(this object actual, [JetBrains.Annotations.InstantHandleAttribute()] params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions(this object actual, string customMessage, [JetBrains.Annotations.InstantHandleAttribute()] params System.Action[] conditions) { }
        public static void ShouldSatisfyAllConditions(this object actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage, [JetBrains.Annotations.InstantHandleAttribute()] params System.Action[] conditions) { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldThrowAsyncExtensions
    {
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task, string customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Threading.Tasks.Task task, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual, string customMessage)
            where TException : System.Exception { }
        public static System.Threading.Tasks.Task<TException> ShouldThrowAsync<TException>(this System.Func<System.Threading.Tasks.Task> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldThrowExtensions
    {
        public static void ShouldNotThrow(this System.Action action) { }
        public static void ShouldNotThrow(this System.Action action, string customMessage) { }
        public static void ShouldNotThrow(this System.Action action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<T> action) { }
        public static T ShouldNotThrow<T>(this System.Func<T> action, string customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<T> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static TException ShouldThrow<TException>(this System.Action actual)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Action actual, string customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Action actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
    }
    [Shouldly.ShouldlyMethodsAttribute()]
    public class static ShouldThrowTaskExtensions
    {
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, string customMessage) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, string customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, string customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static void ShouldNotThrow(this System.Threading.Tasks.Task action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static void ShouldNotThrow(this System.Func<System.Threading.Tasks.Task> action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, string customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static T ShouldNotThrow<T>(this System.Threading.Tasks.Task<T> action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, string customMessage) { }
        public static T ShouldNotThrow<T>(this System.Func<System.Threading.Tasks.Task<T>> action, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage) { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, string customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, string customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, string customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Threading.Tasks.Task actual, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, string customMessage)
            where TException : System.Exception { }
        public static TException ShouldThrow<TException>(this System.Func<System.Threading.Tasks.Task> actual, System.TimeSpan timeoutAfter, [JetBrains.Annotations.InstantHandleAttribute()] System.Func<string> customMessage)
            where TException : System.Exception { }
    }
    [System.FlagsAttribute()]
    public enum StringCompareShould
    {
        IgnoreCase = 1,
        IgnoreLineEndings = 2,
    }
    public class TestMethodInfo
    {
        public TestMethodInfo(System.Diagnostics.StackFrame callingFrame) { }
        public string MethodName { get; }
        public string SourceFileDirectory { get; }
    }
}