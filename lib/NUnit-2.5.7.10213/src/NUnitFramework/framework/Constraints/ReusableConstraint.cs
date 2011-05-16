using System;

namespace NUnit.Framework.Constraints
{
    public class ReusableConstraint : IResolveConstraint
    {
        private Constraint constraint;

        public ReusableConstraint(IResolveConstraint c)
        {
            this.constraint = c.Resolve();
        }

        public static implicit operator ReusableConstraint(Constraint c)
        {
            return new ReusableConstraint(c);
        }

        public override string ToString()
        {
            return constraint.ToString();
        }

        #region IResolveConstraint Members

        public Constraint Resolve()
        {
            return constraint;
        }

        #endregion
    }
}
