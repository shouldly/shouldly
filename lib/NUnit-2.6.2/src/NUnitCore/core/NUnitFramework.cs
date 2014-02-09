// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using NUnit.Core.Extensibility;

namespace NUnit.Core
{
	/// <summary>
	/// Static methods that implement aspects of the NUnit framework that cut 
	/// across individual test types, extensions, etc. Some of these use the 
	/// methods of the Reflect class to implement operations specific to the 
	/// NUnit Framework.
	/// </summary>
	public class NUnitFramework
	{
        #region Constants

		#region Attribute Names
		// NOTE: Attributes used in switch statements must be const

        // Attributes that apply to Assemblies, Classes and Methods
        public const string IgnoreAttribute = "NUnit.Framework.IgnoreAttribute";
		public const string PlatformAttribute = "NUnit.Framework.PlatformAttribute";
		public const string CultureAttribute = "NUnit.Framework.CultureAttribute";
		public const string ExplicitAttribute = "NUnit.Framework.ExplicitAttribute";
        public const string CategoryAttribute = "NUnit.Framework.CategoryAttribute";
        public const string PropertyAttribute = "NUnit.Framework.PropertyAttribute";
		public const string DescriptionAttribute = "NUnit.Framework.DescriptionAttribute";
        public const string RequiredAddinAttribute = "NUnit.Framework.RequiredAddinAttribute";

        // Attributes that apply only to Classes
        public const string TestFixtureAttribute = "NUnit.Framework.TestFixtureAttribute";
        public const string SetUpFixtureAttribute = "NUnit.Framework.SetUpFixtureAttribute";

        // Attributes that apply only to Methods
        public const string TestAttribute = "NUnit.Framework.TestAttribute";
        public const string TestCaseAttribute = "NUnit.Framework.TestCaseAttribute";
        public const string TestCaseSourceAttribute = "NUnit.Framework.TestCaseSourceAttribute";
        public const string TheoryAttribute = "NUnit.Framework.TheoryAttribute";
        public static readonly string SetUpAttribute = "NUnit.Framework.SetUpAttribute";
        public static readonly string TearDownAttribute = "NUnit.Framework.TearDownAttribute";
        public static readonly string FixtureSetUpAttribute = "NUnit.Framework.TestFixtureSetUpAttribute";
        public static readonly string FixtureTearDownAttribute = "NUnit.Framework.TestFixtureTearDownAttribute";
        public static readonly string ExpectedExceptionAttribute = "NUnit.Framework.ExpectedExceptionAttribute";

        // Attributes that apply only to Properties
        public static readonly string SuiteAttribute = "NUnit.Framework.SuiteAttribute";
        #endregion

        #region Other Framework Types

        public static readonly string TestActionInterface = "NUnit.Framework.ITestAction, nunit.framework";

	    public static readonly string TestDetailsClass = "NUnit.Framework.TestDetails, nunit.framework";

        public static readonly string AssertException = "NUnit.Framework.AssertionException";
        public static readonly string IgnoreException = "NUnit.Framework.IgnoreException";
        public static readonly string InconclusiveException = "NUnit.Framework.InconclusiveException";
        public static readonly string SuccessException = "NUnit.Framework.SuccessException";
        public static readonly string AssertType = "NUnit.Framework.Assert";
		public static readonly string ExpectExceptionInterface = "NUnit.Framework.IExpectException";
        #endregion

        #region Core Types
        public static readonly string SuiteBuilderAttribute = typeof(SuiteBuilderAttribute).FullName;
        public static readonly string SuiteBuilderInterface = typeof(ISuiteBuilder).FullName;

        public static readonly string TestCaseBuilderAttributeName = typeof(TestCaseBuilderAttribute).FullName;
        public static readonly string TestCaseBuilderInterfaceName = typeof(ITestCaseBuilder).FullName;

        public static readonly string TestDecoratorAttributeName = typeof(TestDecoratorAttribute).FullName;
        public static readonly string TestDecoratorInterfaceName = typeof(ITestDecorator).FullName;
        #endregion

        #endregion

