// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using NUnit.Framework;
using NUnit.TestData;
using NUnit.TestUtilities;
using System.Collections;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class ValueSourceTests
    {
        [Test]
        public void ValueSourceCanBeStaticProperty(
            [ValueSource("StaticProperty")] string source)
        {
            Assert.AreEqual("StaticProperty", source);
        }

        static IEnumerable StaticProperty
        {
            get
            {
#if CLR_2_0 || CLR_4_0
                yield return "StaticProperty";
#else
                return new object[] { "StaticProperty" }; 
#endif
            }
        }

        [Test]
        public void ValueSourceCanBeInstanceProperty(
            [ValueSource("InstanceProperty")] string source)
        {
            Assert.AreEqual("InstanceProperty", source);
        }

        IEnumerable InstanceProperty
        {
            get { return new object[] { "InstanceProperty" }; }
        }

        [Test]
        public void ValueSourceCanBeStaticMethod(
            [ValueSource("StaticMethod")] string source)
        {
            Assert.AreEqual("StaticMethod", source);
        }

        static IEnumerable StaticMethod()
        {
            return new object[] { "StaticMethod" };
        }

        [Test]
        public void ValueSourceCanBeInstanceMethod(
            [ValueSource("InstanceMethod")] string source)
        {
            Assert.AreEqual("InstanceMethod", source);
        }

        IEnumerable InstanceMethod()
        {
            return new object[] { "InstanceMethod" };
        }

        [Test]
        public void ValueSourceCanBeStaticField(
            [ValueSource("StaticField")] string source)
        {
            Assert.AreEqual("StaticField", source);
        }

        internal static object[] StaticField = { "StaticField" };

        [Test]
        public void ValueSourceCanBeInstanceField(
            [ValueSource("InstanceField")] string source)
        {
            Assert.AreEqual("InstanceField", source);
        }

        internal static object[] InstanceField = { "InstanceField" };

        [Test]
        public void ValueSourceIsInvokedWithCorrectCurrentDirectory(
            [ValueSource("CheckCurrentDirectory")] bool isOK)
        {
            Assert.That(isOK);
        }

        private static IEnumerable CheckCurrentDirectory
        {
            get
            {
                return new object[] { System.IO.File.Exists("nunit.core.tests.dll") };
            }
        }

        [Test, Sequential]
        public void MultipleArguments(
            [ValueSource("Numerators")] int n, 
            [ValueSource("Denominators")] int d, 
            [ValueSource("Quotients")] int q)
        {
            Assert.AreEqual(q, n / d);
        }

        internal static int[] Numerators = new int[] { 12, 12, 12 };
        internal static int[] Denominators = new int[] { 3, 4, 6 };
        internal static int[] Quotients = new int[] { 4, 3, 2 };

        [Test, Sequential]
        public void ValueSourceMayBeInAnotherClass(
            [ValueSource(typeof(DivideDataProvider), "Numerators")] int n,
            [ValueSource(typeof(DivideDataProvider), "Denominators")] int d,
            [ValueSource(typeof(DivideDataProvider), "Quotients")] int q)
        {
            Assert.AreEqual(q, n / d);
        }

        private class DivideDataProvider
        {
            internal static int[] Numerators = new int[] { 12, 12, 12 };
            internal static int[] Denominators = new int[] { 3, 4, 6 };
            internal static int[] Quotients = new int[] { 4, 3, 2 };
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void ValueSourceMayBeGeneric(
            [ValueSourceAttribute(typeof(ValueProvider), "IntegerProvider")] int val)
        {
            Assert.That(2 * val, Is.EqualTo(val + val));
        }

        public class ValueProvider
        {
            public IEnumerable<int> IntegerProvider()
            {
                List<int> dataList = new List<int>();

                dataList.Add(1);
                dataList.Add(2);
                dataList.Add(4);
                dataList.Add(8);

                return dataList;
            }
        }
#endif
    }
}
