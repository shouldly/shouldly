// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif
using NUnit.Framework;

public class MyNoNamespaceClass { }

namespace NUnit.TestData.TypeHelperFixture
{
    // NOTE: TestFixture attributes are not used by the unit tests but
    // are provided so these class may be loaded in the NUnit gui
    // for inspection.

    [TestFixture]
    public class SimpleClass
    {
    }

    public class ContainerClass
    {
        public class NestedClass
        {
            [TestFixture]
            public class DoublyNestedClass { }

#if CLR_2_0 || CLR_4_0
            [TestFixture(typeof(long))]
            public class DoublyNestedGeneric<T> { }
#endif
        }

#if CLR_2_0 || CLR_4_0
        public class NestedGeneric<T>
        {
            [TestFixture(typeof(int))]
            public class DoublyNestedClass { }

            [TestFixture(typeof(int),typeof(string))]
            public class DoublyNestedGeneric<U>
            {
            }
        }
#endif
    }

#if CLR_2_0 || CLR_4_0
    [TestFixture(typeof(int[]))]
    [TestFixture(typeof(List<int>))]
    [TestFixture(typeof(List<string>))]
    [TestFixture(typeof(List<List<int>>))]
    public class ListTester<TList> where TList : System.Collections.IList
    {
    }

    [TestFixture(typeof(int), typeof(decimal), typeof(string))]
    public class GenericClass<T, U, V>
    {
    }

    public class GenericContainerClass<T>
    {
        public class NestedClass
        {
            [TestFixture(typeof(int[]))]
            public class DoublyNestedClass { }

            [TestFixture(typeof(int), typeof(string))]
            [TestFixture(typeof(long), typeof(string))]
            public class DoublyNestedGeneric<U> { }
        }

        public class NestedGeneric<U>
        {
            [TestFixture(typeof(string), typeof(int))]
            public class DoublyNestedClass { }

            [TestFixture(typeof(string), typeof(int), typeof(bool))]
            public class DoublyNestedGeneric<V>
            {
            }
        }
    }
#endif
}
