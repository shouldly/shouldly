// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

#if CLR_2_0 || CLR_4_0
using System;
using NUnit.Framework;

namespace NUnit.Core.Tests.Generic
{
    [Category("Generics")]
    [TestFixture(typeof(double))]
    public class TypeParameterUsedWithTestMethod<T>
    {
        [TestCase(5)]
        [TestCase(1.23)]
        public void TestMyArgType(T x)
        {
            Assert.That(x, Is.TypeOf(typeof(T)));
        }
    }
}
#endif