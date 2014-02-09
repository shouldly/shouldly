// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;

namespace NUnit.Framework.Syntax
{
    [TestFixture]
    public class ExactTypeTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<typeof System.String>";
            staticSyntax = Is.TypeOf(typeof(string));
            inheritedSyntax = Helper().TypeOf(typeof(string));
            builderSyntax = Builder().TypeOf(typeof(string));
        }
    }

    [TestFixture]
    public class InstanceOfTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<instanceof System.String>";
            staticSyntax = Is.InstanceOf(typeof(string));
            inheritedSyntax = Helper().InstanceOf(typeof(string));
            builderSyntax = Builder().InstanceOf(typeof(string));
        }
    }

    [TestFixture, Obsolete]
    public class InstanceOfTypeTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<instanceof System.String>";
            staticSyntax = Is.InstanceOfType(typeof(string));
            inheritedSyntax = Helper().InstanceOfType(typeof(string));
            builderSyntax = Builder().InstanceOfType(typeof(string));
        }
    }

    [TestFixture]
    public class AssignableFromTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<assignablefrom System.String>";
            staticSyntax = Is.AssignableFrom(typeof(string));
            inheritedSyntax = Helper().AssignableFrom(typeof(string));
            builderSyntax = Builder().AssignableFrom(typeof(string));
        }
    }

    [TestFixture]
    public class AssignableToTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<assignableto System.String>";
            staticSyntax = Is.AssignableTo(typeof(string));
            inheritedSyntax = Helper().AssignableTo(typeof(string));
            builderSyntax = Builder().AssignableTo(typeof(string));
        }
    }

    [TestFixture]
    public class AttributeTest : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<attributeexists NUnit.Framework.TestFixtureAttribute>";
            staticSyntax = Has.Attribute(typeof(TestFixtureAttribute));
            inheritedSyntax = Helper().Attribute(typeof(TestFixtureAttribute));
            builderSyntax = Builder().Attribute(typeof(TestFixtureAttribute));
        }
    }

    [TestFixture]
    public class AttributeTestWithFollowingConstraint : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = @"<attribute NUnit.Framework.TestFixtureAttribute <property Description <not <null>>>>";
            staticSyntax = Has.Attribute(typeof(TestFixtureAttribute)).Property("Description").Not.Null;
            inheritedSyntax = Helper().Attribute(typeof(TestFixtureAttribute)).Property("Description").Not.Null;
            builderSyntax = Builder().Attribute(typeof(TestFixtureAttribute)).Property("Description").Not.Null;
        }
    }

#if CLR_2_0 || CLR_4_0
    [TestFixture]
    public class ExactTypeTest_Generic : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<typeof System.String>";
            staticSyntax = Is.TypeOf<string>();
            inheritedSyntax = Helper().TypeOf<string>();
            builderSyntax = Builder().TypeOf<string>();
        }
    }

    [TestFixture]
    public class InstanceOfTest_Generic : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<instanceof System.String>";
            staticSyntax = Is.InstanceOf<string>();
            inheritedSyntax = Helper().InstanceOf<string>();
            builderSyntax = Builder().InstanceOf<string>();
        }
    }

    [TestFixture, Obsolete]
    public class InstanceOfTypeTest_Generic : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<instanceof System.String>";
            staticSyntax = Is.InstanceOfType<string>();
            inheritedSyntax = Helper().InstanceOfType<string>();
            builderSyntax = Builder().InstanceOfType<string>();
        }
    }

    [TestFixture]
    public class AssignableFromTest_Generic : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<assignablefrom System.String>";
            staticSyntax = Is.AssignableFrom<string>();
            inheritedSyntax = Helper().AssignableFrom<string>();
            builderSyntax = Builder().AssignableFrom<string>();
        }
    }

    [TestFixture]
    public class AssignableToTest_Generic : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<assignableto System.String>";
            staticSyntax = Is.AssignableTo<string>();
            inheritedSyntax = Helper().AssignableTo<string>();
            builderSyntax = Builder().AssignableTo<string>();
        }
    }

    [TestFixture]
    public class AttributeTest_Generic : SyntaxTest
    {
        [SetUp]
        public void SetUp()
        {
            parseTree = "<attributeexists NUnit.Framework.TestFixtureAttribute>";
            staticSyntax = Has.Attribute<TestFixtureAttribute>();
            inheritedSyntax = Helper().Attribute<TestFixtureAttribute>();
            builderSyntax = Builder().Attribute<TestFixtureAttribute>();
        }
    }
#endif
}
