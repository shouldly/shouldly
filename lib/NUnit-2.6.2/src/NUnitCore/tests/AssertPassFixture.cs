// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class AssertPassFixture
    {
        [Test]
        public void AssertPassReturnsSuccess()
        {
            Assert.Pass("This test is OK!");
        }

        [Test]
        public void SubsequentFailureIsIrrelevant()
        {
            Assert.Pass("This test is OK!");
            Assert.Fail("No it's NOT!");
        }
    }

    [TestFixture]
    public class AssertInconclusiveFixture
    {
        [Test]
        public void AssertInconclusiveThrowsException()
        {
            Exception ex = Assert.Throws(
                typeof(InconclusiveException),
                new TestDelegate(InconclusiveTest));
            Assert.AreEqual("Unable to run test", ex.Message);
        }

        private static void InconclusiveTest()
        {
            Assert.Inconclusive("Unable to run test");
        }
    }
}
