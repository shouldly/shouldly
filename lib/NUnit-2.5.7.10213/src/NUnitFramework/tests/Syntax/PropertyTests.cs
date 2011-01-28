// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;

namespace NUnit.Framework.Syntax
{
    public class PropertyExistsTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<propertyexists X>";
            staticSyntax = Has.Property("X");
            inheritedSyntax = Helper().Property("X");
            builderSyntax = Builder().Property("X");
        }
    }

    public class PropertyExistsTest_AndFollows : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<and <propertyexists X> <equal 7>>";
            staticSyntax = Has.Property("X").And.EqualTo(7);
            inheritedSyntax = Helper().Property("X").And.EqualTo(7);
            builderSyntax = Builder().Property("X").And.EqualTo(7);
        }
    }

    public class PropertyTest_ConstraintFollows : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<property X <greaterthan 5>>";
            staticSyntax = Has.Property("X").GreaterThan(5);
            inheritedSyntax = Helper().Property("X").GreaterThan(5);
            builderSyntax = Builder().Property("X").GreaterThan(5);
        }
    }

    public class PropertyTest_NotFollows : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<property X <not <greaterthan 5>>>";
            staticSyntax = Has.Property("X").Not.GreaterThan(5);
            inheritedSyntax = Helper().Property("X").Not.GreaterThan(5);
            builderSyntax = Builder().Property("X").Not.GreaterThan(5);
        }
    }

    public class LengthTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<property Length <greaterthan 5>>";
            staticSyntax = Has.Length.GreaterThan(5);
            inheritedSyntax = Helper().Length.GreaterThan(5);
            builderSyntax = Builder().Length.GreaterThan(5);
        }
    }

    public class CountTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<property Count <equal 5>>";
            staticSyntax = Has.Count.EqualTo(5);
            inheritedSyntax = Helper().Count.EqualTo(5);
            builderSyntax = Builder().Count.EqualTo(5);
        }
    }

    public class MessageTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = @"<property Message <startswith ""Expected"">>";
            staticSyntax = Has.Message.StartsWith("Expected");
            inheritedSyntax = Helper().Message.StartsWith("Expected");
            builderSyntax = Builder().Message.StartsWith("Expected");
        }
    }

    public class PropertySyntaxVariations
    {
        private readonly int[] ints = new int[] { 1, 2, 3 };

        [Test]
        public void ExistenceTest()
        {
            Assert.That(ints, Has.Property("Length"));
            Assert.That(ints, Has.Length);
        }

        [Test]
        public void SeparateConstraintTest()
        {
            Assert.That(ints, Has.Property("Length").EqualTo(3));
            Assert.That(ints, Has.Length.EqualTo(3));
        }
    }
}