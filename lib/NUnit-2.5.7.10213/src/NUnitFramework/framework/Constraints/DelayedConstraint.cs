﻿// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Threading;

namespace NUnit.Framework.Constraints
{
    ///<summary>
    /// Applies a delay to the match so that a match can be evaluated in the future.
    ///</summary>
    public class DelayedConstraint : PrefixConstraint
    {
        private readonly int delayInMilliseconds;
        private readonly int pollingInterval;

        ///<summary>
        /// Creates a new DelayedConstraint
        ///</summary>
        ///<param name="baseConstraint">The inner constraint two decorate</param>
        ///<param name="delayInMilliseconds">The time interval after which the match is performed</param>
        ///<exception cref="InvalidOperationException">If the value of <paramref name="delayInMilliseconds"/> is less than 0</exception>
        public DelayedConstraint(Constraint baseConstraint, int delayInMilliseconds)
            : this(baseConstraint, delayInMilliseconds, 0) { }

        ///<summary>
        /// Creates a new DelayedConstraint
        ///</summary>
        ///<param name="baseConstraint">The inner constraint two decorate</param>
        ///<param name="delayInMilliseconds">The time interval after which the match is performed</param>
        ///<param name="pollingInterval">The time interval used for polling</param>
        ///<exception cref="InvalidOperationException">If the value of <paramref name="delayInMilliseconds"/> is less than 0</exception>
        public DelayedConstraint(Constraint baseConstraint, int delayInMilliseconds, int pollingInterval)
            : base(baseConstraint)
        {
            if (delayInMilliseconds < 0)
                throw new ArgumentException("Cannot check a condition in the past", "delayInMilliseconds");

            this.delayInMilliseconds = delayInMilliseconds;
            this.pollingInterval = pollingInterval;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for if the base constraint fails, false if it succeeds</returns>
        public override bool Matches(object actual)
        {
            Thread.Sleep(delayInMilliseconds);
            this.actual = actual;
            return baseConstraint.Matches(actual);
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a delegate
        /// </summary>
        /// <param name="del">The delegate whose value is to be tested</param>
        /// <returns>True for if the base constraint fails, false if it succeeds</returns>
        public override bool Matches(ActualValueDelegate del)
        {
			int remainingDelay = delayInMilliseconds;

			while (pollingInterval > 0 && pollingInterval < remainingDelay)
			{
				remainingDelay -= pollingInterval;
				Thread.Sleep(pollingInterval);
				this.actual = del();
				if (baseConstraint.Matches(actual))
					return true;
			}

			if ( remainingDelay > 0 )
				Thread.Sleep(remainingDelay);
			this.actual = del();
			return baseConstraint.Matches(actual);
        }

#if NET_2_0
        /// <summary>
        /// Test whether the constraint is satisfied by a given reference.
        /// Overridden to wait for the specified delay period before
        /// calling the base constraint with the dereferenced value.
        /// </summary>
        /// <param name="actual">A reference to the value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches<T>(ref T actual)
        {
            int remainingDelay = delayInMilliseconds;

            while (pollingInterval > 0 && pollingInterval < remainingDelay)
            {
                remainingDelay -= pollingInterval;
                Thread.Sleep(pollingInterval);
                this.actual = actual;
                if (baseConstraint.Matches(actual))
                    return true;
            }

            if ( remainingDelay > 0 )
                Thread.Sleep(remainingDelay);
            this.actual = actual;
            return baseConstraint.Matches(actual);
        }
#else
		/// <summary>
		/// Test whether the constraint is satisfied by a given boolean reference.
		/// Overridden to wait for the specified delay period before
		/// calling the base constraint with the dereferenced value.
		/// </summary>
		/// <param name="actual">A reference to the value to be tested</param>
		/// <returns>True for success, false for failure</returns>
		public override bool Matches(ref bool actual)
		{
			int remainingDelay = delayInMilliseconds;

			while (pollingInterval > 0 && pollingInterval < remainingDelay)
			{
				remainingDelay -= pollingInterval;
				Thread.Sleep(pollingInterval);
				this.actual = actual;
				if (baseConstraint.Matches(actual))
					return true;
			}

			if ( remainingDelay > 0 )
				Thread.Sleep(remainingDelay);
			this.actual = actual;
			return baseConstraint.Matches(actual);
		}
#endif

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            baseConstraint.WriteDescriptionTo(writer);
            writer.Write(string.Format(" after {0} millisecond delay", delayInMilliseconds));
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a MessageWriter.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            baseConstraint.WriteActualValueTo(writer);
        }

        /// <summary>
        /// Returns the string representation of the constraint.
        /// </summary>
        protected override string GetStringRepresentation()
        {
            return string.Format("<after {0} {1}>", delayInMilliseconds, baseConstraint);
        }
    }
}
