// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;

namespace NUnit.Framework
{
    /// <summary>
    /// RangeAttribute is used to supply a range of values to an
    /// individual parameter of a parameterized test.
    /// </summary>
    public class RangeAttribute : ValuesAttribute
    {
        /// <summary>
        /// Construct a range of ints using default step of 1
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public RangeAttribute(int from, int to) : this(from, to, 1) { }

        /// <summary>
        /// Construct a range of ints specifying the step size 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        public RangeAttribute(int from, int to, int step)
        {
            int count = (to - from) / step + 1;
            this.data = new object[count];
            int index = 0;
            for (int val = from; index < count; val += step)
                this.data[index++] = val;
        }

        /// <summary>
        /// Construct a range of longs
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        public RangeAttribute(long from, long to, long step)
        {
            long count = (to - from) / step + 1;
            this.data = new object[count];
            int index = 0;
            for (long val = from; index < count; val += step)
                data[index++] = val;
        }

        /// <summary>
        /// Construct a range of doubles
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        public RangeAttribute(double from, double to, double step)
        {
            double tol = step / 1000;
            int count = (int)((to - from) / step + tol + 1);
            this.data = new object[count];
            int index = 0;
            for (double val = from; index < count; val += step)
                data[index++] = val;
        }

        /// <summary>
        /// Construct a range of floats
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        public RangeAttribute(float from, float to, float step)
        {
            float tol = step / 1000;
            int count = (int)((to - from) / step + tol + 1);
            this.data = new object[count];
            int index = 0;
            for (float val = from; index < count; val += step)
                data[index++] = val;
        }
    }
}
