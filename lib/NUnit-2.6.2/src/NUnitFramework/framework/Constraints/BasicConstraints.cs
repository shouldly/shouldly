// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// BasicConstraint is the abstract base for constraints that
    /// perform a simple comparison to a constant value.
    /// </summary>
    public abstract class BasicConstraint : Constraint
    {
        private object expected;
        private string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BasicConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <param name="description">The description.</param>
        public BasicConstraint(object expected, string description)
        {
            this.expected = expected;
            this.description = description;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (actual == null && expected == null)
                return true;

            if (actual == null || expected == null)
                return false;
            
            return expected.Equals(actual);
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write(description);
        }
    }

    /// <summary>
    /// NullConstraint tests that the actual value is null
    /// </summary>
    public class NullConstraint : BasicConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NullConstraint"/> class.
        /// </summary>
        public NullConstraint() : base(null, "null") { }
    }

    /// <summary>
    /// TrueConstraint tests that the actual value is true
    /// </summary>
    public class TrueConstraint : BasicConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TrueConstraint"/> class.
        /// </summary>
        public TrueConstraint() : base(true, "True") { }
    }

    /// <summary>
    /// FalseConstraint tests that the actual value is false
    /// </summary>
    public class FalseConstraint : BasicConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FalseConstraint"/> class.
        /// </summary>
        public FalseConstraint() : base(false, "False") { }
    }

    /// <summary>
    /// NaNConstraint tests that the actual value is a double or float NaN
    /// </summary>
    public class NaNConstraint : Constraint
    {
        /// <summary>
        /// Test that the actual value is an NaN
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            return actual is double && double.IsNaN((double)actual)
                || actual is float && float.IsNaN((float)actual);
        }

        /// <summary>
        /// Write the constraint description to a specified writer
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write("NaN");
        }
    }
}
