// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Reflection;

namespace NUnit.Framework
{
    /// <summary>
    /// Randomizer returns a set of random values in a repeatable
    /// way, to allow re-running of tests if necessary.
    /// </summary>
    public class Randomizer : Random
    {
        #region Static Members
        private static Random seedGenerator = new Random();

        private static Hashtable randomizers = new Hashtable();

        /// <summary>
        /// Get a random seed for use in creating a randomizer.
        /// </summary>
        public static int RandomSeed
        {
            get { return seedGenerator.Next(); }
        }

        /// <summary>
        /// Get a randomizer for a particular member, returning
        /// one that has already been created if it exists.
        /// This ensures that the same values are generated
        /// each time the tests are reloaded.
        /// </summary>
        public static Randomizer GetRandomizer(MemberInfo member)
        {
            Randomizer r = (Randomizer)randomizers[member];

            if ( r == null )
                randomizers[member] = r = new Randomizer();

            return r;
        }


        /// <summary>
        /// Get a randomizer for a particular parameter, returning
        /// one that has already been created if it exists.
        /// This ensures that the same values are generated
        /// each time the tests are reloaded.
        /// </summary>
        public static Randomizer GetRandomizer(ParameterInfo parameter)
        {
            return GetRandomizer(parameter.Member);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a randomizer using a random seed
        /// </summary>
        public Randomizer() : base(RandomSeed) { }

        /// <summary>
        /// Construct a randomizer using a specified seed
        /// </summary>
        public Randomizer(int seed) : base(seed) { }
        #endregion

        #region Public Methods
        /// <summary>
        /// Return an array of random doubles between 0.0 and 1.0.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public double[] GetDoubles(int count)
        {
            double[] rvals = new double[count];

            for (int index = 0; index < count; index++)
                rvals[index] = NextDouble();

            return rvals;
        }

        /// <summary>
        /// Return an array of random doubles with values in a specified range.
        /// </summary>
        public double[] GetDoubles(double min, double max, int count)
        {
            double range = max - min;
            double[] rvals = new double[count];

            for (int index = 0; index < count; index++)
                rvals[index] = NextDouble() * range + min;

            return rvals;
        }

        /// <summary>
        /// Return an array of random ints with values in a specified range.
        /// </summary>
        public int[] GetInts(int min, int max, int count)
        {
            int[] ivals = new int[count];

            for (int index = 0; index < count; index++)
                ivals[index] = Next(min, max);

            return ivals;
        }
        #endregion
    }
}
