// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class ComparerTests
    {
        [TestCase(4, 4)]
        [TestCase(4.0d, 4.0d)]
        [TestCase(4.0f, 4.0f)]
        [TestCase(4, 4.0d)]
        [TestCase(4, 4.0f)]
        [TestCase(4.0d, 4)]
        [TestCase(4.0d, 4.0f)]
        [TestCase(4.0f, 4)]
        [TestCase(4.0f, 4.0d)]
        [TestCase(SpecialValue.Null, SpecialValue.Null)]
#if NET_2_0
        [TestCase(null, null)]
#endif
        public void EqualItems(object x, object y)
        {
            Assert.That(NUnitComparer.Default.Compare(x, y) == 0);
            Assert.That(NUnitEqualityComparer.Default.ObjectsEqual(x, y));
        }

        [TestCase(4, 2)]
        [TestCase(4.0d, 2.0d)]
        [TestCase(4.0f, 2.0f)]
        [TestCase(4, 2.0d)]
        [TestCase(4, 2.0f)]
        [TestCase(4.0d, 2)]
        [TestCase(4.0d, 2.0f)]
        [TestCase(4.0f, 2)]
        [TestCase(4.0f, 2.0d)]
        [TestCase(4, SpecialValue.Null)]
#if NET_2_0
        [TestCase(4, null)]
#endif
        public void UnequalItems(object greater, object lesser)
        {
            Assert.That(NUnitComparer.Default.Compare(greater, lesser) > 0);
            Assert.That(NUnitComparer.Default.Compare(lesser, greater) < 0);
            Assert.False(NUnitEqualityComparer.Default.ObjectsEqual( greater, lesser ));
            Assert.False(NUnitEqualityComparer.Default.ObjectsEqual( lesser, greater ));
        }

        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.NaN)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.NegativeInfinity)]
        [TestCase(float.NaN)]
        public void SpecialFloatingPointValues(object x)
        {
            Assert.That(NUnitEqualityComparer.Default.ObjectsEqual(x, x));
        }
    }
}
