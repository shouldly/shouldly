/* MIT License

Copyright (c) 2016 JetBrains http://www.jetbrains.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

#nullable disable

#pragma warning disable 1591
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable RedundantTypeDeclarationBody
// ReSharper disable ArrangeNamespaceBody
// ReSharper disable InconsistentNaming

namespace JetBrains.Annotations
{
    /// <summary>
    /// Indicates that the value of the marked element could be <c>null</c> sometimes,
    /// so checking for <c>null</c> is required before its usage.
    /// </summary>
    /// <example><code>
    /// [CanBeNull] object Test() => null;
    ///
    /// void UseTest() {
    ///   var p = Test();
    ///   var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    internal sealed class CanBeNullAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the value of the marked element can never be <c>null</c>.
    /// </summary>
    /// <example><code>
    /// [NotNull] object Foo() {
    ///   return null; // Warning: Possible 'null' assignment
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    internal sealed class NotNullAttribute : Attribute
    {
    }

    /// <summary>
    /// Can be applied to symbols of types derived from <c>IEnumerable</c> as well as to symbols of <c>Task</c>
    /// and <c>Lazy</c> classes to indicate that the value of a collection item, of the <c>Task.Result</c> property,
    /// or of the <c>Lazy.Value</c> property can never be null.
    /// </summary>
    /// <example><code>
    /// public void Foo([ItemNotNull]List&lt;string&gt; books)
    /// {
    ///   foreach (var book in books) {
    ///     if (book != null) // Warning: Expression is always true
    ///      Console.WriteLine(book.ToUpper());
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field)]
    internal sealed class ItemNotNullAttribute : Attribute
    {
    }

    /// <summary>
    /// Can be applied to symbols of types derived from <c>IEnumerable</c> as well as to symbols of <c>Task</c>
    /// and <c>Lazy</c> classes to indicate that the value of a collection item, of the <c>Task.Result</c> property,
    /// or of the <c>Lazy.Value</c> property can be null.
    /// </summary>
    /// <example><code>
    /// public void Foo([ItemCanBeNull]List&lt;string&gt; books)
    /// {
    ///   foreach (var book in books)
    ///   {
    ///     // Warning: Possible 'System.NullReferenceException'
    ///     Console.WriteLine(book.ToUpper());
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field)]
    internal sealed class ItemCanBeNullAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked method builds a string by the format pattern and (optional) arguments.
    /// The parameter that accepts the format string should be specified in the constructor. The format string
    /// should be in the <see cref="string.Format(IFormatProvider,string,object[])"/>-like form.
    /// </summary>
    /// <example><code>
    /// [StringFormatMethod("message")]
    /// void ShowError(string message, params object[] args) { /* do something */ }
    ///
    /// void Foo() {
    ///   ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
    /// }
    /// </code></example>
    /// <seealso cref="StructuredMessageTemplateAttribute"/>
    [AttributeUsage(
        AttributeTargets.Constructor | AttributeTargets.Method |
        AttributeTargets.Property | AttributeTargets.Delegate)]
    internal sealed class StringFormatMethodAttribute : Attribute
    {
        /// <param name="formatParameterName">
        /// Specifies which parameter of an annotated method should be treated as the format string.
        /// </param>
        public StringFormatMethodAttribute([NotNull] string formatParameterName)
        {
            FormatParameterName = formatParameterName;
        }

        [NotNull] public string FormatParameterName { get; }
    }

    /// <summary>
    /// Indicates that the marked parameter is a message template where placeholders are to be replaced by
    /// the following arguments in the order in which they appear.
    /// </summary>
    /// <example><code>
    /// void LogInfo([StructuredMessageTemplate]string message, params object[] args) { /* do something */ }
    ///
    /// void Foo() {
    ///   LogInfo("User created: {username}"); // Warning: Non-existing argument in format string
    /// }
    /// </code></example>
    /// <seealso cref="StringFormatMethodAttribute"/>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class StructuredMessageTemplateAttribute : Attribute
    {
    }

    /// <summary>
    /// Use this annotation to specify a type that contains static or const fields
    /// with values for the annotated property/field/parameter.
    /// The specified type will be used to improve completion suggestions.
    /// </summary>
    /// <example><code>
    /// namespace TestNamespace
    /// {
    ///   public class Constants
    ///   {
    ///     public static int INT_CONST = 1;
    ///     public const string STRING_CONST = "1";
    ///   }
    ///
    ///   public class Class1
    ///   {
    ///     [ValueProvider("TestNamespace.Constants")] public int myField;
    ///     public void Foo([ValueProvider("TestNamespace.Constants")] string str) { }
    ///
    ///     public void Test()
    ///     {
    ///       Foo(/*try completion here*/);//
    ///       myField = /*try completion here*/
    ///     }
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = true)]
    internal sealed class ValueProviderAttribute : Attribute
    {
        public ValueProviderAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull] public string Name { get; }
    }

    /// <summary>
    /// Indicates that the integral value falls into the specified interval.
    /// It is allowed to specify multiple non-intersecting intervals.
    /// Values of interval boundaries are included in the interval.
    /// </summary>
    /// <example><code>
    /// void Foo([ValueRange(0, 100)] int value) {
    ///   if (value == -1) { // Warning: Expression is always 'false'
    ///     ...
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Method | AttributeTargets.Delegate,
        AllowMultiple = true)]
    internal sealed class ValueRangeAttribute : Attribute
    {
        public object From { get; }
        public object To { get; }

        public ValueRangeAttribute(long from, long to)
        {
            From = from;
            To = to;
        }

        public ValueRangeAttribute(ulong from, ulong to)
        {
            From = from;
            To = to;
        }

        public ValueRangeAttribute(long value)
        {
            From = To = value;
        }

        public ValueRangeAttribute(ulong value)
        {
            From = To = value;
        }
    }

    /// <summary>
    /// Indicates that the integral value never falls below zero.
    /// </summary>
    /// <example><code>
    /// void Foo([NonNegativeValue] int value) {
    ///   if (value == -1) { // Warning: Expression is always 'false'
    ///     ...
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Method | AttributeTargets.Delegate)]
    internal sealed class NonNegativeValueAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the function argument should be a string literal and match
    /// one of the parameters of the caller function. This annotation is used for parameters
    /// like <c>string paramName</c> parameter of the <see cref="System.ArgumentNullException"/> constructor.
    /// </summary>
    /// <example><code>
    /// void Foo(string param) {
    ///   if (param == null)
    ///     throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class InvokerParameterNameAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the method is contained in a type that implements the
    /// <c>System.ComponentModel.INotifyPropertyChanged</c> interface and this method
    /// is used to notify that some property value changed.
    /// </summary>
    /// <remarks>
    /// The method should be non-static and conform to one of the supported signatures:
    /// <list>
    /// <item><c>NotifyChanged(string)</c></item>
    /// <item><c>NotifyChanged(params string[])</c></item>
    /// <item><c>NotifyChanged{T}(Expression{Func{T}})</c></item>
    /// <item><c>NotifyChanged{T,U}(Expression{Func{T,U}})</c></item>
    /// <item><c>SetProperty{T}(ref T, T, string)</c></item>
    /// </list>
    /// </remarks>
    /// <example><code>
    /// public class Foo : INotifyPropertyChanged {
    ///   public event PropertyChangedEventHandler PropertyChanged;
    ///
    ///   [NotifyPropertyChangedInvocator]
    ///   protected virtual void NotifyChanged(string propertyName) { ... }
    ///
    ///   string _name;
    ///
    ///   public string Name {
    ///     get { return _name; }
    ///     set { _name = value; NotifyChanged("LastName"); /* Warning */ }
    ///   }
    /// }
    /// </code>
    /// Examples of generated notifications:
    /// <list>
    /// <item><c>NotifyChanged("Property")</c></item>
    /// <item><c>NotifyChanged(() =&gt; Property)</c></item>
    /// <item><c>NotifyChanged((VM x) =&gt; x.Property)</c></item>
    /// <item><c>SetProperty(ref myField, value, "Property")</c></item>
    /// </list>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        public NotifyPropertyChangedInvocatorAttribute()
        {
        }

        public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
        {
            ParameterName = parameterName;
        }

        [CanBeNull] public string ParameterName { get; }
    }

    /// <summary>
    /// Describes dependence between the input and output of a method.
    /// </summary>
    /// <syntax>
    /// <p>Function Definition Table syntax:</p>
    /// <list>
    /// <item>FDT      ::= FDTRow [;FDTRow]*</item>
    /// <item>FDTRow   ::= Input =&gt; Output | Output &lt;= Input</item>
    /// <item>Input    ::= ParameterName: Value [, Input]*</item>
    /// <item>Output   ::= [ParameterName: Value]* {halt|stop|void|nothing|Value}</item>
    /// <item>Value    ::= true | false | null | notnull | canbenull</item>
    /// </list>
    /// If the method has a single input parameter, its name could be omitted.<br/>
    /// Using <c>halt</c> (or <c>void</c>/<c>nothing</c>, which is the same) for the method output
    /// means that the method doesn't return normally (throws or terminates the process).<br/>
    /// The <c>canbenull</c> value is only applicable to output parameters.<br/>
    /// You can use multiple <c>[ContractAnnotation]</c> for each FDT row, or use a single attribute
    /// with rows separated by the semicolon. The order of rows doesn't matter, all rows are checked
    /// for applicability and applied per each program state tracked by the analysis engine.<br/>
    /// </syntax>
    /// <examples><list>
    /// <item><code>
    /// [ContractAnnotation("=&gt; halt")]
    /// public void TerminationMethod()
    /// </code></item>
    /// <item><code>
    /// [ContractAnnotation("null &lt;= param:null")] // reverse condition syntax
    /// public string GetName(string surname)
    /// </code></item>
    /// <item><code>
    /// [ContractAnnotation("s:null =&gt; true")]
    /// public bool IsNullOrEmpty(string s) // string.IsNullOrEmpty()
    /// </code></item>
    /// <item><code>
    /// // A method that returns null if the parameter is null,
    /// // and not null if the parameter is not null
    /// [ContractAnnotation("null =&gt; null; notnull =&gt; notnull")]
    /// public object Transform(object data)
    /// </code></item>
    /// <item><code>
    /// [ContractAnnotation("=&gt; true, result: notnull; =&gt; false, result: null")]
    /// public bool TryParse(string s, out Person result)
    /// </code></item>
    /// </list></examples>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ContractAnnotationAttribute : Attribute
    {
        public ContractAnnotationAttribute([NotNull] string contract)
            : this(contract, false)
        {
        }

        public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
        {
            Contract = contract;
            ForceFullStates = forceFullStates;
        }

        [NotNull] public string Contract { get; }

        public bool ForceFullStates { get; }
    }

    /// <summary>
    /// Indicates whether the marked element should be localized.
    /// </summary>
    /// <example><code>
    /// [LocalizationRequiredAttribute(true)]
    /// class Foo {
    ///   string str = "my string"; // Warning: Localizable string
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class LocalizationRequiredAttribute : Attribute
    {
        public LocalizationRequiredAttribute() : this(true)
        {
        }

        public LocalizationRequiredAttribute(bool required)
        {
            Required = required;
        }

        public bool Required { get; }
    }

    /// <summary>
    /// Indicates that the value of the marked type (or its derivatives)
    /// cannot be compared using <c>==</c> or <c>!=</c> operators, and <c>Equals()</c>
    /// should be used instead. However, using <c>==</c> or <c>!=</c> for comparison
    /// with <c>null</c> is always permitted.
    /// </summary>
    /// <example><code>
    /// [CannotApplyEqualityOperator]
    /// class NoEquality { }
    ///
    /// class UsesNoEquality {
    ///   void Test() {
    ///     var ca1 = new NoEquality();
    ///     var ca2 = new NoEquality();
    ///     if (ca1 != null) { // OK
    ///       bool condition = ca1 == ca2; // Warning
    ///     }
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct)]
    internal sealed class CannotApplyEqualityOperatorAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the method or type uses equality members of the annotated element.
    /// </summary>
    /// <remarks>
    /// When applied to the method's generic parameter, indicates that the equality of the annotated type is used,
    /// unless a custom equality comparer is passed when calling this method. The attribute can also be applied
    /// directly to the method's parameter or return type to specify equality usage for it.
    /// When applied to the type's generic parameter, indicates that type equality usage can happen anywhere
    /// inside this type, so the instantiation of this type is treated as equality usage, unless a custom
    /// equality comparer is passed to the constructor.
    /// </remarks>
    /// <example><code>
    /// struct StructWithDefaultEquality { }
    ///
    /// class MySet&lt;[DefaultEqualityUsage] T&gt; { }
    ///
    /// static class Extensions {
    ///     public static MySet&lt;T&gt; ToMySet&lt;[DefaultEqualityUsage] T&gt;(this IEnumerable&lt;T&gt; items) =&gt; new();
    /// }
    ///
    /// class MyList&lt;T&gt; { public int IndexOf([DefaultEqualityUsage] T item) =&gt; 0; }
    ///
    /// class UsesDefaultEquality {
    ///     void Test() {
    ///         var list = new MyList&lt;StructWithDefaultEquality&gt;();
    ///         list.IndexOf(new StructWithDefaultEquality()); // Warning: Default equality of struct 'StructWithDefaultEquality' is used
    ///
    ///         var set = new MySet&lt;StructWithDefaultEquality&gt;(); // Warning: Default equality of struct 'StructWithDefaultEquality' is used
    ///         var set2 = new StructWithDefaultEquality[1].ToMySet(); // Warning: Default equality of struct 'StructWithDefaultEquality' is used
    ///     }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.GenericParameter | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    internal sealed class DefaultEqualityUsageAttribute : Attribute
    {
    }

    /// <summary>
    /// When applied to a target attribute, specifies a requirement for any type marked
    /// with the target attribute to implement or inherit the specific type or types.
    /// </summary>
    /// <example><code>
    /// [BaseTypeRequired(typeof(IComponent)] // Specify requirement
    /// class ComponentAttribute : Attribute { }
    ///
    /// [Component] // ComponentAttribute requires implementing IComponent interface
    /// class MyComponent : IComponent { }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [BaseTypeRequired(typeof(Attribute))]
    internal sealed class BaseTypeRequiredAttribute : Attribute
    {
        public BaseTypeRequiredAttribute([NotNull] Type baseType)
        {
            BaseType = baseType;
        }

        [NotNull] public Type BaseType { get; }
    }

    /// <summary>
    /// Indicates that the marked symbol is used implicitly (via reflection, in an external library, and so on),
    /// so this symbol will be ignored by usage-checking inspections. <br/>
    /// You can use <see cref="ImplicitUseKindFlags"/> and <see cref="ImplicitUseTargetFlags"/>
    /// to configure how this attribute is applied.
    /// </summary>
    /// <example><code>
    /// [UsedImplicitly]
    /// public class TypeConverter {}
    ///
    /// public class SummaryData
    /// {
    ///   [UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
    ///   public SummaryData() {}
    /// }
    ///
    /// [UsedImplicitly(ImplicitUseTargetFlags.WithInheritors | ImplicitUseTargetFlags.Default)]
    /// public interface IService {}
    /// </code></example>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class UsedImplicitlyAttribute : Attribute
    {
        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        public ImplicitUseKindFlags UseKindFlags { get; }

        public ImplicitUseTargetFlags TargetFlags { get; }

        public string Reason { get; set; }
    }

    /// <summary>
    /// Can be applied to attributes, type parameters, and parameters of a type assignable from <see cref="System.Type"/>.
    /// When applied to an attribute, the decorated attribute behaves the same as <see cref="UsedImplicitlyAttribute"/>.
    /// When applied to a type parameter or to a parameter of type <see cref="System.Type"/>,
    /// indicates that the corresponding type is used implicitly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter | AttributeTargets.Parameter)]
    internal sealed class MeansImplicitUseAttribute : Attribute
    {
        public MeansImplicitUseAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly] public ImplicitUseKindFlags UseKindFlags { get; }

        [UsedImplicitly] public ImplicitUseTargetFlags TargetFlags { get; }
    }

    /// <summary>
    /// Specifies the details of an implicitly used symbol when it is marked
    /// with <see cref="MeansImplicitUseAttribute"/> or <see cref="UsedImplicitlyAttribute"/>.
    /// </summary>
    [Flags]
    internal enum ImplicitUseKindFlags
    {
        Default = Access | Assign | InstantiatedWithFixedConstructorSignature,

        /// <summary>Only entity marked with attribute considered used.</summary>
        Access = 1,

        /// <summary>Indicates implicit assignment to a member.</summary>
        Assign = 2,

        /// <summary>
        /// Indicates implicit instantiation of a type with fixed constructor signature.
        /// That means any unused constructor parameters will not be reported as such.
        /// </summary>
        InstantiatedWithFixedConstructorSignature = 4,

        /// <summary>Indicates implicit instantiation of a type.</summary>
        InstantiatedNoFixedConstructorSignature = 8,
    }

    /// <summary>
    /// Specifies what is considered to be used implicitly when marked
    /// with <see cref="MeansImplicitUseAttribute"/> or <see cref="UsedImplicitlyAttribute"/>.
    /// </summary>
    [Flags]
    internal enum ImplicitUseTargetFlags
    {
        Default = Itself,
        Itself = 1,

        /// <summary>Members of the type marked with the attribute are considered used.</summary>
        Members = 2,

        /// <summary> Inherited entities are considered used. </summary>
        WithInheritors = 4,

        /// <summary>Entity marked with the attribute and all its members considered used.</summary>
        WithMembers = Itself | Members
    }

    /// <summary>
    /// This attribute is intended to mark publicly available APIs
    /// that should not be removed and therefore should never be reported as unused.
    /// </summary>
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    internal sealed class PublicAPIAttribute : Attribute
    {
        public PublicAPIAttribute()
        {
        }

        public PublicAPIAttribute([NotNull] string comment)
        {
            Comment = comment;
        }

        [CanBeNull] public string Comment { get; }
    }

    /// <summary>
    /// Tells the code analysis engine if the parameter is completely handled when the invoked method is on stack.
    /// If the parameter is a delegate, indicates that the delegate can only be invoked during method execution
    /// (the delegate can be invoked zero or multiple times, but not stored to some field and invoked later,
    /// when the containing method is no longer on the execution stack).
    /// If the parameter is an enumerable, indicates that it is enumerated while the method is executed.
    /// If <see cref="RequireAwait"/> is true, the attribute will only take effect
    /// if the method invocation is located under the <c>await</c> expression.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class InstantHandleAttribute : Attribute
    {
        /// <summary>
        /// Requires the method invocation to be used under the <c>await</c> expression for this attribute to take effect.
        /// Can be used for delegate/enumerable parameters of <c>async</c> methods.
        /// </summary>
        public bool RequireAwait { get; set; }
    }

    /// <summary>
    /// Indicates that the method does not make any observable state changes.
    /// The same as <see cref="T:System.Diagnostics.Contracts.PureAttribute"/>.
    /// </summary>
    /// <example><code>
    /// [Pure] int Multiply(int x, int y) => x * y;
    ///
    /// void M() {
    ///   Multiply(123, 42); // Warning: Return value of pure method is not used
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class PureAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the return value of the method invocation must be used.
    /// </summary>
    /// <remarks>
    /// Methods decorated with this attribute (in contrast to pure methods) might change state,
    /// but make no sense without using their return value. <br/>
    /// Similarly to <see cref="PureAttribute"/>, this attribute
    /// will help detect usages of the method when the return value is not used.
    /// Optionally, you can specify a message to use when showing warnings, e.g.
    /// <code>[MustUseReturnValue("Use the return value to...")]</code>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MustUseReturnValueAttribute : Attribute
    {
        public MustUseReturnValueAttribute()
        {
        }

        public MustUseReturnValueAttribute([NotNull] string justification)
        {
            Justification = justification;
        }

        [CanBeNull] public string Justification { get; }

        /// <summary>
        /// Enables the special handling of the "fluent" APIs that perform mutations and return 'this' object.
        /// In this case the analysis checks the fluent invocations chain and only warns if the initial receiver value
        /// is probably a temporary value - in this case the very last fluent method return assumed to be temporary as well,
        /// therefore is a subject of warning if unused. If the initial receiver is a local variable or 'this' reference
        /// the analysis assumes that fluent invocations were used to mutate the existing value and warning will not be shown.
        /// </summary>
        /// <remarks>
        /// This property must only be used for methods with the return type matching the receiver type.
        /// </remarks>
        public bool IsFluentBuilderMethod { get; set; }
    }

    /// <summary>
    /// Indicates that the resource disposal must be handled at the use site,
    /// meaning that the resource ownership is transferred to the caller.
    /// This annotation can be used to annotate disposable types or their constructors individually to enable
    /// the IDE code analysis for resource disposal in every context where the new instance of this type is created.
    /// Factory methods and <c>out</c> parameters can also be annotated to indicate that the return value
    /// of the disposable type needs handling.
    /// </summary>
    /// <remarks>
    /// Annotation of input parameters with this attribute is meaningless.<br/>
    /// Constructors inherit this attribute from their type if it is annotated,
    /// but not from the base constructors they delegate to (if any).<br/>
    /// Resource disposal is expected via <c>using (resource)</c> statement,
    /// <c>using var</c> declaration, explicit <c>Dispose()</c> call, or passing the resource as an argument
    /// to a parameter annotated with the <see cref="HandlesResourceDisposalAttribute"/> attribute.
    /// </remarks>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct |
        AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Parameter)]
    internal sealed class MustDisposeResourceAttribute : Attribute
    {
        public MustDisposeResourceAttribute()
        {
            Value = true;
        }

        public MustDisposeResourceAttribute(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// When set to <c>false</c>, disposing of the resource is not obligatory.
        /// The main use-case for explicit <c>[MustDisposeResource(false)]</c> annotation
        /// is to loosen the annotation for inheritors.
        /// </summary>
        public bool Value { get; }
    }

    /// <summary>
    /// Indicates that method or class instance acquires resource ownership and will dispose it after use.
    /// </summary>
    /// <remarks>
    /// Annotation of <c>out</c> parameters with this attribute is meaningless.<br/>
    /// When an instance method is annotated with this attribute,
    /// it means that it is handling the resource disposal of the corresponding resource instance.<br/>
    /// When a field or a property is annotated with this attribute, it means that this type owns the resource
    /// and will handle the resource disposal properly (e.g. in own <c>IDisposable</c> implementation).
    /// </remarks>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class HandlesResourceDisposalAttribute : Attribute
    {
    }

    /// <summary>
    /// This annotation allows enforcing allocation-less usage patterns of delegates for performance-critical APIs.
    /// When this annotation is applied to the parameter of a delegate type,
    /// the IDE checks the input argument of this parameter:
    /// * When a lambda expression or anonymous method is passed as an argument, the IDE verifies that the passed closure
    ///   has no captures of the containing local variables and the compiler is able to cache the delegate instance
    ///   to avoid heap allocations. Otherwise, a warning is produced.
    /// * The IDE warns when the method name or local function name is passed as an argument because this always results
    ///   in heap allocation of the delegate instance.
    /// </summary>
    /// <remarks>
    /// In C# 9.0+ code, the IDE will also suggest annotating the anonymous functions with the <c>static</c> modifier
    /// to make use of the similar analysis provided by the language/compiler.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class RequireStaticDelegateAttribute : Attribute
    {
        public bool IsError { get; set; }
    }

    /// <summary>
    /// Indicates the type member or parameter of some type that should be used instead of all other ways
    /// to get the value of that type. This annotation is useful when you have some 'context' value evaluated
    /// and stored somewhere, meaning that all other ways to get this value must be consolidated with the existing one.
    /// </summary>
    /// <example><code>
    /// class Foo {
    ///   [ProvidesContext] IBarService _barService = ...;
    ///
    ///   void ProcessNode(INode node) {
    ///     DoSomething(node, node.GetGlobalServices().Bar);
    ///     //              ^ Warning: use value of '_barService' field
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Method |
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct |
        AttributeTargets.GenericParameter)]
    internal sealed class ProvidesContextAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that a parameter is a path to a file or a folder within a web project.
    /// Path can be relative or absolute, starting from web root (~).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class PathReferenceAttribute : Attribute
    {
        public PathReferenceAttribute()
        {
        }

        public PathReferenceAttribute([NotNull, PathReference] string basePath)
        {
            BasePath = basePath;
        }

        [CanBeNull] public string BasePath { get; }
    }

    /// <summary>
    /// An extension method marked with this attribute is processed by code completion
    /// as a 'Source Template'. When the extension method is completed over some expression, its source code
    /// is automatically expanded like a template at the call site.
    /// </summary>
    /// <remarks>
    /// Template method bodies can contain valid source code and/or special comments starting with '$'.
    /// Text inside these comments is added as source code when the template is applied. Template parameters
    /// can be used either as additional method parameters or as identifiers wrapped in two '$' signs.
    /// Use the <see cref="MacroAttribute"/> attribute to specify macros for parameters.
    /// The expression to be used in the expansion can be adjusted
    /// by the <see cref="SourceTemplateAttribute.Target"/> parameter.
    /// </remarks>
    /// <example>
    /// In this example, the <c>forEach</c> method is a source template available over all values
    /// of enumerable types, producing an ordinary C# <c>foreach</c> statement and placing the caret inside the block:
    /// <code>
    /// [SourceTemplate]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; xs) {
    ///   foreach (var x in xs) {
    ///      //$ $END$
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class SourceTemplateAttribute : Attribute
    {
        /// <summary>
        /// Allows specifying the expression to capture for template execution if more than one expression
        /// is available at the expansion point.
        /// If not specified, <see cref="SourceTemplateTargetExpression.Inner"/> is assumed.
        /// </summary>
        public SourceTemplateTargetExpression Target { get; set; }
    }

    /// <summary>
    /// Provides a value for the <see cref="SourceTemplateAttribute"/> to define how to capture
    /// the expression at the point of expansion
    /// </summary>
    internal enum SourceTemplateTargetExpression
    {
        /// <summary>Selects inner expression</summary>
        /// <example><c>value > 42.{caret}</c> captures <c>42</c></example>
        /// <example><c>_args = args.{caret}</c> captures <c>args</c></example>
        Inner = 0,

        /// <summary>Selects outer expression</summary>
        /// <example><c>value > 42.{caret}</c> captures <c>value > 42</c></example>
        /// <example><c>_args = args.{caret}</c> captures whole assignment</example>
        Outer = 1
    }

    /// <summary>
    /// Allows specifying a macro for a parameter of a <see cref="SourceTemplateAttribute">source template</see>.
    /// </summary>
    /// <remarks>
    /// You can apply the attribute to the whole method or to any of its additional parameters. The macro expression
    /// is defined in the <see cref="MacroAttribute.Expression"/> property. When applied to a method, the target
    /// template parameter is defined in the <see cref="MacroAttribute.Target"/> property. To apply the macro silently
    /// for the parameter, set the <see cref="MacroAttribute.Editable"/> property value to -1.
    /// </remarks>
    /// <example>
    /// Applying the attribute to a source template method:
    /// <code>
    /// [SourceTemplate, Macro(Target = "item", Expression = "suggestVariableName()")]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; collection) {
    ///   foreach (var item in collection) {
    ///     //$ $END$
    ///   }
    /// }
    /// </code>
    /// Applying the attribute to a template method parameter:
    /// <code>
    /// [SourceTemplate]
    /// public static void something(this Entity x, [Macro(Expression = "guid()", Editable = -1)] string newguid) {
    ///   /*$ var $x$Id = "$newguid$" + x.ToString();
    ///   x.DoSomething($x$Id); */
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class MacroAttribute : Attribute
    {
        /// <summary>
        /// Allows specifying a macro that will be executed for a <see cref="SourceTemplateAttribute">source template</see>
        /// parameter when the template is expanded.
        /// </summary>
        [CanBeNull]
        public string Expression { get; set; }

        /// <summary>
        /// Allows specifying the occurrence of the target parameter that becomes editable when the template is deployed.
        /// </summary>
        /// <remarks>
        /// If the target parameter is used several times in the template, only one occurrence becomes editable;
        /// other occurrences are changed synchronously. To specify the zero-based index of the editable occurrence,
        /// use values >= 0. To make the parameter non-editable when the template is expanded, use -1.
        /// </remarks>
        public int Editable { get; set; }

        /// <summary>
        /// Identifies the target parameter of a <see cref="SourceTemplateAttribute">source template</see> if the
        /// <see cref="MacroAttribute"/> is applied to a template method.
        /// </summary>
        [CanBeNull]
        public string Target { get; set; }
    }

    /// <summary>
    /// Indicates how a method, constructor invocation, or property access
    /// over a collection type affects the contents of the collection.
    /// When applied to a return value of a method, indicates whether the returned collection
    /// is created exclusively for the caller (<c>CollectionAccessType.UpdatedContent</c>) or
    /// can be read/updated from outside (<c>CollectionAccessType.Read</c>/<c>CollectionAccessType.UpdatedContent</c>).
    /// Use <see cref="CollectionAccessType"/> to specify the access type.
    /// </summary>
    /// <remarks>
    /// Using this attribute only makes sense if all collection methods are marked with this attribute.
    /// </remarks>
    /// <example><code>
    /// public class MyStringCollection : List&lt;string&gt;
    /// {
    ///   [CollectionAccess(CollectionAccessType.Read)]
    ///   public string GetFirstString()
    ///   {
    ///     return this.ElementAt(0);
    ///   }
    /// }
    /// class Test
    /// {
    ///   public void Foo()
    ///   {
    ///     // Warning: Contents of the collection is never updated
    ///     var col = new MyStringCollection();
    ///     string x = col.GetFirstString();
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property |
                    AttributeTargets.ReturnValue)]
    internal sealed class CollectionAccessAttribute : Attribute
    {
        public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
        {
            CollectionAccessType = collectionAccessType;
        }

        public CollectionAccessType CollectionAccessType { get; }
    }

    /// <summary>
    /// Provides a value for the <see cref="CollectionAccessAttribute"/> to define
    /// how the collection method invocation affects the contents of the collection.
    /// </summary>
    [Flags]
    internal enum CollectionAccessType
    {
        /// <summary>Method does not use or modify content of the collection.</summary>
        None = 0,

        /// <summary>Method only reads content of the collection but does not modify it.</summary>
        Read = 1,

        /// <summary>Method can change content of the collection but does not add new elements.</summary>
        ModifyExistingContent = 2,

        /// <summary>Method can add new elements to the collection.</summary>
        UpdatedContent = ModifyExistingContent | 4
    }

    /// <summary>
    /// Indicates that the marked method is an assertion method, i.e. it halts the control flow if
    /// one of the conditions is satisfied. To set the condition, mark one of the parameters with
    /// <see cref="AssertionConditionAttribute"/> attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class AssertionMethodAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates the condition parameter of the assertion method. The method itself should be
    /// marked by the <see cref="AssertionMethodAttribute"/> attribute. The mandatory argument of
    /// the attribute is the assertion type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class AssertionConditionAttribute : Attribute
    {
        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            ConditionType = conditionType;
        }

        public AssertionConditionType ConditionType { get; }
    }

    /// <summary>
    /// Specifies the assertion type. If the assertion method argument satisfies the condition,
    /// then the execution continues. Otherwise, execution is assumed to be halted.
    /// </summary>
    internal enum AssertionConditionType
    {
        /// <summary>Marked parameter should be evaluated to true.</summary>
        IS_TRUE = 0,

        /// <summary>Marked parameter should be evaluated to false.</summary>
        IS_FALSE = 1,

        /// <summary>Marked parameter should be evaluated to null value.</summary>
        IS_NULL = 2,

        /// <summary>Marked parameter should be evaluated to not null value.</summary>
        IS_NOT_NULL = 3,
    }

    /// <summary>
    /// Indicates that the marked method unconditionally terminates control flow execution.
    /// For example, it could unconditionally throw an exception.
    /// </summary>
    [Obsolete("Use [ContractAnnotation('=> halt')] instead")]
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class TerminatesProgramAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the method is a pure LINQ method with postponed enumeration (like <c>Enumerable.Select</c> or
    /// <c>Enumerable.Where</c>). This annotation allows inference of the <c>[InstantHandle]</c> annotation for parameters
    /// of delegate type by analyzing LINQ method chains.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class LinqTunnelAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that an <c>IEnumerable</c> passed as a parameter is not enumerated.
    /// Use this annotation to suppress the 'Possible multiple enumeration of IEnumerable' inspection.
    /// </summary>
    /// <example><code>
    /// static void ThrowIfNull&lt;T&gt;([NoEnumeration] T v, string n) where T : class
    /// {
    ///   // custom check for null but no enumeration
    /// }
    ///
    /// void Foo(IEnumerable&lt;string&gt; values)
    /// {
    ///   ThrowIfNull(values, nameof(values));
    ///   var x = values.ToList(); // No warnings about multiple enumeration
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class NoEnumerationAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked parameter, field, or property is a regular expression pattern.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class RegexPatternAttribute : Attribute
    {
    }

    /// <summary>
    /// Language of the injected code fragment inside a string literal marked by the <see cref="LanguageInjectionAttribute"/>.
    /// </summary>
    internal enum InjectedLanguage
    {
        CSS = 0,
        HTML = 1,
        JAVASCRIPT = 2,
        JSON = 3,
        XML = 4
    }

    /// <summary>
    /// Indicates that the marked parameter, field, or property accepts string literals
    /// containing code fragments in a specified language.
    /// </summary>
    /// <example><code>
    /// void Foo([LanguageInjection(InjectedLanguage.CSS, Prefix = "body{", Suffix = "}")] string cssProps)
    /// {
    ///   // cssProps should only contain a list of CSS properties
    /// }
    /// </code></example>
    /// <example><code>
    /// void Bar([LanguageInjection("json")] string json)
    /// {
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.ReturnValue)]
    internal sealed class LanguageInjectionAttribute : Attribute
    {
        public LanguageInjectionAttribute(InjectedLanguage injectedLanguage)
        {
            InjectedLanguage = injectedLanguage;
        }

        public LanguageInjectionAttribute([NotNull] string injectedLanguage)
        {
            InjectedLanguageName = injectedLanguage;
        }

        /// <summary>Specifies a language of the injected code fragment.</summary>
        public InjectedLanguage InjectedLanguage { get; }

        /// <summary>Specifies a language name of the injected code fragment.</summary>
        [CanBeNull]
        public string InjectedLanguageName { get; }

        /// <summary>Specifies a string that 'precedes' the injected string literal.</summary>
        [CanBeNull]
        public string Prefix { get; set; }

        /// <summary>Specifies a string that 'follows' the injected string literal.</summary>
        [CanBeNull]
        public string Suffix { get; set; }
    }

    /// <summary>
    /// Prevents the Member Reordering feature in the IDE from tossing members of the marked class.
    /// </summary>
    /// <remarks>
    /// The attribute must be mentioned in your member reordering patterns.
    /// </remarks>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum,
        AllowMultiple = true)]
    internal sealed class NoReorderAttribute : Attribute
    {
    }

    /// <summary>
    /// Defines a code search pattern using the Structural Search and Replace syntax.
    /// It allows you to find and, if necessary, replace blocks of code that match a specific pattern.
    /// </summary>
    /// <remarks>
    /// Search and replace patterns consist of a textual part and placeholders.
    /// Textural part must contain only identifiers allowed in the target language and will be matched exactly
    /// (whitespaces, tabulation characters, and line breaks are ignored).
    /// Placeholders allow matching variable parts of the target code blocks.
    /// <br/>
    /// A placeholder has the following format:
    /// <c>$placeholder_name$</c> - where <c>placeholder_name</c> is an arbitrary identifier.
    /// Predefined placeholders:
    /// <list type="bullet">
    /// <item><c>$this$</c> - expression of containing type</item>
    /// <item><c>$thisType$</c> - containing type</item>
    /// <item><c>$member$</c> - current member placeholder</item>
    /// <item><c>$qualifier$</c> - this placeholder is available in the replace pattern and can be used
    /// to insert a qualifier expression matched by the <c>$member$</c> placeholder.
    /// (Note that if <c>$qualifier$</c> placeholder is used,
    /// then <c>$member$</c> placeholder will match only qualified references)</item>
    /// <item><c>$expression$</c> - expression of any type</item>
    /// <item><c>$identifier$</c> - identifier placeholder</item>
    /// <item><c>$args$</c> - any number of arguments</item>
    /// <item><c>$arg$</c> - single argument</item>
    /// <item><c>$arg1$ ... $arg10$</c> - single argument</item>
    /// <item><c>$stmts$</c> - any number of statements</item>
    /// <item><c>$stmt$</c> - single statement</item>
    /// <item><c>$stmt1$ ... $stmt10$</c> - single statement</item>
    /// <item><c>$name{Expression, 'Namespace.FooType'}$</c> - expression with the <c>Namespace.FooType</c> type</item>
    /// <item><c>$expression{'Namespace.FooType'}$</c> - expression with the <c>Namespace.FooType</c> type</item>
    /// <item><c>$name{Type, 'Namespace.FooType'}$</c> - <c>Namespace.FooType</c> type</item>
    /// <item><c>$type{'Namespace.FooType'}$</c> - <c>Namespace.FooType</c> type</item>
    /// <item><c>$statement{1,2}$</c> - 1 or 2 statements</item>
    /// </list>
    /// You can also define your own placeholders of the supported types and specify arguments for each placeholder type.
    /// This can be done using the following format: <c>$name{type, arguments}$</c>. Where
    /// <c>name</c> - is the name of your placeholder,
    /// <c>type</c> - is the type of your placeholder
    /// (one of the following: Expression, Type, Identifier, Statement, Argument, Member),
    /// <c>arguments</c> - a list of arguments for your placeholder. Each placeholder type supports its own arguments.
    /// Check the examples below for more details.
    /// The placeholder type may be omitted and determined from the placeholder name,
    /// if the name has one of the following prefixes:
    /// <list type="bullet">
    /// <item>expr, expression - expression placeholder, e.g. <c>$exprPlaceholder{}$</c>, <c>$expressionFoo{}$</c></item>
    /// <item>arg, argument - argument placeholder, e.g. <c>$argPlaceholder{}$</c>, <c>$argumentFoo{}$</c></item>
    /// <item>ident, identifier - identifier placeholder, e.g. <c>$identPlaceholder{}$</c>, <c>$identifierFoo{}$</c></item>
    /// <item>stmt, statement - statement placeholder, e.g. <c>$stmtPlaceholder{}$</c>, <c>$statementFoo{}$</c></item>
    /// <item>type - type placeholder, e.g. <c>$typePlaceholder{}$</c>, <c>$typeFoo{}$</c></item>
    /// <item>member - member placeholder, e.g. <c>$memberPlaceholder{}$</c>, <c>$memberFoo{}$</c></item>
    /// </list>
    /// </remarks>
    /// <para>
    /// Expression placeholder arguments:
    /// <list type="bullet">
    /// <item>expressionType - string value in single quotes, specifies full type name to match
    /// (empty string by default)</item>
    /// <item>exactType - boolean value, specifies if expression should have exact type match (false by default)</item>
    /// </list>
    /// Examples:
    /// <list type="bullet">
    /// <item><c>$myExpr{Expression, 'Namespace.FooType', true}$</c> - defines an expression placeholder
    /// matching expressions of the <c>Namespace.FooType</c> type with exact matching.</item>
    /// <item><c>$myExpr{Expression, 'Namespace.FooType'}$</c> - defines an expression placeholder
    /// matching expressions of the <c>Namespace.FooType</c> type or expressions that can be
    /// implicitly converted to <c>Namespace.FooType</c>.</item>
    /// <item><c>$myExpr{Expression}$</c> - defines an expression placeholder matching expressions of any type.</item>
    /// <item><c>$exprFoo{'Namespace.FooType', true}$</c> - defines an expression placeholder
    /// matching expressions of the <c>Namespace.FooType</c> type with exact matching.</item>
    /// </list>
    /// </para>
    /// <para>
    /// Type placeholder arguments:
    /// <list type="bullet">
    /// <item>type - string value in single quotes, specifies the full type name to match (empty string by default)</item>
    /// <item>exactType - boolean value, specifies whether the expression should have the exact type match
    /// (false by default)</item>
    /// </list>
    /// Examples:
    /// <list type="bullet">
    /// <item><c>$myType{Type, 'Namespace.FooType', true}$</c> - defines a type placeholder
    /// matching <c>Namespace.FooType</c> types with exact matching.</item>
    /// <item><c>$myType{Type, 'Namespace.FooType'}$</c> - defines a type placeholder matching <c>Namespace.FooType</c>
    /// types or types that can be implicitly converted to <c>Namespace.FooType</c>.</item>
    /// <item><c>$myType{Type}$</c> - defines a type placeholder matching any type.</item>
    /// <item><c>$typeFoo{'Namespace.FooType', true}$</c> - defines a type placeholder matching <c>Namespace.FooType</c>
    /// types with exact matching.</item>
    /// </list>
    /// </para>
    /// <para>
    /// Identifier placeholder arguments:
    /// <list type="bullet">
    /// <item>nameRegex - string value in single quotes, specifies regex to use for matching (empty string by default)</item>
    /// <item>nameRegexCaseSensitive - boolean value, specifies if name regex is case-sensitive (true by default)</item>
    /// <item>type - string value in single quotes, specifies full type name to match (empty string by default)</item>
    /// <item>exactType - boolean value, specifies if expression should have exact type match (false by default)</item>
    /// </list>
    /// Examples:
    /// <list type="bullet">
    /// <item><c>$myIdentifier{Identifier, 'my.*', false, 'Namespace.FooType', true}$</c> -
    /// defines an identifier placeholder matching identifiers (ignoring case) starting with <c>my</c> prefix with
    /// <c>Namespace.FooType</c> type.</item>
    /// <item><c>$myIdentifier{Identifier, 'my.*', true, 'Namespace.FooType', true}$</c> -
    /// defines an identifier placeholder matching identifiers (case sensitively) starting with <c>my</c> prefix with
    /// <c>Namespace.FooType</c> type.</item>
    /// <item><c>$identFoo{'my.*'}$</c> - defines an identifier placeholder matching identifiers (case sensitively)
    /// starting with <c>my</c> prefix.</item>
    /// </list>
    /// </para>
    /// <para>
    /// Statement placeholder arguments:
    /// <list type="bullet">
    /// <item>minimalOccurrences - minimal number of statements to match (-1 by default)</item>
    /// <item>maximalOccurrences - maximal number of statements to match (-1 by default)</item>
    /// </list>
    /// Examples:
    /// <list type="bullet">
    /// <item><c>$myStmt{Statement, 1, 2}$</c> - defines a statement placeholder matching 1 or 2 statements.</item>
    /// <item><c>$myStmt{Statement}$</c> - defines a statement placeholder matching any number of statements.</item>
    /// <item><c>$stmtFoo{1, 2}$</c> - defines a statement placeholder matching 1 or 2 statements.</item>
    /// </list>
    /// </para>
    /// <para>
    /// Argument placeholder arguments:
    /// <list type="bullet">
    /// <item>minimalOccurrences - minimal number of arguments to match (-1 by default)</item>
    /// <item>maximalOccurrences - maximal number of arguments to match (-1 by default)</item>
    /// </list>
    /// Examples:
    /// <list type="bullet">
    /// <item><c>$myArg{Argument, 1, 2}$</c> - defines an argument placeholder matching 1 or 2 arguments.</item>
    /// <item><c>$myArg{Argument}$</c> - defines an argument placeholder matching any number of arguments.</item>
    /// <item><c>$argFoo{1, 2}$</c> - defines an argument placeholder matching 1 or 2 arguments.</item>
    /// </list>
    /// </para>
    /// <para>
    /// Member placeholder arguments:
    /// <list type="bullet">
    /// <item>docId - string value in single quotes, specifies XML documentation ID of the member to match (empty by default)</item>
    /// </list>
    /// Examples:
    /// <list type="bullet">
    /// <item><c>$myMember{Member, 'M:System.String.IsNullOrEmpty(System.String)'}$</c> -
    /// defines a member placeholder matching <c>IsNullOrEmpty</c> member of the <c>System.String</c> type.</item>
    /// <item><c>$memberFoo{'M:System.String.IsNullOrEmpty(System.String)'}$</c> -
    /// defines a member placeholder matching <c>IsNullOrEmpty</c> member of the <c>System.String</c> type.</item>
    /// </list>
    /// </para>
    /// <seealso href="https://www.jetbrains.com/help/resharper/Navigation_and_Search__Structural_Search_and_Replace.html">
    /// Structural Search and Replace</seealso>
    /// <seealso href="https://www.jetbrains.com/help/resharper/Code_Analysis__Find_and_Update_Obsolete_APIs.html">
    /// Find and update deprecated APIs</seealso>
    [AttributeUsage(
        AttributeTargets.Method
        | AttributeTargets.Constructor
        | AttributeTargets.Property
        | AttributeTargets.Field
        | AttributeTargets.Event
        | AttributeTargets.Interface
        | AttributeTargets.Class
        | AttributeTargets.Struct
        | AttributeTargets.Enum,
        AllowMultiple = true,
        Inherited = false)]
    internal sealed class CodeTemplateAttribute : Attribute
    {
        public CodeTemplateAttribute(string searchTemplate)
        {
            SearchTemplate = searchTemplate;
        }

        /// <summary>
        /// Structural search pattern.
        /// </summary>
        /// <remarks>
        /// The pattern includes a textual part, which must only contain identifiers allowed in the target language
        /// and placeholders to match variable parts of the target code blocks.
        /// </remarks>
        public string SearchTemplate { get; }

        /// <summary>
        /// Message to show when a code block matching the search pattern was found.
        /// </summary>
        /// <remarks>
        /// You can also prepend the message text with 'Error:', 'Warning:', 'Suggestion:' or 'Hint:' prefix
        /// to specify the pattern severity.
        /// Code patterns with replace templates have the 'Suggestion' severity by default.
        /// If a replace pattern is not provided, the pattern will have the 'Warning' severity.
        ///</remarks>
        public string Message { get; set; }

        /// <summary>
        /// Replace pattern to use for replacing a matched pattern.
        /// </summary>
        public string ReplaceTemplate { get; set; }

        /// <summary>
        /// Replace message to show in the light bulb.
        /// </summary>
        public string ReplaceMessage { get; set; }

        /// <summary>
        /// Apply code formatting after code replacement.
        /// </summary>
        public bool FormatAfterReplace { get; set; } = true;

        /// <summary>
        /// Whether similar code blocks should be matched.
        /// </summary>
        public bool MatchSimilarConstructs { get; set; }

        /// <summary>
        /// Automatically insert namespace import directives or remove qualifiers
        /// that become redundant after the template is applied.
        /// </summary>
        public bool ShortenReferences { get; set; }

        /// <summary>
        /// The string to use as a suppression key.
        /// By default, the following suppression key is used: <c>CodeTemplate_SomeType_SomeMember</c>,
        /// where 'SomeType' and 'SomeMember' are names of the associated containing type and member,
        /// to which this attribute is applied.
        /// </summary>
        public string SuppressionKey { get; set; }
    }

    /// <summary>
    /// Indicates that the string literal passed as an argument to this parameter
    /// should not be checked for spelling or grammar errors.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class IgnoreSpellingAndGrammarErrorsAttribute : Attribute
    {
    }

    #region ASP.NET

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class AspChildControlTypeAttribute : Attribute
    {
        public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
        {
            TagName = tagName;
            ControlType = controlType;
        }

        [NotNull] public string TagName { get; }

        [NotNull] public Type ControlType { get; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    internal sealed class AspDataFieldAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    internal sealed class AspDataFieldsAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class AspMethodPropertyAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal sealed class AspRequiredAttributeAttribute : Attribute
    {
        public AspRequiredAttributeAttribute([NotNull] string attribute)
        {
            Attribute = attribute;
        }

        [NotNull] public string Attribute { get; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class AspTypePropertyAttribute : Attribute
    {
        public bool CreateConstructorReferences { get; }

        public AspTypePropertyAttribute(bool createConstructorReferences)
        {
            CreateConstructorReferences = createConstructorReferences;
        }
    }

    #endregion

    #region ASP.NET MVC

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
    {
        public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcAreaViewComponentViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaViewComponentViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcMasterLocationFormatAttribute : Attribute
    {
        public AspMvcMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
    {
        public AspMvcPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcViewComponentViewLocationFormatAttribute : Attribute
    {
        public AspMvcViewComponentViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    internal sealed class AspMvcViewLocationFormatAttribute : Attribute
    {
        public AspMvcViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC action. If applied to a method, the MVC action name is calculated
    /// implicitly from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Field |
                    AttributeTargets.Property)]
    internal sealed class AspMvcActionAttribute : Attribute
    {
        public AspMvcActionAttribute()
        {
        }

        public AspMvcActionAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        [CanBeNull] public string AnonymousProperty { get; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that the marked parameter is an MVC area.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class AspMvcAreaAttribute : Attribute
    {
        public AspMvcAreaAttribute()
        {
        }

        public AspMvcAreaAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        [CanBeNull] public string AnonymousProperty { get; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is
    /// an MVC controller. If applied to a method, the MVC controller name is calculated
    /// implicitly from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Field |
                    AttributeTargets.Property)]
    internal sealed class AspMvcControllerAttribute : Attribute
    {
        public AspMvcControllerAttribute()
        {
        }

        public AspMvcControllerAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        [CanBeNull] public string AnonymousProperty { get; }
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that the marked parameter is an MVC Master. Use this attribute
    /// for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class AspMvcMasterAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that the marked parameter is an MVC model type. Use this attribute
    /// for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, Object)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class AspMvcModelTypeAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC
    /// partial view. If applied to a method, the MVC partial view name is calculated implicitly
    /// from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Field |
                    AttributeTargets.Property)]
    internal sealed class AspMvcPartialViewAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. Allows disabling inspections for MVC views within a class or a method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal sealed class AspMvcSuppressViewErrorAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class AspMvcDisplayTemplateAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that the marked parameter is an MVC editor template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class AspMvcEditorTemplateAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. Indicates that the marked parameter is an MVC template.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class AspMvcTemplateAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view component. If applied to a method, the MVC view name is calculated implicitly
    /// from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Controller.View(Object)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Field |
                    AttributeTargets.Property)]
    internal sealed class AspMvcViewAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view component name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class AspMvcViewComponentAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view component view. If applied to a method, the MVC view component view name is default.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Field |
                    AttributeTargets.Property)]
    internal sealed class AspMvcViewComponentViewAttribute : Attribute
    {
    }

    /// <summary>
    /// ASP.NET MVC attribute. When applied to a parameter of an attribute,
    /// indicates that this parameter is an MVC action name.
    /// </summary>
    /// <example><code>
    /// [ActionName("Foo")]
    /// public ActionResult Login(string returnUrl) {
    ///   ViewBag.ReturnUrl = Url.Action("Foo"); // OK
    ///   return RedirectToAction("Bar"); // Error: Cannot resolve action
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    internal sealed class AspMvcActionSelectorAttribute : Attribute
    {
    }

    #endregion

    #region ASP.NET Routing

    /// <summary>
    /// Indicates that the marked parameter, field, or property is a route template.
    /// </summary>
    /// <remarks>
    /// This attribute allows IDE to recognize the use of web frameworks' route templates
    /// to enable syntax highlighting, code completion, navigation, rename and other features in string literals.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class RouteTemplateAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked type is custom route parameter constraint,
    /// which is registered in the application's Startup with the name <c>ConstraintName</c>.
    /// </summary>
    /// <remarks>
    /// You can specify <c>ProposedType</c> if target constraint matches only route parameters of specific type,
    /// it will allow IDE to create method's parameter from usage in route template
    /// with specified type instead of default <c>System.String</c>
    /// and check if constraint's proposed type conflicts with matched parameter's type.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class RouteParameterConstraintAttribute : Attribute
    {
        [NotNull] public string ConstraintName { get; }
        [CanBeNull] public Type ProposedType { get; set; }

        public RouteParameterConstraintAttribute([NotNull] string constraintName)
        {
            ConstraintName = constraintName;
        }
    }

    /// <summary>
    /// Indicates that the marked parameter, field, or property is a URI string.
    /// </summary>
    /// <remarks>
    /// This attribute enables code completion, navigation, renaming, and other features
    /// in URI string literals assigned to annotated parameters, fields, or properties.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class UriStringAttribute : Attribute
    {
        public UriStringAttribute()
        {
        }

        public UriStringAttribute(string httpVerb)
        {
            HttpVerb = httpVerb;
        }

        [CanBeNull] public string HttpVerb { get; }
    }

    /// <summary>
    /// Indicates that the marked method declares routing convention for ASP.NET.
    /// </summary>
    /// <remarks>
    /// The IDE will analyze all usages of methods marked with this attribute,
    /// and will add all routes to completion, navigation, and other features over URI strings.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class AspRouteConventionAttribute : Attribute
    {
        public AspRouteConventionAttribute()
        {
        }

        public AspRouteConventionAttribute(string predefinedPattern)
        {
            PredefinedPattern = predefinedPattern;
        }

        [CanBeNull] public string PredefinedPattern { get; }
    }

    /// <summary>
    /// Indicates that the marked method parameter contains default route values of routing convention for ASP.NET.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class AspDefaultRouteValuesAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked method parameter contains constraints on route values of routing convention for ASP.NET.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class AspRouteValuesConstraintsAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked parameter or property contains routing order provided by ASP.NET routing attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    internal sealed class AspRouteOrderAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked parameter or property contains HTTP verbs provided by ASP.NET routing attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    internal sealed class AspRouteVerbsAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked attribute is used for attribute routing in ASP.NET.
    /// </summary>
    /// <remarks>
    /// The IDE will analyze all usages of attributes marked with this attribute,
    /// and will add all routes to completion, navigation and other features over URI strings.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class AspAttributeRoutingAttribute : Attribute
    {
        public string HttpVerb { get; set; }
    }

    /// <summary>
    /// Indicates that the marked method declares an ASP.NET Minimal API endpoint.
    /// </summary>
    /// <remarks>
    /// The IDE will analyze all usages of methods marked with this attribute,
    /// and will add all routes to completion, navigation and other features over URI strings.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class AspMinimalApiDeclarationAttribute : Attribute
    {
        public string HttpVerb { get; set; }
    }

    /// <summary>
    /// Indicates that the marked method declares an ASP.NET Minimal API endpoints group.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class AspMinimalApiGroupAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked parameter contains an ASP.NET Minimal API endpoint handler.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class AspMinimalApiHandlerAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates that the marked method contains Minimal API endpoint declaration.
    /// </summary>
    /// <remarks>
    /// The IDE will analyze all usages of methods marked with this attribute,
    /// and will add all declared in attributes routes to completion, navigation and other features over URI strings.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class AspMinimalApiImplicitEndpointDeclarationAttribute : Attribute
    {
        public string HttpVerb { get; set; }

        public string RouteTemplate { get; set; }

        public Type BodyType { get; set; }

        /// <summary>
        /// Comma-separated list of query parameters defined for endpoint
        /// </summary>
        public string QueryParameters { get; set; }
    }

    #endregion

    #region Razor

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
    internal sealed class HtmlElementAttributesAttribute : Attribute
    {
        public HtmlElementAttributesAttribute()
        {
        }

        public HtmlElementAttributesAttribute([NotNull] string name)
        {
            Name = name;
        }

        [CanBeNull] public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class HtmlAttributeValueAttribute : Attribute
    {
        public HtmlAttributeValueAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull] public string Name { get; }
    }

    /// <summary>
    /// Razor attribute. Indicates that the marked parameter or method is a Razor section.
    /// Use this attribute for custom wrappers similar to
    /// <c>System.Web.WebPages.WebPageBase.RenderSection(String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    internal sealed class RazorSectionAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class RazorImportNamespaceAttribute : Attribute
    {
        public RazorImportNamespaceAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull] public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class RazorInjectionAttribute : Attribute
    {
        public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
        {
            Type = type;
            FieldName = fieldName;
        }

        [NotNull] public string Type { get; }

        [NotNull] public string FieldName { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class RazorDirectiveAttribute : Attribute
    {
        public RazorDirectiveAttribute([NotNull] string directive)
        {
            Directive = directive;
        }

        [NotNull] public string Directive { get; }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class RazorPageBaseTypeAttribute : Attribute
    {
        public RazorPageBaseTypeAttribute([NotNull] string baseType)
        {
            BaseType = baseType;
        }

        public RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
        {
            BaseType = baseType;
            PageName = pageName;
        }

        [NotNull] public string BaseType { get; }
        [CanBeNull] public string PageName { get; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class RazorHelperCommonAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class RazorLayoutAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class RazorWriteLiteralMethodAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class RazorWriteMethodAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class RazorWriteMethodParameterAttribute : Attribute
    {
    }

    #endregion

    #region XAML

    /// <summary>
    /// XAML attribute. Indicates the type that has an <c>ItemsSource</c> property and should be treated
    /// as an <c>ItemsControl</c>-derived type, to enable inner items <c>DataContext</c> type resolution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class XamlItemsControlAttribute : Attribute
    {
    }

    /// <summary>
    /// XAML attribute. Indicates the property of some <c>BindingBase</c>-derived type, that
    /// is used to bind some item of an <c>ItemsControl</c>-derived type. This annotation will
    /// enable the <c>DataContext</c> type resolution for XAML bindings for such properties.
    /// </summary>
    /// <remarks>
    /// The property should have a tree ancestor of the <c>ItemsControl</c> type or
    /// marked with the <see cref="XamlItemsControlAttribute"/> attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class XamlItemBindingOfItemsControlAttribute : Attribute
    {
    }

    /// <summary>
    /// XAML attribute. Indicates the property of some <c>Style</c>-derived type that
    /// is used to style items of an <c>ItemsControl</c>-derived type. This annotation will
    /// enable the <c>DataContext</c> type resolution in XAML bindings for such properties.
    /// </summary>
    /// <remarks>
    /// Property should have a tree ancestor of the <c>ItemsControl</c> type or
    /// marked with the <see cref="XamlItemsControlAttribute"/> attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class XamlItemStyleOfItemsControlAttribute : Attribute
    {
    }

    /// <summary>
    /// XAML attribute. Indicates that DependencyProperty has <c>OneWay</c> binding mode by default.
    /// </summary>
    /// <remarks>
    /// This attribute must be applied to DependencyProperty's CLR accessor property if it is present, or to a DependencyProperty descriptor field otherwise.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class XamlOneWayBindingModeByDefaultAttribute : Attribute
    {
    }

    /// <summary>
    /// XAML attribute. Indicates that DependencyProperty has <c>TwoWay</c> binding mode by default.
    /// </summary>
    /// <remarks>
    /// This attribute must be applied to DependencyProperty's CLR accessor property if it is present, or to a DependencyProperty descriptor field otherwise.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    internal sealed class XamlTwoWayBindingModeByDefaultAttribute : Attribute
    {
    }

    #endregion

    #region Unit Testing

    /// <summary>
    /// Specifies a type being tested by a test class or a test method.
    /// </summary>
    /// <remarks>
    /// This information can be used by the IDE to navigate between tests and tested types,
    /// or by test runners to group tests by subject and to provide better test reports.
    /// </remarks>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Interface,
        AllowMultiple = true)]
    internal sealed class TestSubjectAttribute : Attribute
    {
        /// <summary>
        /// Gets the type being tested.
        /// </summary>
        [NotNull]
        public Type Subject { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestSubjectAttribute"/> class with the specified tested type.
        /// </summary>
        /// <param name="subject">The type being tested.</param>
        public TestSubjectAttribute([NotNull] Type subject)
        {
            Subject = subject;
        }
    }

    /// <summary>
    /// Marks a generic argument as the test subject for a test class.
    /// </summary>
    /// <remarks>
    /// Can be applied to a generic parameter of a base test class to indicate that
    /// the type passed as the argument is the class being tested. This information can be used by the IDE
    /// to navigate between tests and tested types,
    /// or by test runners to group tests by subject and to provide better test reports.
    /// </remarks>
    /// <example><code>
    /// public class BaseTestClass&lt;[MeansTestSubject] T&gt;
    /// {
    ///   protected T Component { get; }
    /// }
    ///
    /// public class CalculatorAdditionTests : BaseTestClass&lt;Calculator&gt;
    /// {
    ///   [Test]
    ///   public void Should_add_two_numbers()
    ///   {
    ///      Assert.That(Component.Add(2, 3), Is.EqualTo(5));
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.GenericParameter)]
    internal sealed class MeansTestSubjectAttribute : Attribute
    {
    }

    #endregion
}
