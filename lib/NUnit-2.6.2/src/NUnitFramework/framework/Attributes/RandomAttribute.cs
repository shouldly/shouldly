// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Reflection;

namespace NUnit.Framework
{
    /// <summary>
    /// RandomAttribute is used to supply a set of random values
    /// to a single parameter of a parameterized test.
    /// </summary>
    public class RandomAttribute : ValuesAttribute
    {
        enum SampleType
        {
            Raw,
            IntRange,
            DoubleRange
        }

        SampleType sampleType;
        private int count;
        private int min, max;
        private double dmin, dmax;

        /// <summary>
        /// Construct a set of doubles from 0.0 to 1.0,
        /// specifying only the count.
        /// </summary>
        /// <param name="count"></param>
        public RandomAttribute(int count)
        {
            this.count = count;
            this.sampleType = SampleType.Raw;
        }

        /// <summary>
        /// Construct a set of doubles from min to max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        public RandomAttribute(double min, double max, int count)
        {
            this.count = count;
            this.dmin = min;
            this.dmax = max;
            this.sampleType = SampleType.DoubleRange;
        }

        /// <summary>
        /// Construct a set of ints from min to max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        public RandomAttribute(int min, int max, int count)
        {
            this.count = count;
            this.min = min;
            this.max = max;
            this.sampleType = SampleType.IntRange;
        }

        /// <summary>
        /// Get the collection of values to be used as arguments
        /// </summary>
        public override IEnumerable GetData(ParameterInfo parameter)
        {
            Randomizer r = Randomizer.GetRandomizer(parameter);
            IList values;

            switch (sampleType)
            {
                default:
                case SampleType.Raw:
                    values = r.GetDoubles(count);
                    break;
                case SampleType.IntRange:
                    values = r.GetInts(min, max, count);
                    break;
                case SampleType.DoubleRange:
                    values = r.GetDoubles(dmin, dmax, count);
                    break;
            }

            // Copy the random values into the data array
            // and call the base class which may need to
            // convert them to another type.
            this.data = new object[values.Count];
            for (int i = 0; i < values.Count; i++)
                this.data[i] = values[i];

            return base.GetData(parameter);
        }
    }
}
