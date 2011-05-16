﻿// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Syntax
{
    public class NotOperatorOverride : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<not <null>>";
            staticSyntax = !Is.Null;
            inheritedSyntax = !Helper().Null;
            builderSyntax = !Builder().Null;
        }
    }

    public class AndOperatorOverride : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<and <greaterthan 5> <lessthan 10>>";
            staticSyntax = Is.GreaterThan(5) & Is.LessThan(10);
            inheritedSyntax = Helper().GreaterThan(5) & Is.LessThan(10);
            builderSyntax = Builder().GreaterThan(5) & Builder().LessThan(10);
        }
    }

    public class OrOperatorOverride : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<or <lessthan 5> <greaterthan 10>>";
            staticSyntax = Is.LessThan(5) | Is.GreaterThan(10);
            inheritedSyntax = Helper().LessThan(5) | Is.GreaterThan(10);
            builderSyntax = Builder().LessThan(5) | Is.GreaterThan(10);
        }
    }

    public class MixedOperatorOverrides
    {
        [Test]
        public void ComplexTests()
        {
            string expected = "<and <and <not <null>> <not <lessthan 5>>> <not <greaterthan 10>>>";

            Constraint c =
                Is.Not.Null & Is.Not.LessThan(5) & Is.Not.GreaterThan(10);
            Assert.That(c.ToString(), Is.EqualTo(expected).NoClip);

            c = !Is.Null & !Is.LessThan(5) & !Is.GreaterThan(10);
            Assert.That(c.ToString(), Is.EqualTo(expected).NoClip);

            Constraint x = null;
            c = !x & !Is.LessThan(5) & !Is.GreaterThan(10);
            Assert.That(c.ToString(), Is.EqualTo(expected).NoClip);
        }
    }
}
