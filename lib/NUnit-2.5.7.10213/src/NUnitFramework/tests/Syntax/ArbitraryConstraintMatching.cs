using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Syntax
{
    [TestFixture]
    public class ArbitraryConstraintMatching
    {
        Constraint custom = new CustomConstraint();
        Constraint another = new AnotherConstraint();

        [Test]
        public void CanMatchCustomConstraint()
        {
            IResolveConstraint constraint = new ConstraintExpression().Matches(custom);
            Assert.That(constraint.Resolve().ToString(), Is.EqualTo("<custom>"));
        }

        [Test]
        public void CanMatchCustomConstraintAfterPrefix()
        {
            IResolveConstraint constraint = Is.All.Matches(custom);
            Assert.That(constraint.Resolve().ToString(), Is.EqualTo("<all <custom>>"));
        }

        [Test]
        public void CanMatchCustomConstraintsUnderAndOperator()
        {
            IResolveConstraint constraint = Is.All.Matches(custom).And.Matches(another);
            Assert.That(constraint.Resolve().ToString(), Is.EqualTo("<all <and <custom> <another>>>")); 
        }

#if NET_2_0
        [Test]
        public void CanMatchPredicate()
        {
            IResolveConstraint constraint = new ConstraintExpression().Matches(new Predicate<int>(IsEven));
            Assert.That(constraint.Resolve().ToString(), Is.EqualTo("<predicate>"));
            Assert.That(42, constraint);
        }

        bool IsEven(int num)
        {
            return (num & 1) == 0;
        }

#if CS_3_0
        [Test]
        public void CanMatchLambda()
        {
            IResolveConstraint constraint = new ConstraintExpression().Matches<int>( (x) => (x & 1) == 0);
            Assert.That(constraint.Resolve().ToString(), Is.EqualTo("<predicate>"));
            Assert.That(42, constraint);
        }
#endif
#endif

        class CustomConstraint : Constraint
        {
            public override bool Matches(object actual)
            {
                throw new NotImplementedException();
            }

            public override void WriteDescriptionTo(MessageWriter writer)
            {
                throw new NotImplementedException();
            }
        }

        class AnotherConstraint : CustomConstraint
        {
        }
    }
}