        #region Properties
        private static Assembly frameworkAssembly;
        private static bool frameworkAssemblyInitialized;
        private static Assembly FrameworkAssembly
        {
            get
            {
                if (!frameworkAssemblyInitialized)
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                        if (assembly.GetName().Name == "nunit.framework" ||
                            assembly.GetName().Name == "NUnitLite")
                        {
                            frameworkAssembly = assembly;
                            break;
                        }

                frameworkAssemblyInitialized = true;

                return frameworkAssembly;
            }
        }
        #endregion

        #region Check SetUp and TearDown methods
        public static bool CheckSetUpTearDownMethods(Type fixtureType, string attributeName, ref string reason)
        {
            foreach( MethodInfo theMethod in Reflect.GetMethodsWithAttribute(fixtureType, attributeName, true ))
                if ( theMethod.IsAbstract ||
                     !theMethod.IsPublic && !theMethod.IsFamily ||
                     theMethod.GetParameters().Length > 0 ||
                     !theMethod.ReturnType.Equals(typeof(void)))
                {
                    reason = string.Format( "Invalid signature for SetUp or TearDown method: {0}", theMethod.Name );
                    return false;
                }

            return true;
        }
        #endregion

        #region Get Special Properties of Attributes

        #region IgnoreReason
        public static string GetIgnoreReason( System.Attribute attribute )
		{
            return Reflect.GetPropertyValue(attribute, PropertyNames.Reason) as string;
		}
		#endregion

		#region Description
		/// <summary>
		/// Method to return the description from an source
		/// </summary>
		/// <param name="source">The source to check</param>
		/// <returns>The description, if any, or null</returns>
		public static string GetDescription(System.Attribute attribute)
		{
            return Reflect.GetPropertyValue(attribute, PropertyNames.Description) as string;
		}
		#endregion

		#endregion

		#region ApplyCommonAttributes
        /// <summary>
        /// Modify a newly constructed test based on a type or method by 
        /// applying any of NUnit's common attributes.
        /// </summary>
        /// <param name="member">The type or method from which the test was constructed</param>
        /// <param name="test">The test to which the attributes apply</param>
        public static void ApplyCommonAttributes(MemberInfo member, Test test)
        {
            ApplyCommonAttributes( Reflect.GetAttributes( member, true ), test );
        }

        /// <summary>
        /// Modify a newly constructed test based on an assembly by applying 
        /// any of NUnit's common attributes.
        /// </summary>
        /// <param name="assembly">The assembly from which the test was constructed</param>
        /// <param name="test">The test to which the attributes apply</param>
        public static void ApplyCommonAttributes(Assembly assembly, Test test)
        {
            ApplyCommonAttributes( Reflect.GetAttributes( assembly, false ), test );
        }

