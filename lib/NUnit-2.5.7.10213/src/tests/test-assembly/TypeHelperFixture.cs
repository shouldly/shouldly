using System;
using System.Collections;
#if NET_2_0
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

#if NET_2_0
            [TestFixture(typeof(long))]
            public class DoublyNestedGeneric<T> { }
#endif
        }

#if NET_2_0
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

#if NET_2_0
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
