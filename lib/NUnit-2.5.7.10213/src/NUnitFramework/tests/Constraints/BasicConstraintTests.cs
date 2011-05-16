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
        
        object[] SuccessData = new object[] { null };
        
        object[] FailureData = new object[] { "hello" };

        string[] ActualValues = new string[] { "\"hello\"" };
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
        
        object[] SuccessData = new object[] { true, 2+2==4 };
        
        object[] FailureData = new object[] { null, "hello", false, 2+2==5 };

        string[] ActualValues = new string[] { "null", "\"hello\"", "False", "False" };
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

        object[] SuccessData = new object[] { false, 2 + 2 == 5 };

        object[] FailureData = new object[] { null, "hello", true, 2+2==4 };

        string[] ActualValues = new string[] { "null", "\"hello\"", "True", "True" };
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
        
        object[] SuccessData = new object[] { double.NaN, float.NaN };

        object[] FailureData = new object[] { null, "hello", 42, 
            double.PositiveInfinity, double.NegativeInfinity,
            float.PositiveInfinity, float.NegativeInfinity };

        string[] ActualValues = new string[] { "null", "\"hello\"", "42", 
            "Infinity", "-Infinity", "Infinity", "-Infinity" };
    }
}