        /// <summary>
        /// Modify a newly constructed test by applying any of NUnit's common
        /// attributes, based on an input array of attributes. This method checks
        /// for all attributes, relying on the fact that specific attributes can only
        /// occur on those constructs on which they are allowed.
        /// </summary>
        /// <param name="attributes">An array of attributes possibly including NUnit attributes
        /// <param name="test">The test to which the attributes apply</param>
        public static void ApplyCommonAttributes(Attribute[] attributes, Test test)
        {
            foreach (Attribute attribute in attributes)
            {
				Type attributeType = attribute.GetType();
				string attributeName = attributeType.FullName;
                bool isValid = test.RunState != RunState.NotRunnable;

                switch (attributeName)
                {
					case TestFixtureAttribute:
					case TestAttribute:
						if ( test.Description == null )
							test.Description = GetDescription( attribute );
						break;
					case DescriptionAttribute:
						test.Description = GetDescription( attribute );
						break;
                    case PlatformAttribute:
                        PlatformHelper pHelper = new PlatformHelper();
                        if (isValid && !pHelper.IsPlatformSupported(attribute))
                        {
                            test.RunState = RunState.Skipped;
                            test.IgnoreReason = GetIgnoreReason(attribute);
							if ( test.IgnoreReason == null )
								test.IgnoreReason = pHelper.Reason;
                        }
                        break;
					case CultureAttribute:
						CultureDetector cultureDetector = new CultureDetector();
						if (isValid && !cultureDetector.IsCultureSupported(attribute))
						{
							test.RunState = RunState.Skipped;
							test.IgnoreReason = cultureDetector.Reason;
						}
						break;
                    case RequiredAddinAttribute:
                        string required = (string)Reflect.GetPropertyValue(attribute, PropertyNames.RequiredAddin);
                        if (!IsAddinAvailable(required))
                        {
                            test.RunState = RunState.NotRunnable;
                            test.IgnoreReason = string.Format("Required addin {0} not available", required);
                        }
                        break;
                    case "System.STAThreadAttribute":
                        test.Properties.Add("APARTMENT_STATE", System.Threading.ApartmentState.STA);
                        break;
                    case "System.MTAThreadAttribute":
                        test.Properties.Add("APARTMENT_STATE", System.Threading.ApartmentState.MTA);
                        break;
                    default:
						if ( Reflect.InheritsFrom( attributeType, CategoryAttribute ) )
						{
                            string categoryName = (string)Reflect.GetPropertyValue(attribute, PropertyNames.CategoryName);
                            test.Categories.Add(categoryName);

                            if (!IsValidCategoryName(categoryName))
                            {
                                test.RunState = RunState.NotRunnable;
                                test.IgnoreReason = "Category name must not contain ',', '!', '+' or '-'";
                            }
                        }
						else if ( Reflect.InheritsFrom( attributeType, PropertyAttribute ) )
						{
							object propObject = Reflect.GetPropertyValue( attribute, PropertyNames.Properties );
                            IDictionary props = propObject as IDictionary;
                            if (props != null)
                                foreach (DictionaryEntry entry in props)
                                    test.Properties.Add(entry.Key, entry.Value);
                            else
                            {
                                // In case we are running NUnitLite tests
                                IEnumerable entries = propObject as IEnumerable;
                                if (entries != null)
                                    foreach (object entry in entries)
                                    {
                                        object key = Reflect.GetPropertyValue(entry, "Name");
                                        object value = Reflect.GetPropertyValue(entry, "Value");
                                        test.Properties.Add(key, value);
                                    }
                            }
						}
                        else if ( Reflect.InheritsFrom( attributeType, ExplicitAttribute ) )
                         {
                             if (isValid)
                             {
                                 test.RunState = RunState.Explicit;
                                 test.IgnoreReason = GetIgnoreReason(attribute);
                             }
                         }
                        else if (Reflect.InheritsFrom(attributeType, IgnoreAttribute))
                         {
                             if (isValid)
                             {
                                 test.RunState = RunState.Ignored;
                                 test.IgnoreReason = GetIgnoreReason(attribute);
                             }
                         }
						break;
                }
            }
        }
		#endregion

        #region ApplyExpectedExceptionAttribute
        /// <summary>
        /// Modify a newly constructed test by checking for ExpectedExceptionAttribute
        /// and setting properties on the test accordingly.
        /// </summary>
        /// <param name="attributes">An array of attributes possibly including NUnit attributes
        /// <param name="test">The test to which the attributes apply</param>
        public static void ApplyExpectedExceptionAttribute(MethodInfo method, TestMethod testMethod)
        {
            Attribute attribute = Reflect.GetAttribute(
                method, NUnitFramework.ExpectedExceptionAttribute, false);

            if (attribute != null)
                testMethod.ExceptionProcessor = new ExpectedExceptionProcessor(testMethod, attribute);
        }

        #endregion

        #region IsSuiteBuilder
        public static bool IsSuiteBuilder( Type type )
		{
			return Reflect.HasAttribute( type, SuiteBuilderAttribute, false )
				&& Reflect.HasInterface( type, SuiteBuilderInterface );
		}
		#endregion

		#region IsTestCaseBuilder
		public static bool IsTestCaseBuilder( Type type )
		{
			return Reflect.HasAttribute( type, TestCaseBuilderAttributeName, false )
				&& Reflect.HasInterface( type, TestCaseBuilderInterfaceName );
		}
		#endregion

