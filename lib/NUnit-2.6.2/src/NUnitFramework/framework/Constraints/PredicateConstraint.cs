// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// Predicate constraint wraps a Predicate in a constraint,
    /// returning success if the predicate is true.
    /// </summary>
    public class PredicateConstraint<T> : Constraint
    {
        Predicate<T> predicate;

        /// <summary>
        /// Construct a PredicateConstraint from a predicate
        /// </summary>
        public PredicateConstraint(Predicate<T> predicate)
        {
            this.predicate = predicate;
        }

        /// <summary>
        /// Determines whether the predicate succeeds when applied
        /// to the actual value.
        /// </summary>
        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (!(actual is T))
                throw new ArgumentException("The actual value is not of type " + typeof(T).Name, "actual");

            return predicate((T)actual);
        }

        /// <summary>
        /// Writes the description to a MessageWriter
        /// </summary>
        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WritePredicate("value matching");
            writer.Write(predicate.Method.Name.StartsWith("<")
                ? "lambda expression"
                : predicate.Method.Name);
        }
    }
}
#endif
