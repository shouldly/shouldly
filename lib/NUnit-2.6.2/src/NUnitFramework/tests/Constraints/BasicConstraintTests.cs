// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class NullConstraintTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new NullConstraint();
            expectedDescription = "null";
            stringRepresentation = "<null>";
        }
        
        internal object[] SuccessData = new object[] { null };
        
        internal object[] FailureData = new object[] { "hello" };

        internal string[] ActualValues = new string[] { "\"hello\"" };
    }

    [TestFixture]
    public class TrueConstraintTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new TrueConstraint();
            expectedDescription = "True";
            stringRepresentation = "<true>";
        }
        
        internal object[] SuccessData = new object[] { true, 2+2==4 };
        
        internal object[] FailureData = new object[] { null, "hello", false, 2+2==5 };

        internal string[] ActualValues = new string[] { "null", "\"hello\"", "False", "False" };
    }

    [TestFixture]
    public class FalseConstraintTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new FalseConstraint();
            expectedDescription = "False";
            stringRepresentation = "<false>";
        }

        internal object[] SuccessData = new object[] { false, 2 + 2 == 5 };

        internal object[] FailureData = new object[] { null, "hello", true, 2+2==4 };

        internal string[] ActualValues = new string[] { "null", "\"hello\"", "True", "True" };
    }

    [TestFixture]
    public class NaNConstraintTest : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new NaNConstraint();
            expectedDescription = "NaN";
            stringRepresentation = "<nan>";
        }
        
        internal object[] SuccessData = new object[] { double.NaN, float.NaN };

        internal object[] FailureData = new object[] { null, "hello", 42, 
            double.PositiveInfinity, double.NegativeInfinity,
            float.PositiveInfinity, float.NegativeInfinity };

        internal string[] ActualValues = new string[] { "null", "\"hello\"", "42", 
            "Infinity", "-Infinity", "Infinity", "-Infinity" };
    }
}
