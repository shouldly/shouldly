﻿// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;

namespace NUnit.Framework.Tests
{
    public class TestFixtureAttributeTests
    {
        static object[] fixtureArgs = new object[] { 10, 20, "Charlie" };
#if NET_2_0
        static Type[] typeArgs = new Type[] { typeof(int), typeof(string) };
        static object[] combinedArgs = new object[] { typeof(int), typeof(string), 10, 20, "Charlie" };
#endif

        [Test]
        public void ConstructWithoutArguments()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute();
            Assert.That(attr.Arguments.Length == 0);
#if NET_2_0
            Assert.That(attr.TypeArgs.Length == 0);
#endif
        }

        [Test]
        public void ConstructWithFixtureArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(fixtureArgs);
            Assert.That(attr.Arguments, Is.EqualTo( fixtureArgs ) );
#if NET_2_0
            Assert.That(attr.TypeArgs.Length == 0 );
#endif
        }

#if NET_2_0
        [Test, Category("Generics")]
        public void ConstructWithJustTypeArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(typeArgs);
            Assert.That(attr.Arguments.Length == 0);
            Assert.That(attr.TypeArgs, Is.EqualTo(typeArgs));
        }

        [Test, Category("Generics")]
        public void ConstructWithNoArgumentsAndSetTypeArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute();
            attr.TypeArgs = typeArgs;
            Assert.That(attr.Arguments.Length == 0);
            Assert.That(attr.TypeArgs, Is.EqualTo(typeArgs));
        }

        [Test, Category("Generics")]
        public void ConstructWithFixtureArgsAndSetTypeArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(fixtureArgs);
            attr.TypeArgs = typeArgs;
            Assert.That(attr.Arguments, Is.EqualTo(fixtureArgs));
            Assert.That(attr.TypeArgs, Is.EqualTo(typeArgs));
        }

        [Test, Category("Generics")]
        public void ConstructWithCombinedArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(combinedArgs);
            Assert.That(attr.Arguments, Is.EqualTo(fixtureArgs));
            Assert.That(attr.TypeArgs, Is.EqualTo(typeArgs));
        }
#endif
	}
}
