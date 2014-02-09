// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.TestData
{
    [TestFixture(1)]
    [TestFixture(2)]
    public class ParameterizedTestFixture
    {
        [Test]
        public void MethodWithoutParams()
        {
        }

        [TestCase(10,20)]
        public void MethodWithParams(int x, int y)
        {
        }
    }

    [TestFixture(Category = "XYZ")]
    public class TestFixtureWithSingleCategory
    {
    }

    [TestFixture(Category = "X,Y,Z")]
    public class TestFixtureWithMultipleCategories
    {
    }
}
