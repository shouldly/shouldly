// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Core
{
    /// <summary>
    /// ParameterizedFixtureSuite serves as a container for the set of test 
    /// fixtures created from a given Type using various parameters.
    /// </summary>
    public class ParameterizedFixtureSuite : TestSuite
    {
        private Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterizedFixtureSuite"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public ParameterizedFixtureSuite(Type type)
            : base(type.Namespace, TypeHelper.GetDisplayName(type))
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the type of the test.
        /// </summary>
        /// <value>The type of the test.</value>
        public override string TestType
        {
            get
            {
#if CLR_2_0 || CLR_4_0
                if (type.IsGenericType)
                    return "GenericFixture";
#endif

                return "ParameterizedFixture";
            }
        }

        /// <summary>
        /// Gets the Type represented by this suite.
        /// </summary>
        /// <value>A Sysetm.Type.</value>
        public Type ParameterizedType
        {
            get { return type; }
        }
    }
}
