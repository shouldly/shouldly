// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework
{
    /// <summary>
    /// Marks a test to use a combinatorial join of any argument data 
    /// provided. NUnit will create a test case for every combination of 
    /// the arguments provided. This can result in a large number of test
    /// cases and so should be used judiciously. This is the default join
    /// type, so the attribute need not be used except as documentation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited=false)]
    public class CombinatorialAttribute : PropertyAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CombinatorialAttribute() : base("_JOINTYPE", "Combinatorial") { }
    }

    /// <summary>
    /// Marks a test to use pairwise join of any argument data provided. 
    /// NUnit will attempt too excercise every pair of argument values at 
    /// least once, using as small a number of test cases as it can. With
    /// only two arguments, this is the same as a combinatorial join.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited=false)]
    public class PairwiseAttribute : PropertyAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PairwiseAttribute() : base("_JOINTYPE", "Pairwise") { }
    }

    /// <summary>
    /// Marks a test to use a sequential join of any argument data
    /// provided. NUnit will use arguements for each parameter in
    /// sequence, generating test cases up to the largest number
    /// of argument values provided and using null for any arguments
    /// for which it runs out of values. Normally, this should be
    /// used with the same number of arguments for each parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited=false)]
    public class SequentialAttribute : PropertyAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SequentialAttribute() : base("_JOINTYPE", "Sequential") { }
    }
}
