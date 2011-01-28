// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework
{
	using System;

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

#if NET_2_0
        private Type[] typeArgs;
        private bool argsSeparated;
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
        /// The arguments originally provided to the attribute
        /// </summary>
        public object[] Arguments
        {
            get 
            {
#if NET_2_0
                if (!argsSeparated)
                    SeparateArgs();
#endif
                return arguments; 
            }
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

#if NET_2_0
        /// <summary>
        /// Get or set the type arguments. If not set
        /// explicitly, any leading arguments that are
        /// Types are taken as type arguments.
        /// </summary>
        public Type[] TypeArgs
        {
            get
            {
                if (!argsSeparated)
                    SeparateArgs();

                return typeArgs;
            }
            set 
            { 
                typeArgs = value;
                argsSeparated = true;
            }
        }

        private void SeparateArgs()
        {
            int cnt = 0;
            if (arguments != null)
            {
                foreach (object o in arguments)
                    if (o is Type) cnt++;
                    else break;

                typeArgs = new Type[cnt];
                for (int i = 0; i < cnt; i++)
                    typeArgs[i] = (Type)arguments[i];

                if (cnt > 0)
                {
                    object[] args = new object[arguments.Length - cnt];
                    for (int i = 0; i < args.Length; i++)
                        args[i] = arguments[cnt + i];

                    arguments = args;
                }
            }
            else
                typeArgs = new Type[0];

            argsSeparated = true;
        }
#endif
	}
}
