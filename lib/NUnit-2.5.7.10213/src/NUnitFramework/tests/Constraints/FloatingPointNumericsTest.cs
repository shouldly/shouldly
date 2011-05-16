// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.IO;
using System.Drawing;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class FloatingPointNumericsTest
    {

        /// <summary>Tests the floating point value comparison helper</summary>
        [Test]
        public void FloatEqualityWithUlps()
        {
            Assert.IsTrue(
                FloatingPointNumerics.AreAlmostEqualUlps(0.00000001f, 0.0000000100000008f, 1)
            );
            Assert.IsFalse(
                FloatingPointNumerics.AreAlmostEqualUlps(0.00000001f, 0.0000000100000017f, 1)
            );

            Assert.IsTrue(
                FloatingPointNumerics.AreAlmostEqualUlps(1000000.00f, 1000000.06f, 1)
            );
            Assert.IsFalse(
                FloatingPointNumerics.AreAlmostEqualUlps(1000000.00f, 1000000.13f, 1)
            );
        }

        /// <summary>Tests the double precision floating point value comparison helper</summary>
        [Test]
        public void DoubleEqualityWithUlps()
        {
            Assert.IsTrue(
                FloatingPointNumerics.AreAlmostEqualUlps(0.00000001, 0.000000010000000000000002, 1)
            );
            Assert.IsFalse(
                FloatingPointNumerics.AreAlmostEqualUlps(0.00000001, 0.000000010000000000000004, 1)
            );

            Assert.IsTrue(
                FloatingPointNumerics.AreAlmostEqualUlps(1000000.00, 1000000.0000000001, 1)
            );
            Assert.IsFalse(
                FloatingPointNumerics.AreAlmostEqualUlps(1000000.00, 1000000.0000000002, 1)
            );
        }

        /// <summary>Tests the integer reinterpretation functions</summary>
        [Test]
        public void MirroredIntegerReinterpretation()
        {
            Assert.AreEqual(
                12345.0f,
                FloatingPointNumerics.ReinterpretAsFloat(
                    FloatingPointNumerics.ReinterpretAsInt(12345.0f)
                )
            );
        }

        /// <summary>Tests the long reinterpretation functions</summary>
        [Test]
        public void MirroredLongReinterpretation()
        {
            Assert.AreEqual(
                12345.67890,
                FloatingPointNumerics.ReinterpretAsDouble(
                    FloatingPointNumerics.ReinterpretAsLong(12345.67890)
                )
            );
        }

        /// <summary>Tests the floating point reinterpretation functions</summary>
        [Test]
        public void MirroredFloatReinterpretation()
        {
            Assert.AreEqual(
                12345,
                FloatingPointNumerics.ReinterpretAsInt(
                    FloatingPointNumerics.ReinterpretAsFloat(12345)
                )
            );
        }


        /// <summary>
        ///   Tests the double prevision floating point reinterpretation functions
        /// </summary>
        [Test]
        public void MirroredDoubleReinterpretation()
        {
            Assert.AreEqual(
                1234567890,
                FloatingPointNumerics.ReinterpretAsLong(
                    FloatingPointNumerics.ReinterpretAsDouble(1234567890)
                )
            );
        }

  }
}
