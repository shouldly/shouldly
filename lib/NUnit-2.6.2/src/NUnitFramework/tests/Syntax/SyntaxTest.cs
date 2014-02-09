// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Syntax
{
    public abstract class SyntaxTest
    {
        protected string parseTree;
        protected IResolveConstraint staticSyntax;
        protected IResolveConstraint inheritedSyntax;
        protected IResolveConstraint builderSyntax;

        protected AssertionHelper Helper()
        {
            return new AssertionHelper();
        }

        protected ConstraintExpression Builder()
        {
            return new ConstraintExpression();
        }

        [Test]
        public void SupportedByStaticSyntax()
        {
            Assert.That(
                staticSyntax.Resolve().ToString(),
                Is.EqualTo(parseTree).NoClip);
        }

        [Test]
        public void SupportedByConstraintBuilder()
        {
            Assert.That(
                builderSyntax.Resolve().ToString(),
                Is.EqualTo(parseTree).NoClip);
        }

        [Test]
        public void SupportedByInheritedSyntax()
        {
            Assert.That(
                inheritedSyntax.Resolve().ToString(),
                Is.EqualTo(parseTree).NoClip);
        }
    }
}
