// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework
{
	using System;
    using System.Collections;

	/// <example>
	/// [TestFixture]
	/// public class ExampleClass 
	/// {}
	/// </example>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
	public class TestFixtureAttribute : Attribute
	{
		private string description;

        private object[] arguments;
        private bool isIgnored;
        private string ignoreReason;
        private string category;

#if CLR_2_0 || CLR_4_0
        private Type[] typeArgs;
#endif

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestFixtureAttribute() : this( null ) { }
        
        /// <summary>
        /// Construct with a object[] representing a set of arguments. 
        /// In .NET 2.0, the arguments may later be separated into
        /// type arguments and constructor arguments.
        /// </summary>
        /// <param name="arguments"></param>
        public TestFixtureAttribute(params object[] arguments)
        {
            this.arguments = arguments == null
                ? new object[0]
                : arguments;

            for (int i = 0; i < this.arguments.Length; i++)
                if (arguments[i] is SpecialValue && (SpecialValue)arguments[i] == SpecialValue.Null)
                    arguments[i] = null;
        }

        /// <summary>
        /// Descriptive text for this fixture
        /// </summary>
        public string Description
		{
			get { return description; }
			set { description = value; }
		}

        /// <summary>
        /// Gets and sets the category for this fixture.
        /// May be a comma-separated list of categories.
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// Gets a list of categories for this fixture
        /// </summary>
        public IList Categories
        {
            get { return category == null ? null : category.Split(','); }
        }

        /// <summary>
        /// The arguments originally provided to the attribute
        /// </summary>
        public object[] Arguments
        {
            get { return arguments; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TestFixtureAttribute"/> should be ignored.
        /// </summary>
        /// <value><c>true</c> if ignore; otherwise, <c>false</c>.</value>
        public bool Ignore
        {
            get { return isIgnored; }
            set { isIgnored = value; }
        }

        /// <summary>
        /// Gets or sets the ignore reason. May set Ignored as a side effect.
        /// </summary>
        /// <value>The ignore reason.</value>
        public string IgnoreReason
        {
            get { return ignoreReason; }
            set
            {
                ignoreReason = value;
                isIgnored = ignoreReason != null && ignoreReason != string.Empty;
            }
        }

#if CLR_2_0 || CLR_4_0
        /// <summary>
        /// Get or set the type arguments. If not set
        /// explicitly, any leading arguments that are
        /// Types are taken as type arguments.
        /// </summary>
        public Type[] TypeArgs
        {
            get { return typeArgs; }
            set { typeArgs = value; }
        }
#endif
	}
}
