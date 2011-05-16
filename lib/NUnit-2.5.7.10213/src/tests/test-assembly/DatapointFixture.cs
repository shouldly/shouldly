﻿using System;
using NUnit.Framework;
#if NET_2_0
using System.Collections.Generic;
#endif

namespace NUnit.TestData.DatapointFixture
{
    public abstract class SquareRootTest
    {
        [Theory]
        public void SqrtTimesItselfGivesOriginal(double num)
        {
            Assume.That(num >= 0.0 && num < double.MaxValue);

            double sqrt = Math.Sqrt(num);

            Assert.That(sqrt >= 0.0);
            Assert.That(sqrt * sqrt, Is.EqualTo(num).Within(0.000001));
        }
    }

    public class SquareRootTest_Field_Double : SquareRootTest
    {
        [Datapoint]
        public double zero = 0;

        [Datapoint]
        public double positive = 1;

        [Datapoint]
        public double negative = -1;

        [Datapoint]
        public double max = double.MaxValue;

        [Datapoint]
        public double infinity = double.PositiveInfinity;
    }

    public class SquareRootTest_Field_ArrayOfDouble : SquareRootTest
    {
        [Datapoints]
        public double[] values = new double[] { 0.0, 1.0, -1.0, double.MaxValue, double.PositiveInfinity };
    }

#if NET_2_0 && CS_3_0
    public class SquareRootTest_Field_IEnumerableOfDouble : SquareRootTest
    {
        [Datapoints]
        public IEnumerable<double> values = new List<double> { 0.0, 1.0, -1.0, double.MaxValue, double.PositiveInfinity };
    }

    public class SquareRootTest_Property_IEnumerableOfDouble : SquareRootTest
    {
        [Datapoints]
        public IEnumerable<double> Values
        {
            get { return new List<double> { 0.0, 1.0, -1.0, double.MaxValue, double.PositiveInfinity }; }
        }
    }

    public class SquareRootTest_Method_IEnumerableOfDouble : SquareRootTest
    {
        [Datapoints]
        public IEnumerable<double> GetValues()
        {
            return new List<double> { 0.0, 1.0, -1.0, double.MaxValue, double.PositiveInfinity };
        }
    }
#endif

    public class SquareRootTest_Property_ArrayOfDouble : SquareRootTest
    {
        [Datapoints]
        public double[] Values
        {
            get { return new double[] { 0.0, 1.0, -1.0, double.MaxValue, double.PositiveInfinity }; }
        }
    }

    public class SquareRootTest_Method_ArrayOfDouble : SquareRootTest
    {
        [Datapoints]
        public double[] GetValues()
        {
            return new double[] { 0.0, 1.0, -1.0, double.MaxValue, double.PositiveInfinity };
        }
    }
}
