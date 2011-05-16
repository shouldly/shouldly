// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Reflection;

namespace NUnit.Framework.Tests
{
    public class RandomizerTests
    {
        [Test]
        public void RandomSeedsAreUnique()
        {
            int[] seeds = new int[10];
            for (int i = 0; i < 10; i++)
                seeds[i] = Randomizer.RandomSeed;

            Assert.That(seeds, Is.Unique);
        }

        [Test]
        public void RandomIntsAreUnique()
        {
            Randomizer r = new Randomizer();

            int[] values = new int[10];
            for (int i = 0; i < 10; i++)
                values[i] = r.Next();

            Assert.That(values, Is.Unique);
        }

        [Test]
        public void RandomDoublesAreUnique()
        {
            Randomizer r = new Randomizer();

            double[] values = new double[10];
            for (int i = 0; i < 10; i++)
                values[i] = r.NextDouble();

            Assert.That(values, Is.Unique);
        }

        [Test]
        public void RandomizersWithSameSeedsReturnSameValues()
        {
            Randomizer r1 = new Randomizer(1234);
            Randomizer r2 = new Randomizer(1234);

            for (int i = 0; i < 10; i++)
                Assert.That(r1.NextDouble(), Is.EqualTo(r2.NextDouble()));
        }

        [Test]
        public void RandomizersWithDifferentSeedsReturnDifferentValues()
        {
            Randomizer r1 = new Randomizer(1234);
            Randomizer r2 = new Randomizer(4321);

            for (int i = 0; i < 10; i++)
                Assert.That(r1.NextDouble(), Is.Not.EqualTo(r2.NextDouble()));
        }

        [Test]
        public void ReturnsSameRandomizerForSameParameter()
        {
            ParameterInfo p = testMethod.GetParameters()[0];
            Randomizer r1 = Randomizer.GetRandomizer(p);
            Randomizer r2 = Randomizer.GetRandomizer(p);
            Assert.That(r1, Is.SameAs(r2));
        }

        [Test]
        public void ReturnsSameRandomizerForDifferentParametersOfSameMethod()
        {
            ParameterInfo p1 = testMethod.GetParameters()[0];
            ParameterInfo p2 = testMethod.GetParameters()[1];
            Randomizer r1 = Randomizer.GetRandomizer(p1);
            Randomizer r2 = Randomizer.GetRandomizer(p2);
            Assert.That(r1, Is.SameAs(r2));
        }

        [Test]
        public void ReturnsSameRandomizerForSameMethod()
        {
            Randomizer r1 = Randomizer.GetRandomizer(testMethod);
            Randomizer r2 = Randomizer.GetRandomizer(testMethod);
            Assert.That(r1, Is.SameAs(r2));
        }

        [Test]
        public void ReturnsDifferentRandomizersForDifferentMethods()
        {
            Randomizer r1 = Randomizer.GetRandomizer(testMethod);
            Randomizer r2 = Randomizer.GetRandomizer(MethodInfo.GetCurrentMethod());
            Assert.That(r1, Is.Not.SameAs(r2));
        }

        static readonly MethodInfo testMethod =
            typeof(RandomizerTests).GetMethod("TestMethod", BindingFlags.NonPublic | BindingFlags.Instance);
        private void TestMethod(int x, int y)
        {
        }
    }
}
