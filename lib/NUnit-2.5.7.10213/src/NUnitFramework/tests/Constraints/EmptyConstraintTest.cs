// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class EmptyConstraintTest : ConstraintTestBaseWithArgumentException
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new EmptyConstraint();
            expectedDescription = "<empty>";
            stringRepresentation = "<empty>";
        }

        static object[] SuccessData = new object[] 
        {
            string.Empty,
            new object[0],
            new ArrayList(),
#if NET_2_0
            new System.Collections.Generic.List<int>()
#endif  
        };

        static object[] FailureData = new object[]
        {
            "Hello",
            new object[] { 1, 2, 3 }
        };

        static string[] ActualValues = new string[]
        {
            "\"Hello\"",
            "< 1, 2, 3 >"
        };

        static object[] InvalidData = new object[]
            {
                null,
                5
            };
    }

    [TestFixture]
    public class NullOrEmptyStringConstraintTest : ConstraintTestBaseWithArgumentException
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new NullOrEmptyStringConstraint();
            expectedDescription = "null or empty string";
            stringRepresentation = "<nullorempty>";
        }

        static object[] SuccessData = new object[] 
        {
            string.Empty,
            null
        };

        static object[] FailureData = new object[]
        {
            "Hello"
        };

        static string[] ActualValues = new string[]
        {
            "\"Hello\""
        };

        static object[] InvalidData = new object[]
            {
                5
            };
    }
}