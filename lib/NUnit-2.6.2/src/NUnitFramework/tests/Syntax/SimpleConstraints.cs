// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Syntax
{
    public class NullTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<null>";
            staticSyntax = Is.Null;
            inheritedSyntax = Helper().Null;
            builderSyntax = Builder().Null;
        }
    }

    public class TrueTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<true>";
            staticSyntax = Is.True;
            inheritedSyntax = Helper().True;
            builderSyntax = Builder().True;
        }
    }

    public class FalseTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<false>";
            staticSyntax = Is.False;
            inheritedSyntax = Helper().False;
            builderSyntax = Builder().False;
        }
    }

    public class NaNTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<nan>";
            staticSyntax = Is.NaN;
            inheritedSyntax = Helper().NaN;
            builderSyntax = Builder().NaN;
        }
    }

    public class PositiveTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<greaterthan 0>";
            staticSyntax = Is.Positive;
            inheritedSyntax = Helper().Positive;
            builderSyntax = Builder().Positive;
        }
    }

    public class NegativeTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<lessthan 0>";
            staticSyntax = Is.Negative;
            inheritedSyntax = Helper().Negative;
            builderSyntax = Builder().Negative;
        }
    }

    public class EmptyTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<empty>";
            staticSyntax = Is.Empty;
            inheritedSyntax = Helper().Empty;
            builderSyntax = Builder().Empty;
        }
    }
}
