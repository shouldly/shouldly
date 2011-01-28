// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// ResolvableConstraintExpression is used to represent a compound
    /// constraint being constructed at a point where the last operator
    /// may either terminate the expression or may have additional 
    /// qualifying constraints added to it. 
    /// 
    /// It is used, for example, for a Property element or for
    /// an Exception element, either of which may be optionally
    /// followed by constraints that apply to the property or 
    /// exception.
    /// </summary>
    public class ResolvableConstraintExpression : ConstraintExpression, IResolveConstraint
    {
        /// <summary>
        /// Create a new instance of ResolvableConstraintExpression
        /// </summary>
        public ResolvableConstraintExpression() { }

        /// <summary>
        /// Create a new instance of ResolvableConstraintExpression,
        /// passing in a pre-populated ConstraintBuilder.
        /// </summary>
        public ResolvableConstraintExpression(ConstraintBuilder builder)
            : base(builder) { }

        /// <summary>
        /// Appends an And Operator to the expression
        /// </summary>
        public ConstraintExpression And
        {
            get { return this.Append(new AndOperator()); }
        }

        /// <summary>
        /// Appends an Or operator to the expression.
        /// </summary>
        public ConstraintExpression Or
        {
            get { return this.Append(new OrOperator()); }
        }

        #region IResolveConstraint Members
        /// <summary>
        /// Resolve the current expression to a Constraint
        /// </summary>
        Constraint IResolveConstraint.Resolve()
        {
            return builder.Resolve();
        }
        #endregion
    }
}
