// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif
using NUnit.Framework;
using NUnit.TestData.TypeHelperFixture;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class TypeHelperTests
    {
        [TestCase(typeof(int), "Int32")]
        [TestCase(typeof(SimpleClass), "SimpleClass")]
        [TestCase(typeof(MyNoNamespaceClass), "MyNoNamespaceClass")]
#if CLR_2_0 || CLR_4_0
        [TestCase(typeof(GenericClass<int, decimal, string>), "GenericClass<Int32,Decimal,String>")]
        [TestCase(typeof(GenericClass<int[], decimal[], string[]>), "GenericClass<Int32[],Decimal[],String[]>")]
        [TestCase(typeof(ContainerClass.NestedClass), "ContainerClass+NestedClass")]
        [TestCase(typeof(ContainerClass.NestedClass.DoublyNestedClass), "ContainerClass+NestedClass+DoublyNestedClass")]
        [TestCase(typeof(ContainerClass.NestedClass.DoublyNestedGeneric<int>), "ContainerClass+NestedClass+DoublyNestedGeneric<Int32>")]
        [TestCase(typeof(ContainerClass.NestedGeneric<int>), "ContainerClass+NestedGeneric<Int32>")]
        [TestCase(typeof(ContainerClass.NestedGeneric<int>.DoublyNestedClass), "ContainerClass+NestedGeneric+DoublyNestedClass<Int32>")]
        [TestCase(typeof(ContainerClass.NestedGeneric<string>.DoublyNestedGeneric<int>), "ContainerClass+NestedGeneric+DoublyNestedGeneric<String,Int32>")]
        [TestCase(typeof(GenericContainerClass<string>.NestedClass), "GenericContainerClass+NestedClass<String>")]
        [TestCase(typeof(GenericContainerClass<string>.NestedClass.DoublyNestedClass), "GenericContainerClass+NestedClass+DoublyNestedClass<String>")]
        [TestCase(typeof(GenericContainerClass<string>.NestedClass.DoublyNestedGeneric<bool>), "GenericContainerClass+NestedClass+DoublyNestedGeneric<String,Boolean>")]
        [TestCase(typeof(GenericContainerClass<string>.NestedGeneric<int>), "GenericContainerClass+NestedGeneric<String,Int32>")]
        [TestCase(typeof(GenericContainerClass<string>.NestedGeneric<int>.DoublyNestedClass), "GenericContainerClass+NestedGeneric+DoublyNestedClass<String,Int32>")]
        [TestCase(typeof(GenericContainerClass<string>.NestedGeneric<int>.DoublyNestedGeneric<bool>), "GenericContainerClass+NestedGeneric+DoublyNestedGeneric<String,Int32,Boolean>")]
        [TestCase(typeof(ListTester<List<int>>), "ListTester<List<Int32>>")]
        [TestCase(typeof(ListTester<List<List<int>>>), "ListTester<List<List<Int32>>>")]
#endif
        public void GetDisplayName(Type type, string name)
        {
            Assert.That(TypeHelper.GetDisplayName(type), Is.EqualTo(name));
        }
    }
}
