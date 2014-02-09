// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework.Tests;

#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
using RangeConstraint = NUnit.Framework.Constraints.RangeConstraint<int>;
#endif

namespace NUnit.Framework.Constraints
{
    #region AllItems
    [TestFixture]
    public class AllItemsTests : MessageChecker
    {
        [Test]
        public void AllItemsAreNotNull()
        {
            object[] c = new object[] { 1, "hello", 3, Environment.OSVersion };
            Assert.That(c, new AllItemsConstraint( Is.Not.Null ));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AllItemsAreNotNullFails()
        {
            object[] c = new object[] { 1, "hello", null, 3 };
            expectedMessage = 
				TextMessageWriter.Pfx_Expected + "all items not null" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< 1, \"hello\", null, 3 >" + Environment.NewLine;
            Assert.That(c, new AllItemsConstraint(new NotConstraint(new EqualConstraint(null))));
        }

        [Test]
        public void AllItemsAreInRange()
        {
            int[] c = new int[] { 12, 27, 19, 32, 45, 99, 26 };
            Assert.That(c, new AllItemsConstraint(new RangeConstraint(10, 100)));
        }

        [Test]
        public void AllItemsAreInRange_UsingIComparer()
        {
            int[] c = new int[] { 12, 27, 19, 32, 45, 99, 26 };
            Assert.That(c, new AllItemsConstraint(new RangeConstraint(10, 100).Using(Comparer.Default)));
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void AllItemsAreInRange_UsingIComparerOfT()
        {
            int[] c = new int[] { 12, 27, 19, 32, 45, 99, 26 };
            Assert.That(c, new AllItemsConstraint(new RangeConstraint(10, 100).Using(Comparer.Default)));
        }

        [Test]
        public void AllItemsAreInRange_UsingComparisonOfT()
        {
            int[] c = new int[] { 12, 27, 19, 32, 45, 99, 26 };
            Assert.That(c, new AllItemsConstraint(new RangeConstraint(10, 100).Using(Comparer.Default)));
        }
#endif

		[Test, ExpectedException(typeof(AssertionException))]
        public void AllItemsAreInRangeFailureMessage()
        {
            int[] c = new int[] { 12, 27, 19, 32, 107, 99, 26 };
            expectedMessage = 
                TextMessageWriter.Pfx_Expected + "all items in range (10,100)" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual   + "< 12, 27, 19, 32, 107, 99, 26 >" + Environment.NewLine;
            Assert.That(c, new AllItemsConstraint(new RangeConstraint(10, 100)));
        }

        [Test]
        public void AllItemsAreInstancesOfType()
        {
            object[] c = new object[] { 'a', 'b', 'c' };
            Assert.That(c, new AllItemsConstraint(new InstanceOfTypeConstraint(typeof(char))));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AllItemsAreInstancesOfTypeFailureMessage()
        {
            object[] c = new object[] { 'a', "b", 'c' };
            expectedMessage = 
                TextMessageWriter.Pfx_Expected + "all items instance of <System.Char>" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual   + "< 'a', \"b\", 'c' >" + Environment.NewLine;
            Assert.That(c, new AllItemsConstraint(new InstanceOfTypeConstraint(typeof(char))));
        }
    }
    #endregion

    #region OneItem
    public class ExactCountConstraintTests : MessageChecker
    {
        private static readonly string[] names = new string[] { "Charlie", "Fred", "Joe", "Charlie" };

        [Test]
        public void ZeroItemsMatch()
        {
            Assert.That(names, new ExactCountConstraint(0, Is.EqualTo("Sam")));
            Assert.That(names, Has.Exactly(0).EqualTo("Sam"));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ZeroItemsMatchFails()
        {
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "no item \"Charlie\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< \"Charlie\", \"Fred\", \"Joe\", \"Charlie\" >" + Environment.NewLine;
            Assert.That(names, new ExactCountConstraint(0, Is.EqualTo("Charlie")));
        }

        [Test]
        public void ExactlyOneItemMatches()
        {
            Assert.That(names, new ExactCountConstraint(1, Is.EqualTo("Fred")));
            Assert.That(names, Has.Exactly(1).EqualTo("Fred"));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ExactlyOneItemMatchFails()
        {
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "exactly one item \"Charlie\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< \"Charlie\", \"Fred\", \"Joe\", \"Charlie\" >" + Environment.NewLine;
            Assert.That(names, new ExactCountConstraint(1, Is.EqualTo("Charlie")));
        }

        [Test]
        public void ExactlyTwoItemsMatch()
        {
            Assert.That(names, new ExactCountConstraint(2, Is.EqualTo("Charlie")));
            Assert.That(names, Has.Exactly(2).EqualTo("Charlie"));
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ExactlyTwoItemsMatchFails()
        {
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "exactly 2 items \"Fred\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< \"Charlie\", \"Fred\", \"Joe\", \"Charlie\" >" + Environment.NewLine;
            Assert.That(names, new ExactCountConstraint(2, Is.EqualTo("Fred")));
        }
    }
    #endregion

    #region CollectionContains
    [TestFixture]
    public class CollectionContainsTests
    {
        [Test]
        public void CanTestContentsOfArray()
        {
            object item = "xyz";
            object[] c = new object[] { 123, item, "abc" };
            Assert.That(c, new CollectionContainsConstraint(item));
        }

        [Test]
        public void CanTestContentsOfArrayList()
        {
            object item = "xyz";
            ArrayList list = new ArrayList( new object[] { 123, item, "abc" } );
            Assert.That(list, new CollectionContainsConstraint(item));
        }

        [Test]
        public void CanTestContentsOfSortedList()
        {
            object item = "xyz";
            SortedList list = new SortedList();
            list.Add("a", 123);
            list.Add("b", item);
            list.Add("c", "abc");
            Assert.That(list.Values, new CollectionContainsConstraint(item));
            Assert.That(list.Keys, new CollectionContainsConstraint("b"));
        }

		[Test]
		public void CanTestContentsOfCollectionNotImplementingIList()
		{
			ICollectionAdapter ints = new ICollectionAdapter(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9});           
			Assert.That(ints, new CollectionContainsConstraint( 9 ));
		}

        [Test]
        public void IgnoreCaseIsHonored()
        {
            Assert.That(new string[] { "Hello", "World" }, 
                new CollectionContainsConstraint("WORLD").IgnoreCase);
        }
		
        [Test]
        public void UsesProvidedIComparer()
        {
            MyComparer comparer = new MyComparer();
            Assert.That(new string[] { "Hello", "World" }, 
                new CollectionContainsConstraint("World").Using(comparer));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparer : IComparer
        {
            public bool Called;

            public int Compare(object x, object y)
            {
                Called = true;
                return Comparer.Default.Compare(x, y);
            }
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void UsesProvidedEqualityComparer()
        {
            MyEqualityComparer comparer = new MyEqualityComparer();
            Assert.That(new string[] { "Hello", "World" }, 
                new CollectionContainsConstraint("World").Using(comparer));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyEqualityComparer : IEqualityComparer
        {
            public bool Called;

            bool IEqualityComparer.Equals(object x, object y)
            {
                Called = true;
                return Comparer.Default.Compare(x, y) == 0;
            }

            int IEqualityComparer.GetHashCode(object x)
            {
                return x.GetHashCode();
            }
        }

        [Test]
        public void UsesProvidedEqualityComparerOfT()
        {
            MyEqualityComparerOfT<string> comparer = new MyEqualityComparerOfT<string>();
            Assert.That(new string[] { "Hello", "World" }, 
                new CollectionContainsConstraint("World").Using(comparer));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyEqualityComparerOfT<T> : IEqualityComparer<T>
        {
            public bool Called;

            bool IEqualityComparer<T>.Equals(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y) == 0;
            }

            int IEqualityComparer<T>.GetHashCode(T x)
            {
                return x.GetHashCode();
            }
        }

        [Test]
        public void UsesProvidedComparerOfT()
        {
            MyComparer<string> comparer = new MyComparer<string>();
            Assert.That(new string[] { "Hello", "World" }, 
                new CollectionContainsConstraint("World").Using(comparer));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparer<T> : IComparer<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

        [Test]
        public void UsesProvidedComparisonOfT()
        {
            MyComparison<string> comparer = new MyComparison<string>();
            Assert.That(new string[] { "Hello", "World" }, 
                new CollectionContainsConstraint("World").Using(new Comparison<string>(comparer.Compare)));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparison<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

        [Test]
        public void ContainsWithRecursiveStructure()
        {
            SelfRecursiveEnumerable item = new SelfRecursiveEnumerable();
            SelfRecursiveEnumerable[] container = new SelfRecursiveEnumerable[] {new SelfRecursiveEnumerable(), item};

            Assert.That(container, new CollectionContainsConstraint(item));
        }

        class SelfRecursiveEnumerable : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return this;
            }
        }

#if CS_3_0 || CS_4_0 || CS_5_0
        [Test]
        public void UsesProvidedLambdaExpression()
        {
            Assert.That(new string[] { "Hello", "World" },
                new CollectionContainsConstraint("WORLD").Using<string>( (x,y)=>String.Compare(x, y, true) ));
        }
#endif
#endif
    }
    #endregion

    #region CollectionEquivalent
    public class CollectionEquivalentTests
    {
        [Test]
        public void EqualCollectionsAreEquivalent()
        {
            ICollection set1 = new ICollectionAdapter("x", "y", "z");
            ICollection set2 = new ICollectionAdapter("x", "y", "z");

            Assert.That(new CollectionEquivalentConstraint(set1).Matches(set2));
        }

        [Test]
        public void WorksWithCollectionsOfArrays()
        {
            byte[] array1 = new byte[] { 0x20, 0x44, 0x56, 0x76, 0x1e, 0xff };
            byte[] array2 = new byte[] { 0x42, 0x52, 0x72, 0xef };
            byte[] array3 = new byte[] { 0x20, 0x44, 0x56, 0x76, 0x1e, 0xff };
            byte[] array4 = new byte[] { 0x42, 0x52, 0x72, 0xef };

            ICollection set1 = new ICollectionAdapter(array1, array2);
            ICollection set2 = new ICollectionAdapter(array3, array4);

            Constraint constraint = new CollectionEquivalentConstraint(set1);
            Assert.That(constraint.Matches(set2));

            set2 = new ICollectionAdapter(array4, array3);
            Assert.That(constraint.Matches(set2));
        }

        [Test]
        public void EquivalentIgnoresOrder()
        {
            ICollection set1 = new ICollectionAdapter("x", "y", "z");
            ICollection set2 = new ICollectionAdapter("z", "y", "x");

            Assert.That(new CollectionEquivalentConstraint(set1).Matches(set2));
        }

        [Test]
        public void EquivalentFailsWithDuplicateElementInActual()
        {
            ICollection set1 = new ICollectionAdapter("x", "y", "z");
            ICollection set2 = new ICollectionAdapter("x", "y", "x");

            Assert.False(new CollectionEquivalentConstraint(set1).Matches(set2));
        }

        [Test]
        public void EquivalentFailsWithDuplicateElementInExpected()
        {
            ICollection set1 = new ICollectionAdapter("x", "y", "x");
            ICollection set2 = new ICollectionAdapter("x", "y", "z");

            Assert.False(new CollectionEquivalentConstraint(set1).Matches(set2));
        }

        [Test]
        public void EquivalentHandlesNull()
        {
            ICollection set1 = new ICollectionAdapter(null, "x", null, "z");
            ICollection set2 = new ICollectionAdapter("z", null, "x", null);

            Assert.That(new CollectionEquivalentConstraint(set1).Matches(set2));
        }

        [Test]
        public void EquivalentHonorsIgnoreCase()
        {
            ICollection set1 = new ICollectionAdapter("x", "y", "z");
            ICollection set2 = new ICollectionAdapter("z", "Y", "X");

            Assert.That(new CollectionEquivalentConstraint(set1).IgnoreCase.Matches(set2));
        }

#if CS_3_0 || CS_4_0 || CS_5_0
        [Test]
        public void EquivalentHonorsUsing()
        {
            ICollection set1 = new ICollectionAdapter("x", "y", "z");
            ICollection set2 = new ICollectionAdapter("z", "Y", "X");

            Assert.That(new CollectionEquivalentConstraint(set1)
                .Using<string>( (x,y)=>String.Compare(x,y,true) )
                .Matches(set2));
        }

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0,Mono-4.0")]
        public void WorksWithHashSets()
        {
            var hash1 = new HashSet<string>(new string[] { "presto", "abracadabra", "hocuspocus" });
            var hash2 = new HashSet<string>(new string[] { "abracadabra", "presto", "hocuspocus" });

            Assert.That(new CollectionEquivalentConstraint(hash1).Matches(hash2));
        }

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0,Mono-4.0")]
        public void WorksWithHashSetAndArray()
        {
            var hash = new HashSet<string>(new string[] { "presto", "abracadabra", "hocuspocus" });
            var array = new string[] { "abracadabra", "presto", "hocuspocus" };

            var constraint = new CollectionEquivalentConstraint(hash);
            Assert.That(constraint.Matches(array));
        }

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0,Mono-4.0")]
        public void WorksWithArrayAndHashSet()
        {
            var hash = new HashSet<string>(new string[] { "presto", "abracadabra", "hocuspocus" });
            var array = new string[] { "abracadabra", "presto", "hocuspocus" };

            var constraint = new CollectionEquivalentConstraint(array);
            Assert.That(constraint.Matches(hash));
        }

        [Test, Platform("Net-3.5,Mono-3.5,Net-4.0,Mono-4.0")]
        public void FailureMessageWithHashSetAndArray()
        {
            var hash = new HashSet<string>(new string[] { "presto", "abracadabra", "hocuspocus" });
            var array = new string[] { "abracadabra", "presto", "hocusfocus" };

            var constraint = new CollectionEquivalentConstraint(hash);
            Assert.False(constraint.Matches(array));

            TextMessageWriter writer = new TextMessageWriter();
            constraint.WriteMessageTo(writer);
            Assert.That(writer.ToString(), Is.EqualTo(
                "  Expected: equivalent to < \"presto\", \"abracadabra\", \"hocuspocus\" >" + Environment.NewLine +
                "  But was:  < \"abracadabra\", \"presto\", \"hocusfocus\" >" + Environment.NewLine));
            Console.WriteLine(writer.ToString());
        }
#endif
    }
    #endregion

    #region CollectionOrdered
    [TestFixture]
    public class CollectionOrderedTests : MessageChecker
    {
        [Test]
        public void IsOrdered()
        {
            ArrayList al = new ArrayList();
            al.Add("x");
            al.Add("y");
            al.Add("z");

            Assert.That(al, Is.Ordered);
        }

        [Test]
        public void IsOrdered_2()
        {
            ArrayList al = new ArrayList();
            al.Add(1);
            al.Add(2);
            al.Add(3);

            Assert.That(al, Is.Ordered);
        }

        [Test]
        public void IsOrderedDescending()
        {
            ArrayList al = new ArrayList();
            al.Add("z");
            al.Add("y");
            al.Add("x");

            Assert.That(al, Is.Ordered.Descending);
        }

        [Test]
        public void IsOrderedDescending_2()
        {
            ArrayList al = new ArrayList();
            al.Add(3);
            al.Add(2);
            al.Add(1);

            Assert.That(al, Is.Ordered.Descending);
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void IsOrdered_Fails()
        {
            ArrayList al = new ArrayList();
            al.Add("x");
            al.Add("z");
            al.Add("y");

            expectedMessage =
                "  Expected: collection ordered" + Environment.NewLine +
                "  But was:  < \"x\", \"z\", \"y\" >" + Environment.NewLine;

            Assert.That(al, Is.Ordered);
        }

        [Test]
        public void IsOrdered_Allows_adjacent_equal_values()
        {
            ArrayList al = new ArrayList();
            al.Add("x");
            al.Add("x");
            al.Add("z");

            Assert.That(al, Is.Ordered);
        }

        [Test, ExpectedException(typeof(ArgumentNullException), 
            ExpectedMessage="index 1", MatchType=MessageMatch.Contains)]
        public void IsOrdered_Handles_null()
        {
            ArrayList al = new ArrayList();
            al.Add("x");
            al.Add(null);
            al.Add("z");

            Assert.That(al, Is.Ordered);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void IsOrdered_TypesMustBeComparable()
        {
            ArrayList al = new ArrayList();
            al.Add(1);
            al.Add("x");

            Assert.That(al, Is.Ordered);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void IsOrdered_AtLeastOneArgMustImplementIComparable()
        {
            ArrayList al = new ArrayList();
            al.Add(new object());
            al.Add(new object());

            Assert.That(al, Is.Ordered);
        }

        [Test]
        public void IsOrdered_Handles_custom_comparison()
        {
            ArrayList al = new ArrayList();
            al.Add(new object());
            al.Add(new object());

            AlwaysEqualComparer comparer = new AlwaysEqualComparer();
            Assert.That(al, Is.Ordered.Using(comparer));
            Assert.That(comparer.Called, "TestComparer was not called");
        }

        [Test]
        public void IsOrdered_Handles_custom_comparison2()
        {
            ArrayList al = new ArrayList();
            al.Add(2);
            al.Add(1);

            TestComparer comparer = new TestComparer();
            Assert.That(al, Is.Ordered.Using(comparer));
            Assert.That(comparer.Called, "TestComparer was not called");
        }

#if CLR_2_0 || CLR_4_0
        [Test]
        public void UsesProvidedComparerOfT()
        {
            ArrayList al = new ArrayList();
            al.Add(1);
            al.Add(2);

            MyComparer<int> comparer = new MyComparer<int>();
            Assert.That(al, Is.Ordered.Using(comparer));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparer<T> : IComparer<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

        [Test]
        public void UsesProvidedComparisonOfT()
        {
            ArrayList al = new ArrayList();
            al.Add(1);
            al.Add(2);

            MyComparison<int> comparer = new MyComparison<int>();
            Assert.That(al, Is.Ordered.Using(new Comparison<int>(comparer.Compare)));
            Assert.That(comparer.Called, "Comparer was not called");
        }

        class MyComparison<T>
        {
            public bool Called;

            public int Compare(T x, T y)
            {
                Called = true;
                return Comparer<T>.Default.Compare(x, y);
            }
        }

#if CS_3_0 || CS_4_0 || CS_5_0
        [Test]
        public void UsesProvidedLambda()
        {
            ArrayList al = new ArrayList();
            al.Add(1);
            al.Add(2);

            Comparison<int> comparer = (x, y) => x.CompareTo(y);
            Assert.That(al, Is.Ordered.Using(comparer));
        }
#endif
#endif

        [Test]
        public void IsOrderedBy()
        {
            ArrayList al = new ArrayList();
            al.Add(new OrderedByTestClass(1));
            al.Add(new OrderedByTestClass(2));

            Assert.That(al, Is.Ordered.By("Value"));
        }

        [Test]
        public void IsOrderedBy_Comparer()
        {
            ArrayList al = new ArrayList();
            al.Add(new OrderedByTestClass(1));
            al.Add(new OrderedByTestClass(2));

            Assert.That(al, Is.Ordered.By("Value").Using(Comparer.Default));
        }

        [Test]
        public void IsOrderedBy_Handles_heterogeneous_classes_as_long_as_the_property_is_of_same_type()
        {
            ArrayList al = new ArrayList();
            al.Add(new OrderedByTestClass(1));
            al.Add(new OrderedByTestClass2(2));

            Assert.That(al, Is.Ordered.By("Value"));
        }

        class OrderedByTestClass
        {
            private int myValue;

            public int Value 
            {
                get { return myValue; }
                set { myValue = value; } 
            }

            public OrderedByTestClass(int value)
            {
                Value = value;
            }
        }

        class OrderedByTestClass2
        {
            private int myValue;
            public int Value 
            {
                get { return myValue; }
                set { myValue = value; } 
            }

            public OrderedByTestClass2(int value)
            {
                Value = value;
            }
        }
    }
    #endregion
}
