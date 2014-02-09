// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// RangeConstraint tests whethe two values are within a 
    /// specified range.
    /// </summary>
#if CLR_2_0 || CLR_4_0
    public class RangeConstraint<T> : ComparisonConstraint where T : IComparable<T>
    {
        private T from;
        private T to;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RangeConstraint"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public RangeConstraint(T from, T to)
            : base(from, to)
        {
            this.from = from;
            this.to = to;
            this.comparer = ComparisonAdapter.For(new NUnitComparer<T>());
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (from == null || to == null || actual == null)
                throw new ArgumentException("Cannot compare using a null reference", "expected");

            return comparer.Compare(from, actual) <= 0 &&
                   comparer.Compare(to, actual) >= 0;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {

            writer.Write("in range ({0},{1})", from, to);
        }
    }
#else
    public class RangeConstraint : ComparisonConstraint
    {
        private IComparable from;
        private IComparable to;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RangeConstraint"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public RangeConstraint(IComparable from, IComparable to) : base( from, to )
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if ( from == null || to == null || actual == null)
                throw new ArgumentException( "Cannot compare using a null reference", "expected" );

            return comparer.Compare(from, actual) <= 0 &&
                   comparer.Compare(to, actual) >= 0;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {

            writer.Write("in range ({0},{1})", from, to);
        }
    }
#endif
}
