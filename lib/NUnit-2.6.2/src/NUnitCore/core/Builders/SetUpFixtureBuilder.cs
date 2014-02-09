// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;

namespace NUnit.Core.Builders
{
	/// <summary>
	/// SetUpFixtureBuilder knows how to build a SetUpFixture.
	/// </summary>
	public class SetUpFixtureBuilder : Extensibility.ISuiteBuilder
	{	
		#region ISuiteBuilder Members
		public Test BuildFrom(Type type)
		{
			SetUpFixture fixture = new SetUpFixture( type );

            string reason = null;
            if (!IsValidFixtureType(type, ref reason))
            {
                fixture.RunState = RunState.NotRunnable;
                fixture.IgnoreReason = reason;
            }

            return fixture;
		}

		public bool CanBuildFrom(Type type)
		{
			return Reflect.HasAttribute( type, NUnitFramework.SetUpFixtureAttribute, false );
		}
		#endregion

        private bool IsValidFixtureType(Type type, ref string reason)
        {
            if (type.IsAbstract)
            {
                reason = string.Format("{0} is an abstract class", type.FullName);
                return false;
            }

            if (Reflect.GetConstructor(type) == null)
            {
                reason = string.Format("{0} does not have a valid constructor", type.FullName);
                return false;
            }

            if (!NUnitFramework.CheckSetUpTearDownMethods(type, NUnitFramework.SetUpAttribute, ref reason) ||
                !NUnitFramework.CheckSetUpTearDownMethods(type, NUnitFramework.TearDownAttribute, ref reason) )
                    return false;

            if ( Reflect.HasMethodWithAttribute(type, NUnitFramework.FixtureSetUpAttribute, true) )
            {
                reason = "TestFixtureSetUp method not allowed on a SetUpFixture";
                return false;
            }

            if ( Reflect.HasMethodWithAttribute(type, NUnitFramework.FixtureTearDownAttribute, true) )
            {
                reason = "TestFixtureTearDown method not allowed on a SetUpFixture";
                return false;
            }

            return true;
        }
	}
}
