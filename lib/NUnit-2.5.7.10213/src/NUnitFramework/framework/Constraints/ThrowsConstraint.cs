// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Constraints
{
    #region ThrowsConstraint
    /// <summary>
    /// ThrowsConstraint is used to test the exception thrown by 
    /// a delegate by applying a constraint to it.
    /// </summary>
    public class ThrowsConstraint : PrefixConstraint
    {
        private Exception caughtException;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ThrowsConstraint"/> class,
        /// using a constraint to be applied to the exception.
        /// </summary>
        /// <param name="baseConstraint">A constraint to apply to the caught exception.</param>
        public ThrowsConstraint(Constraint baseConstraint)
            : base(baseConstraint) { }

        /// <summary>
        /// Get the actual exception thrown - used by Assert.Throws.
        /// </summary>
        public Exception ActualException
        {
            get { return caughtException; }
        }

        #region Constraint Overrides
        /// <summary>
        /// Executes the code of the delegate and captures any exception.
        /// If a non-null base constraint was provided, it applies that
        /// constraint to the exception.
        /// </summary>
        /// <param name="actual">A delegate representing the code to be tested</param>
        /// <returns>True if an exception is thrown and the constraint succeeds, otherwise false</returns>
        public override bool Matches(object actual)
        {
            TestDelegate code = actual as TestDelegate;
            if (code == null)
                throw new ArgumentException(
                    string.Format("The actual value must be a TestDelegate but was {0}",actual.GetType().Name), "actual");

            this.caughtException = null;

            try
            {
                code();
            }
            catch (Exception ex)
            {
                this.caughtException = ex;
            }

            if (this.caughtException == null)
                return false;

            return baseConstraint == null || baseConstraint.Matches(caughtException);
        }

#if NET_2_0
        /// <summary>
        /// Converts an ActualValueDelegate to a TestDelegate
        /// before calling the primary overload.
        /// </summary>
        /// <param name="del"></param>
        /// <returns></returns>
        public override bool Matches(ActualValueDelegate del)
        {
            TestDelegate testDelegate = new TestDelegate(delegate { del(); });
            return Matches((object)testDelegate);
        }
#endif

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            if (baseConstraint == null)
                writer.WritePredicate("an exception");
            else
                baseConstraint.WriteDescriptionTo(writer);
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. The default implementation simply writes
        /// the raw value of actual, leaving it to the writer to
        /// perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo(MessageWriter writer)
        {
            if (caughtException == null)
                writer.Write("no exception thrown");
            else if (baseConstraint != null)
                baseConstraint.WriteActualValueTo(writer);
            else
                writer.WriteActualValue(caughtException);
        }
        #endregion

        /// <summary>
        /// Returns the string representation of this constraint
        /// </summary>
        protected override string GetStringRepresentation()
        {
            if (baseConstraint == null)
                return "<throws>";
            
            return base.GetStringRepresentation();
        }
    }
    #endregion

    #region ThrowsNothingConstraint
    /// <summary>
    /// ThrowsNothingConstraint tests that a delegate does not
    /// throw an exception.
    /// </summary>
	public class ThrowsNothingConstraint : Constraint
	{
		private Exception caughtException;

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True if no exception is thrown, otherwise false</returns>
		public override bool Matches(object actual)
		{
			TestDelegate code = actual as TestDelegate;
			if (code == null)
				throw new ArgumentException("The actual value must be a TestDelegate", "actual");

            this.caughtException = null;

            try
            {
                code();
            }
            catch (Exception ex)
            {
                this.caughtException = ex;
            }

			return this.caughtException == null;
		}

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
		public override void WriteDescriptionTo(MessageWriter writer)
		{
			writer.Write(string.Format("No Exception to be thrown"));
		}

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. The default implementation simply writes
        /// the raw value of actual, leaving it to the writer to
        /// perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
		public override void WriteActualValueTo(MessageWriter writer)
		{
			writer.WriteActualValue( this.caughtException.GetType() );
		}
    }
    #endregion
}
