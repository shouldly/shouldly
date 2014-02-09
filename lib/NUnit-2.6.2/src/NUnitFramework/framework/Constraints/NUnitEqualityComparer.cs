// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.IO;
using System.Collections;
#if CLR_2_0 || CLR_4_0
using System.Reflection;
#endif

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// NUnitEqualityComparer encapsulates NUnit's handling of
    /// equality tests between objects.
    /// </summary>
    public class NUnitEqualityComparer : INUnitEqualityComparer
    {
        #region Static and Instance Fields
        /// <summary>
        /// If true, all string comparisons will ignore case
        /// </summary>
        private bool caseInsensitive;

        /// <summary>
        /// If true, arrays will be treated as collections, allowing
        /// those of different dimensions to be compared
        /// </summary>
        private bool compareAsCollection;

        /// <summary>
        /// Comparison objects used in comparisons for some constraints.
        /// </summary>
        private ArrayList externalComparers = new ArrayList();

        private ArrayList failurePoints;

        private static readonly int BUFFER_SIZE = 4096;
        #endregion

        #region Properties

        /// <summary>
        /// Returns the default NUnitEqualityComparer
        /// </summary>
        public static NUnitEqualityComparer Default
        {
            get { return new NUnitEqualityComparer(); }
        }
        /// <summary>
        /// Gets and sets a flag indicating whether case should
        /// be ignored in determining equality.
        /// </summary>
        public bool IgnoreCase
        {
            get { return caseInsensitive; }
            set { caseInsensitive = value; }
        }

        /// <summary>
        /// Gets and sets a flag indicating that arrays should be
        /// compared as collections, without regard to their shape.
        /// </summary>
        public bool CompareAsCollection
        {
            get { return compareAsCollection; }
            set { compareAsCollection = value; }
        }

        /// <summary>
        /// Gets and sets an external comparer to be used to
        /// test for equality. It is applied to members of
        /// collections, in place of NUnit's own logic.
        /// </summary>
        public IList ExternalComparers
        {
            get { return externalComparers; }
        }

        /// <summary>
        /// Gets the list of failure points for the last Match performed.
        /// </summary>
        public IList FailurePoints
        {
            get { return failurePoints; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compares two objects for equality within a tolerance.
        /// </summary>
        public bool AreEqual(object x, object y, ref Tolerance tolerance)
        {
            return AreEqual(x, y, new EnumerableRecursionHelper(), ref tolerance);
        }

        private bool AreEqual(object x, object y, EnumerableRecursionHelper recursionHelper, ref Tolerance tolerance)
        {
            this.failurePoints = new ArrayList();

            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            if (object.ReferenceEquals(x, y))
                return true;

            Type xType = x.GetType();
            Type yType = y.GetType();

            EqualityAdapter externalComparer = GetExternalComparer(x, y);
            if (externalComparer != null)
                return externalComparer.AreEqual(x, y);

            if (xType.IsArray && yType.IsArray && !compareAsCollection)
                return ArraysEqual((Array) x, (Array) y, recursionHelper, ref tolerance);

            if (x is IDictionary && y is IDictionary)
                return DictionariesEqual((IDictionary) x, (IDictionary) y, recursionHelper, ref tolerance);

            //if (x is ICollection && y is ICollection)
            //    return CollectionsEqual((ICollection)x, (ICollection)y, ref tolerance);

            if (x is IEnumerable && y is IEnumerable && !(x is string && y is string))
                return EnumerablesEqual((IEnumerable) x, (IEnumerable) y, recursionHelper, ref tolerance);

            if (x is string && y is string)
                return StringsEqual((string) x, (string) y);

            if (x is Stream && y is Stream)
                return StreamsEqual((Stream) x, (Stream) y);

            if (x is DirectoryInfo && y is DirectoryInfo)
                return DirectoriesEqual((DirectoryInfo) x, (DirectoryInfo) y);

            if (Numerics.IsNumericType(x) && Numerics.IsNumericType(y))
                return Numerics.AreEqual(x, y, ref tolerance);

            if (tolerance != null && tolerance.Value is TimeSpan)
            {
                TimeSpan amount = (TimeSpan) tolerance.Value;

                if (x is DateTime && y is DateTime)
                    return ((DateTime) x - (DateTime) y).Duration() <= amount;

                if (x is TimeSpan && y is TimeSpan)
                    return ((TimeSpan) x - (TimeSpan) y).Duration() <= amount;
            }

#if CLR_2_0 || CLR_4_0
            if (FirstImplementsIEquatableOfSecond(xType, yType))
                return InvokeFirstIEquatableEqualsSecond(x, y);
            else if (FirstImplementsIEquatableOfSecond(yType, xType))
                return InvokeFirstIEquatableEqualsSecond(y, x);
#endif

            return x.Equals(y);
        }

#if CLR_2_0 || CLR_4_0
    	private static bool FirstImplementsIEquatableOfSecond(Type first, Type second)
    	{
    		Type[] equatableArguments = GetEquatableGenericArguments(first);

    		foreach (var xEquatableArgument in equatableArguments)
    			if (xEquatableArgument.Equals(second))
    				return true;

    		return false;
    	}

    	private static Type[] GetEquatableGenericArguments(Type type)
    	{
    		return Array.ConvertAll(Array.FindAll(type.GetInterfaces(),
                                    delegate(Type @interface)
                                    {
                                  	    return @interface.IsGenericType &&
                                  	           @interface.GetGenericTypeDefinition().Equals(typeof (IEquatable<>));
                                    }),
								    delegate(Type iEquatableInterface)
								    {
								  	    return iEquatableInterface.GetGenericArguments()[0];
								    });
    	}

    	private static bool InvokeFirstIEquatableEqualsSecond(object first, object second)
    	{
    		MethodInfo equals = typeof (IEquatable<>).MakeGenericType(second.GetType()).GetMethod("Equals");

    		return (bool) equals.Invoke(first, new object[] {second});
    	}
#endif

    	#endregion

        #region Helper Methods

        private EqualityAdapter GetExternalComparer(object x, object y)
        {
            foreach (EqualityAdapter adapter in externalComparers)
                if (adapter.CanCompare(x, y))
                    return adapter;

            return null;
        }

        /// <summary>
        /// Helper method to compare two arrays
        /// </summary>
        private bool ArraysEqual(Array x, Array y, EnumerableRecursionHelper recursionHelper, ref Tolerance tolerance)
        {
            int rank = x.Rank;

            if (rank != y.Rank)
                return false;

            for (int r = 1; r < rank; r++)
                if (x.GetLength(r) != y.GetLength(r))
                    return false;

            return EnumerablesEqual((IEnumerable)x, (IEnumerable)y, recursionHelper, ref tolerance);
        }

        private bool DictionariesEqual(IDictionary x, IDictionary y, EnumerableRecursionHelper recursionHelper, ref Tolerance tolerance)
        {
            if (x.Count != y.Count)
                return false;

            CollectionTally tally = new CollectionTally(this, x.Keys);
            if (!tally.TryRemove(y.Keys) || tally.Count > 0)
                return false;

            foreach (object key in x.Keys)
                if (!AreEqual(x[key], y[key], recursionHelper, ref tolerance))
                    return false;

            return true;
        }

        private bool StringsEqual(string x, string y)
        {
            string s1 = caseInsensitive ? x.ToLower() : x;
            string s2 = caseInsensitive ? y.ToLower() : y;

            return s1.Equals(s2);
        }
        
        private bool EnumerablesEqual(IEnumerable expected, IEnumerable actual, EnumerableRecursionHelper recursionHelper, ref Tolerance tolerance)
        {
            if (recursionHelper.CheckRecursion(expected, actual))
                return false;

            IEnumerator expectedEnum = expected.GetEnumerator();
            IEnumerator actualEnum = actual.GetEnumerator();

            int count;
            for (count = 0; ; count++)
            {
                bool expectedHasData = expectedEnum.MoveNext();
                bool actualHasData = actualEnum.MoveNext();

                if (!expectedHasData && !actualHasData)
                    return true;

                if (expectedHasData != actualHasData ||
                    !AreEqual(expectedEnum.Current, actualEnum.Current, recursionHelper, ref tolerance))
                {
                    FailurePoint fp = new FailurePoint();
                    fp.Position = count;
                    fp.ExpectedHasData = expectedHasData;
                    if (expectedHasData)
                            fp.ExpectedValue = expectedEnum.Current;
                    fp.ActualHasData = actualHasData;
                    if (actualHasData)
                        fp.ActualValue = actualEnum.Current;
                    failurePoints.Insert(0, fp);
                    return false;
                }
            }
        }

        /// <summary>
        /// Method to compare two DirectoryInfo objects
        /// </summary>
        /// <param name="x">first directory to compare</param>
        /// <param name="y">second directory to compare</param>
        /// <returns>true if equivalent, false if not</returns>
        private bool DirectoriesEqual(DirectoryInfo x, DirectoryInfo y)
        {
            // Do quick compares first
            if (x.Attributes != y.Attributes ||
                x.CreationTime != y.CreationTime ||
                x.LastAccessTime != y.LastAccessTime)
            {
                return false;
            }

            // TODO: Find a cleaner way to do this
            return new SamePathConstraint(x.FullName).Matches(y.FullName);
        }

        private bool StreamsEqual(Stream x, Stream y)
        {
            if (x == y) return true;

            if (!x.CanRead)
                throw new ArgumentException("Stream is not readable", "expected");
            if (!y.CanRead)
                throw new ArgumentException("Stream is not readable", "actual");
            if (!x.CanSeek)
                throw new ArgumentException("Stream is not seekable", "expected");
            if (!y.CanSeek)
                throw new ArgumentException("Stream is not seekable", "actual");

            if (x.Length != y.Length) return false;

            byte[] bufferExpected = new byte[BUFFER_SIZE];
            byte[] bufferActual = new byte[BUFFER_SIZE];

            BinaryReader binaryReaderExpected = new BinaryReader(x);
            BinaryReader binaryReaderActual = new BinaryReader(y);

            long expectedPosition = x.Position;
            long actualPosition = y.Position;

            try
            {
                binaryReaderExpected.BaseStream.Seek(0, SeekOrigin.Begin);
                binaryReaderActual.BaseStream.Seek(0, SeekOrigin.Begin);

                for (long readByte = 0; readByte < x.Length; readByte += BUFFER_SIZE)
                {
                    binaryReaderExpected.Read(bufferExpected, 0, BUFFER_SIZE);
                    binaryReaderActual.Read(bufferActual, 0, BUFFER_SIZE);

                    for (int count = 0; count < BUFFER_SIZE; ++count)
                    {
                        if (bufferExpected[count] != bufferActual[count])
                        {
                            failurePoints.Insert(0, readByte + count);
                            //FailureMessage.WriteLine("\tIndex : {0}", readByte + count);
                            return false;
                        }
                    }
                }
            }
            finally
            {
                x.Position = expectedPosition;
                y.Position = actualPosition;
            }

            return true;
        }
        #endregion

        #region Nested FailurePoint Class

        /// <summary>
        /// FailurePoint class represents one point of failure
        /// in an equality test.
        /// </summary>
        public class FailurePoint
        {
            /// <summary>
            /// The location of the failure
            /// </summary>
            public int Position;

            /// <summary>
            /// The expected value
            /// </summary>
            public object ExpectedValue;

            /// <summary>
            /// The actual value
            /// </summary>
            public object ActualValue;

            /// <summary>
            /// Indicates whether the expected value is valid
            /// </summary>
            public bool ExpectedHasData;

            /// <summary>
            /// Indicates whether the actual value is valid
            /// </summary>
            public bool ActualHasData;
        }

        #endregion

        #region Nested EnumerableRecursionHelper class

        class EnumerableRecursionHelper
        {
            readonly Hashtable table = new Hashtable();

            public bool CheckRecursion(IEnumerable expected, IEnumerable actual)
            {
                UnorderedReferencePair key = new UnorderedReferencePair(expected, actual);

                if (table.Contains(key))
                    return true;

                table.Add(key, null);
                return false;
            }

#if CLR_2_0 || CLR_4_0
            class UnorderedReferencePair : IEquatable<UnorderedReferencePair>
#else
            class UnorderedReferencePair
#endif
            {
                private readonly object first;
                private readonly object second;

                public UnorderedReferencePair(object first, object second)
                {
                    this.first = first;
                    this.second = second;
                }

                public bool Equals(UnorderedReferencePair other)
                {
                    return (Equals(first, other.first) && Equals(second, other.second)) || 
                           (Equals(first, other.second) && Equals(second, other.first));
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    return obj is UnorderedReferencePair && Equals((UnorderedReferencePair) obj);
                }

                public override int GetHashCode()
                {
                    unchecked
                    {
                        return ((first != null ? first.GetHashCode() : 0)*397) ^ ((second != null ? second.GetHashCode() : 0)*397);
                    }
                }
            }
        }

        #endregion
    }
}