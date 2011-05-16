// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// The IConstraintExpression interface is implemented by all
    /// complete and resolvable constraints and expressions.
    /// </summary>
    public interface IResolveConstraint
    {
        /// <summary>
        /// Return the top-level constraint for this expression
        /// </summary>
        /// <returns></returns>
        Constraint Resolve();
    }
}