		#region IsTestDecorator
		public static bool IsTestDecorator( Type type )
		{
			return Reflect.HasAttribute( type, TestDecoratorAttributeName, false )
				&& Reflect.HasInterface( type, TestDecoratorInterfaceName );
		}
		#endregion

        #region IsAddinAvailable
        public static bool IsAddinAvailable(string name)
        {
            foreach (Addin addin in CoreExtensions.Host.AddinRegistry.Addins)
                if (addin.Name == name && addin.Status == AddinStatus.Loaded)
                    return true;

            return false;
        }
        #endregion

        #region Framework Assert Access

        /// <summary>
        /// NUnitFramework.Assert is a nested class that implements
        /// a few of the framework operations by reflection, 
        /// using whatever framework version is available.
        /// </summary>
        public class Assert
        {
            #region Properties
            private static Type assertType;
            private static Type AssertType
            {
                get
                {
                    if (assertType == null && FrameworkAssembly != null)
                        assertType = FrameworkAssembly.GetType(NUnitFramework.AssertType);

                    return assertType;
                }
            }

            private static MethodInfo areEqualMethod;
            private static MethodInfo AreEqualMethod
            {
                get
                {
                    if (areEqualMethod == null && AssertType != null)
                        areEqualMethod = AssertType.GetMethod(
                            "AreEqual", 
                            BindingFlags.Static | BindingFlags.Public, 
                            null, 
                            new Type[] { typeof(object), typeof(object) },
                            null );

                    return areEqualMethod;
                }
            }

            private static PropertyInfo counterProperty;
            private static PropertyInfo CounterProperty
            {
                get
                {
                    if (counterProperty == null && AssertType != null)
                        counterProperty = Reflect.GetNamedProperty(
                            AssertType,
                            "Counter",
                            BindingFlags.Public | BindingFlags.Static);

                    return counterProperty;
                }
            }
            #endregion

            /// <summary>
            /// Invoke Assert.AreEqual by reflection
            /// </summary>
            /// <param name="expected">The expected value</param>
            /// <param name="actual">The actual value</param>
            public static void AreEqual(object expected, object actual)
            {
                if (AreEqualMethod != null)
                    try
                    {
                        AreEqualMethod.Invoke(null, new object[] { expected, actual });
                    }
                    catch (TargetInvocationException e)
                    {
                        Exception inner = e.InnerException;
                        throw new NUnitException("Rethrown", inner);
                    }
            }

            /// <summary>
            /// Get the assertion counter. It clears itself automatically
            /// on each call.
            /// </summary>
            /// <returns>Count of number of asserts since last call</returns>
            public static int GetAssertCount()
            {
                return CounterProperty == null
                    ? 0
                    : (int)CounterProperty.GetValue(null, new object[0]);
            }
        }

        #endregion

        #region GetResultState
        /// <summary>
        /// Returns a result state for a special exception.
        /// If the exception is not handled specially, returns
        /// ResultState.Error.
        /// </summary>
        /// <param name="ex">The exception to be examined</param>
        /// <returns>A ResultState</returns>
        public static ResultState GetResultState(Exception ex)
        {
            if (ex is System.Threading.ThreadAbortException)
                return ResultState.Cancelled;

            string name = ex.GetType().FullName;

            if (name == NUnitFramework.AssertException)
                return ResultState.Failure;
            else
                if (name == NUnitFramework.IgnoreException)
                    return ResultState.Ignored;
                else
                    if (name == NUnitFramework.InconclusiveException)
                        return ResultState.Inconclusive;
                    else
                        if (name == NUnitFramework.SuccessException)
                            return ResultState.Success;
                        else
                            return ResultState.Error;
        }
        #endregion

        #region IsValidCategoryName

        /// <summary>
        /// Returns true if the category name is valid
        /// </summary>
        public static bool IsValidCategoryName(string name)
        {
            return name.IndexOfAny(new char[] { ',', '!', '+', '-' }) < 0;
        }

        #endregion
    }
}
