// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif
using System.Reflection;
using System.Text;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Builders
{
	public class PairwiseStrategy : CombiningStrategy
	{
		internal class FleaRand
		{
			private const int FleaRandSize = 256;

			private uint b;
			private uint c;
			private uint d;
			private uint z;

			private uint[] m = new uint[FleaRandSize];
			private uint[] r = new uint[FleaRandSize];

			private uint q;

			public FleaRand(uint seed)
			{
				this.b = seed;
				this.c = seed;
				this.d = seed;
				this.z = seed;

				for (int i = 0; i < this.m.Length; i++)
				{
					this.m[i] = seed;
				}

				for (int i = 0; i < 10; i++)
				{
					this.Batch();
				}

				this.q = 0;
			}

			public uint Next()
			{
				if (this.q == 0)
				{
					this.Batch();
					this.q = (uint)this.r.Length - 1;
				}
				else
				{
					this.q--;
				}

				return this.r[this.q];
			}

			private void Batch()
			{
				uint a;
				uint b = this.b;
				uint c = this.c + (++this.z);
				uint d = this.d;

				for (int i = 0; i < this.r.Length; i++)
				{
					a = this.m[b % this.m.Length];
					this.m[b % this.m.Length] = d;
					d = (c << 19) + (c >> 13) + b;
					c = b ^ this.m[i];
					b = a + d;
					this.r[i] = c;
				}

				this.b = b;
				this.c = c;
				this.d = d;
			}
		}

		internal class FeatureInfo
		{
			public const string Names = "abcdefghijklmnopqrstuvwxyz";

			public readonly int Dimension;
			public readonly int Feature;

			public FeatureInfo(int dimension, int feature)
			{
				this.Dimension = dimension;
				this.Feature = feature;
			}

#if DEBUG
			public override string ToString()
			{
				return (this.Dimension + 1).ToString() + FeatureInfo.Names[this.Feature];
			}
#endif
		}

		internal class Tuple
		{
			private readonly ArrayList features = new ArrayList();

			public int Count
			{
				get
				{
					return this.features.Count;
				}
			}

			public FeatureInfo this[int index]
			{
				get
				{
					return (FeatureInfo)this.features[index];
				}
			}

			public void Add(FeatureInfo feature)
			{
				this.features.Add(feature);
			}

#if DEBUG
			public override string ToString()
			{
				StringBuilder sb = new StringBuilder();

				sb.Append('(');

				for (int i = 0; i < this.features.Count; i++)
				{
					if (i > 0)
					{
						sb.Append(' ');
					}

					sb.Append(this.features[i].ToString());
				}

				sb.Append(')');

				return sb.ToString();
			}
#endif
		}

		internal class TupleCollection
		{
			private readonly ArrayList tuples = new ArrayList();

			public int Count
			{
				get
				{
					return this.tuples.Count;
				}
			}

			public Tuple this[int index]
			{
				get
				{
					return (Tuple)this.tuples[index];
				}
			}

			public void Add(Tuple tuple)
			{
				this.tuples.Add(tuple);
			}

			public void RemoveAt(int index)
			{
				this.tuples.RemoveAt(index);
			}
		}

		internal class TestCase
		{
			public readonly int[] Features;

			public TestCase(int numberOfDimensions)
			{
				this.Features = new int[numberOfDimensions];
			}

			public bool IsTupleCovered(Tuple tuple)
			{
				for (int i = 0; i < tuple.Count; i++)
				{
					if (this.Features[tuple[i].Dimension] != tuple[i].Feature)
					{
						return false;
					}
				}

				return true;
			}

#if DEBUG
			public override string ToString()
			{
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < this.Features.Length; i++)
				{
					if (i > 0)
					{
						sb.Append(' ');
					}

					sb.Append(i + 1);
					sb.Append(FeatureInfo.Names[this.Features[i]]);
				}

				return sb.ToString();
			}
#endif
		}

		internal class TestCaseCollection : IEnumerable
		{
			private readonly ArrayList testCases = new ArrayList();

			public void Add(TestCase testCase)
			{
				this.testCases.Add(testCase);
			}

			public IEnumerator GetEnumerator()
			{
				return this.testCases.GetEnumerator();
			}

			public bool IsTupleCovered(Tuple tuple)
			{
				foreach (TestCase testCase in this.testCases)
				{
					if (testCase.IsTupleCovered(tuple))
					{
						return true;
					}
				}

				return false;
			}
		}

		internal class PairwiseTestCaseGenerator
		{
			private const int MaxTupleLength = 2;

			private readonly FleaRand random = new FleaRand(0);

			private readonly int[] dimensions;

			private readonly TupleCollection[][] uncoveredTuples;

			private readonly int[][] currentTupleLength;

			private readonly TestCaseCollection testCases = new TestCaseCollection();

			public PairwiseTestCaseGenerator(int[] dimensions)
			{
				this.dimensions = dimensions;

				this.uncoveredTuples = new TupleCollection[this.dimensions.Length][];

				for (int d = 0; d < this.uncoveredTuples.Length; d++)
				{
					this.uncoveredTuples[d] = new TupleCollection[this.dimensions[d]];

					for (int f = 0; f < this.dimensions[d]; f++)
					{
						this.uncoveredTuples[d][f] = new TupleCollection();
					}
				}

				this.currentTupleLength = new int[this.dimensions.Length][];

				for (int d = 0; d < this.dimensions.Length; d++)
				{
					this.currentTupleLength[d] = new int[this.dimensions[d]];
				}
			}

			public IEnumerable GetTestCases()
			{
				this.CreateTestCases();

				this.SelfTest();

				return this.testCases;
			}

			private void CreateTestCases()
			{
				while (true)
				{
					this.ExtendTupleSet();

					Tuple tuple = this.FindTupleToCover();

					if (tuple == null)
					{
						return;
					}

					TestCase testCase = this.FindGoodTestCase(tuple);

					this.RemoveTuplesCoveredBy(testCase);

					this.testCases.Add(testCase);
				}
			}

			private void ExtendTupleSet()
			{
				for (int d = 0; d < this.dimensions.Length; d++)
				{
					for (int f = 0; f < this.dimensions[d]; f++)
					{
						this.ExtendTupleSet(d, f);
					}
				}
			}

			private void ExtendTupleSet(int dimension, int feature)
			{
				// If tuples for [dimension][feature] already exists, it's no needs to add more tuples.
				if (this.uncoveredTuples[dimension][feature].Count > 0)
				{
					return;
				}

				// If maximum tuple length for [dimension][feature] is reached, it's no needs to add more tuples.
				if (this.currentTupleLength[dimension][feature] == MaxTupleLength)
				{
					return;
				}

				this.currentTupleLength[dimension][feature]++;

				int tupleLength = this.currentTupleLength[dimension][feature];

				if (tupleLength == 1)
				{
					Tuple tuple = new Tuple();

					tuple.Add(new FeatureInfo(dimension, feature));

					if (this.testCases.IsTupleCovered(tuple))
					{
						return;
					}

					this.uncoveredTuples[dimension][feature].Add(tuple);
				}
				else
				{
					for (int d = 0; d < this.dimensions.Length; d++)
					{
						for (int f = 0; f < this.dimensions[d]; f++)
						{
							Tuple tuple = new Tuple();
							tuple.Add(new FeatureInfo(d, f));

							if (tuple[0].Dimension == dimension)
							{
								continue;
							}

							tuple.Add(new FeatureInfo(dimension, feature));

							if (this.testCases.IsTupleCovered(tuple))
							{
								continue;
							}

							this.uncoveredTuples[dimension][feature].Add(tuple);
						}
					}
				}
			}

			private Tuple FindTupleToCover()
			{
				int tupleLength = MaxTupleLength;
				int tupleCount = 0;
				Tuple tuple = null;

				for (int d = 0; d < this.dimensions.Length; d++)
				{
					for (int f = 0; f < this.dimensions[d]; f++)
					{
						if (this.currentTupleLength[d][f] < tupleLength)
						{
							tupleLength = this.currentTupleLength[d][f];
							tupleCount = this.uncoveredTuples[d][f].Count;
							tuple = this.uncoveredTuples[d][f][0];
						}
						else
						{
							if (this.currentTupleLength[d][f] == tupleLength && this.uncoveredTuples[d][f].Count > tupleCount)
							{
								tupleCount = this.uncoveredTuples[d][f].Count;
								tuple = this.uncoveredTuples[d][f][0];
							}
						}
					}
				}

				return tuple;
			}

			private TestCase FindGoodTestCase(Tuple tuple)
			{
				TestCase bestTest = null;
				int bestCoverage = -1;

				for (int i = 0; i < 5; i++)
				{
					TestCase test = new TestCase(this.dimensions.Length);

					int coverage = this.CreateTestCase(tuple, test);

					if (coverage > bestCoverage)
					{
						bestTest = test;
						bestCoverage = coverage;
					}
				}

				return bestTest;
			}

			private int CreateTestCase(Tuple tuple, TestCase test)
			{
				// Create a random test case...
				for (int i = 0; i < test.Features.Length; i++)
				{
					test.Features[i] = (int)(this.random.Next() % this.dimensions[i]);
				}

				// ...and inject the tuple into it!
				for (int i = 0; i < tuple.Count; i++)
				{
					test.Features[tuple[i].Dimension] = tuple[i].Feature;
				}

				return this.MaximizeCoverage(test, tuple);
			}

			private int MaximizeCoverage(TestCase test, Tuple tuple)
			{
				int[] dimensionOrder = this.GetMutableDimensions(tuple);

				while (true)
				{
					bool progress = false;
					int totalCoverage = 1;

					// Scramble dimensions.
					for (int i = dimensionOrder.Length; i > 1; i--)
					{
						int j = (int)(this.random.Next() % i);
						int t = dimensionOrder[i - 1];
						dimensionOrder[i - 1] = dimensionOrder[j];
						dimensionOrder[j] = t;
					}

					// For each dimension that can be modified...
					for (int i = 0; i < dimensionOrder.Length; i++)
					{
						int d = dimensionOrder[i];

						ArrayList bestFeatures = new ArrayList();

						int bestCoverage = this.CountTuplesCovered(test, d, test.Features[d]);

						int bestTupleLength = this.currentTupleLength[d][test.Features[d]];

						// For each feature that can be modified, check if it can extend coverage.
						for (int f = 0; f < this.dimensions[d]; f++)
						{
							test.Features[d] = f;

							int coverage = this.CountTuplesCovered(test, d, f);

							if (this.currentTupleLength[d][f] < bestTupleLength)
							{
								progress = true;
								bestTupleLength = this.currentTupleLength[d][f];
								bestCoverage = coverage;
								bestFeatures.Clear();
								bestFeatures.Add(f);
							}
							else
							{
								if (this.currentTupleLength[d][f] == bestTupleLength && coverage >= bestCoverage)
								{
									if (coverage > bestCoverage)
									{
										progress = true;
										bestCoverage = coverage;
										bestFeatures.Clear();
									}

									bestFeatures.Add(f);
								}
							}
						}

						if (bestFeatures.Count == 1)
						{
							test.Features[d] = (int)bestFeatures[0];
						}
						else
						{
							test.Features[d] = (int)bestFeatures[(int)(this.random.Next() % bestFeatures.Count)];
						}

						totalCoverage += bestCoverage;
					}

					if (!progress)
					{
						return totalCoverage;
					}
				}
			}

			private int[] GetMutableDimensions(Tuple tuple)
			{
				bool[] immutableDimensions = new bool[this.dimensions.Length];

				for (int i = 0; i < tuple.Count; i++)
				{
					immutableDimensions[tuple[i].Dimension] = true;
				}

				ArrayList mutableDimensions = new ArrayList();

				for (int i = 0; i < this.dimensions.Length; i++)
				{
					if (!immutableDimensions[i])
					{
						mutableDimensions.Add(i);
					}
				}

				return (int[])mutableDimensions.ToArray(typeof(int));
			}

			private int CountTuplesCovered(TestCase test, int dimension, int feature)
			{
				int tuplesCovered = 0;

				TupleCollection tuples = this.uncoveredTuples[dimension][feature];

				for (int i = 0; i < tuples.Count; i++)
				{
					if (test.IsTupleCovered(tuples[i]))
					{
						tuplesCovered++;
					}
				}

				return tuplesCovered;
			}

			private void RemoveTuplesCoveredBy(TestCase testCase)
			{
				for (int d = 0; d < this.uncoveredTuples.Length; d++)
				{
					for (int f = 0; f < this.uncoveredTuples[d].Length; f++)
					{
						TupleCollection tuples = this.uncoveredTuples[d][f];

						for (int i = tuples.Count - 1; i >= 0; i--)
						{
							if (testCase.IsTupleCovered(tuples[i]))
							{
								tuples.RemoveAt(i);
							}
						}
					}
				}
			}

			private void SelfTest()
			{
				for (int d1 = 0; d1 < this.dimensions.Length - 1; d1++)
				{
					for (int d2 = d1 + 1; d2 < this.dimensions.Length; d2++)
					{
						for (int f1 = 0; f1 < this.dimensions[d1]; f1++)
						{
							for (int f2 = 0; f2 < this.dimensions[d2]; f2++)
							{
								Tuple tuple = new Tuple();
								tuple.Add(new FeatureInfo(d1, f1));
								tuple.Add(new FeatureInfo(d2, f2));

								if (!this.testCases.IsTupleCovered(tuple))
								{
									throw new ApplicationException("PairwiseStrategy self-test failed : Not all pairs are covered!");
								}
							}
						}
					}
				}
			}
		}

		public PairwiseStrategy(IEnumerable[] sources) : base(sources) { }

		public override IEnumerable GetTestCases()
		{
			ArrayList[] valueSet = CreateValueSet();
			int[] dimensions = CreateDimensions(valueSet);

			IEnumerable pairwiseTestCases = new PairwiseTestCaseGenerator(dimensions).GetTestCases();

#if CLR_2_0 || CLR_4_0
            List<ParameterSet> testCases = new List<ParameterSet>();
#else
            ArrayList testCases = new ArrayList();
#endif

			foreach (TestCase pairwiseTestCase in pairwiseTestCases)
			{
				object[] testData = new object[pairwiseTestCase.Features.Length];

				for (int i = 0; i < pairwiseTestCase.Features.Length; i++)
				{
					testData[i] = valueSet[i][pairwiseTestCase.Features[i]];
				}

                ParameterSet testCase = new ParameterSet();
                testCase.Arguments = testData;

				testCases.Add(testCase);
			}

			return testCases;
		}

		private ArrayList[] CreateValueSet()
		{
			ArrayList[] valueSet = new ArrayList[Sources.Length];

			for (int i = 0; i < valueSet.Length; i++)
			{
				ArrayList values = new ArrayList();

				foreach (object value in Sources[i])
				{
					values.Add(value);
				}

				valueSet[i] = values;
			}

			return valueSet;
		}

		private int[] CreateDimensions(ArrayList[] valueSet)
		{
			int[] dimensions = new int[valueSet.Length];

			for (int i = 0; i < valueSet.Length; i++)
			{
				dimensions[i] = valueSet[i].Count;
			}

			return dimensions;
		}
	}
}
