// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Core.Builders;

namespace NUnit.Core.Tests
{
    [TestFixture]
    public class PairwiseTest
    {
        [TestFixture]
        public class LiveTest
        {
            public Hashtable pairsTested = new Hashtable();

            [TestFixtureSetUp]
            public void TestFixtureSetUp()
            {
                pairsTested = new Hashtable();
            }

            [TestFixtureTearDown]
            public void TestFixtureTearDown()
            {
                Assert.That(pairsTested.Count, Is.EqualTo(16));
            }

            [Test, Pairwise]
            public void Test(
                [Values("a", "b", "c")] string a,
                [Values("+", "-")] string b,
                [Values("x", "y")] string c)
            {
                Console.WriteLine("Pairwise: {0} {1} {2}", a, b, c);

                pairsTested[a + b] = null;
                pairsTested[a + c] = null;
                pairsTested[b + c] = null;
            }
        }

        // Test data is taken from various sources. See "Lessons Learned
        // in Software Testing" pp 53-59, for example. For orthogonal cases, see 
        // http://www.freequality.org/sites/www_freequality_org/documents/tools/Tagarray_files/tamatrix.htm
        internal static object[] cases = new object[]
        {
#if ORIGINAL
            new TestCaseData( new int[] { 2, 4 }, 8, 8 ).SetName("Test 2x4"),
            new TestCaseData( new int[] { 2, 2, 2 }, 5, 4 ).SetName("Test 2x2x2"),
            new TestCaseData( new int[] { 3, 2, 2 }, 6, 6 ).SetName("Test 3x2x2"),
            new TestCaseData( new int[] { 3, 2, 2, 2 }, 7, 6 ).SetName("Test 3x2x2x2"),
            new TestCaseData( new int[] { 3, 2, 2, 2, 2 }, 8, 6 ).SetName("Test 3x2x2x2x2"),
            new TestCaseData( new int[] { 3, 2, 2, 2, 2, 2 }, 9, 8 ).SetName("Test 3x2x2x2x2x2"),
            new TestCaseData( new int[] { 3, 3, 3 }, 12, 9 ).SetName("Test 3x3x3"),
            new TestCaseData( new int[] { 4, 4, 4 }, 22, 16 ).SetName("Test 4x4x4"),
            new TestCaseData( new int[] { 5, 5, 5 }, 34, 25 ).SetName("Test 5x5x5")
#else
            new TestCaseData( new int[] { 2, 4 }, 8, 8 ).SetName("Test 2x4"),
            new TestCaseData( new int[] { 2, 2, 2 }, 5, 4 ).SetName("Test 2x2x2"),
            new TestCaseData( new int[] { 3, 2, 2 }, 7, 6 ).SetName("Test 3x2x2"),
            new TestCaseData( new int[] { 3, 2, 2, 2 }, 8, 6 ).SetName("Test 3x2x2x2"),
            new TestCaseData( new int[] { 3, 2, 2, 2, 2 }, 9, 6 ).SetName("Test 3x2x2x2x2"),
            new TestCaseData( new int[] { 3, 2, 2, 2, 2, 2 }, 9, 8 ).SetName("Test 3x2x2x2x2x2"),
            new TestCaseData( new int[] { 3, 3, 3 }, 9, 9 ).SetName("Test 3x3x3"),
            new TestCaseData( new int[] { 4, 4, 4 }, 17, 16 ).SetName("Test 4x4x4"),
            new TestCaseData( new int[] { 5, 5, 5 }, 27, 25 ).SetName("Test 5x5x5")
#endif
        };

        [Test, TestCaseSource("cases")]
        public void Test(int[] dimensions, int bestSoFar, int targetCases)
        {
            int features = dimensions.Length;

            string[][] sources = new string[features][];

            for (int i = 0; i < features; i++)
            {
                string featureName = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(i, 1);

                int n = dimensions[i];
                sources[i] = new string[n];
                for (int j = 0; j < n; j++)
                    sources[i][j] = featureName + j.ToString();
            }

            CombiningStrategy strategy = new PairwiseStrategy(sources);

            Hashtable pairs = new Hashtable();
            int cases = 0;
            foreach (NUnit.Core.Extensibility.ParameterSet testcase in strategy.GetTestCases())
            {
                for (int i = 1; i < features; i++)
                    for (int j = 0; j < i; j++)
                    {
                        string a = testcase.Arguments[i] as string;
                        string b = testcase.Arguments[j] as string;
                        pairs[a + b] = null;
                    }

                ++cases;
            }

            int expectedPairs = 0;
            for (int i = 1; i < features; i++)
                for (int j = 0; j < i; j++)
                    expectedPairs += dimensions[i] * dimensions[j];

            Assert.That(pairs.Count, Is.EqualTo(expectedPairs), "Number of pairs is incorrect");
            Assert.That(cases, Is.AtMost(bestSoFar), "Regression: Number of test cases exceeded target previously reached");
#if DEBUG
            //Assert.That(cases, Is.AtMost(targetCases), "Number of test cases exceeded target");
#endif
        }
    }
}
