// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Constraints
{
    public class PropertyExistsTest : ConstraintTestBaseWithExceptionTests
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new PropertyExistsConstraint("Length");
            expectedDescription = "property Length";
            stringRepresentation = "<propertyexists Length>";
        }

        internal static object[] SuccessData = new object[] { new int[0], "hello", typeof(Array) };

        internal static object[] FailureData = new object[] { 42, new System.Collections.ArrayList(), typeof(Int32) };

        internal static string[] ActualValues = new string[] { "<System.Int32>", "<System.Collections.ArrayList>", "<System.Int32>" };

        internal static object[] InvalidData = new TestCaseData[] 
        { 
            new TestCaseData(null).Throws(typeof(ArgumentNullException))
        };
    }

    public class PropertyTest : ConstraintTestBaseWithExceptionTests
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new PropertyConstraint("Length", new EqualConstraint(5));
            expectedDescription = "property Length equal to 5";
            stringRepresentation = "<property Length <equal 5>>";
        }

        internal static object[] SuccessData = new object[] { new int[5], "hello" };

        internal static object[] FailureData = new object[] { new int[3], "goodbye" };

        internal static string[] ActualValues = new string[] { "3", "7" };

        internal static object[] InvalidData = new object[] 
        { 
            new TestCaseData(null).Throws(typeof(ArgumentNullException)),
            new TestCaseData(42).Throws(typeof(ArgumentException)), 
            new TestCaseData(new System.Collections.ArrayList()).Throws(typeof(ArgumentException))
        };

        [Test]
        public void PropertyEqualToValueWithTolerance()
        {
            Constraint c = new EqualConstraint(105m).Within(0.1m);
            TextMessageWriter w = new TextMessageWriter();
            c.WriteDescriptionTo(w);
            Assert.That(w.ToString(), Is.EqualTo("105m +/- 0.1m"));

            c = new PropertyConstraint("D", new EqualConstraint(105m).Within(0.1m));
            w = new TextMessageWriter();
            c.WriteDescriptionTo(w);
            Assert.That(w.ToString(), Is.EqualTo("property D equal to 105m +/- 0.1m"));
        }
    }
}
