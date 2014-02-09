// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;

namespace NUnit.Core.Filters
{
	/// <summary>
	/// NotFilter negates the operation of another filter
	/// </summary>
	[Serializable]
	public class NotFilter : TestFilter
	{
		ITestFilter baseFilter;
        bool topLevel = false;

        /// <summary>
        /// Construct a not filter on another filter. The topLevel
        /// argument is used to trigger special handling when tests
        /// are being excluded.
        /// </summary>
        /// <param name="baseFilter">The filter to be negated</param>
        public NotFilter(ITestFilter baseFilter) : this(baseFilter, false) { }

        /// <summary>
        /// Construct a not filter on another filter. The topLevel
        /// argument is used to trigger special handling when tests
        /// are being excluded.
        /// </summary>
        /// <param name="baseFilter">The filter to be negated</param>
        /// <param name="topLevel">Indicates whether this is a top level NotFilter</param>
        public NotFilter(ITestFilter baseFilter, bool topLevel)
        {
            this.baseFilter = baseFilter;
            this.topLevel = topLevel;
        }

        /// <summary>
        /// Indicates whether this is a top-level NotFilter,
        /// requiring special handling of Explicit
        /// </summary>
        public bool TopLevel
        {
            get { return topLevel; }
            set { topLevel = value; }
        }

		/// <summary>
		/// Gets the base filter
		/// </summary>
		public ITestFilter BaseFilter
		{
			get { return baseFilter; }
		}

		/// <summary>
		/// Check whether the filter matches a test
		/// </summary>
		/// <param name="test">The test to be matched</param>
		/// <returns>True if it matches, otherwise false</returns>
		public override bool Match( ITest test )
		{
            if (topLevel && test.RunState == RunState.Explicit)
                return false;

			return !baseFilter.Pass( test );
		}

		/// <summary>
		/// Determine whether any descendant of the test matches the filter criteria.
		/// </summary>
		/// <param name="test">The test to be matched</param>
		/// <returns>True if at least one descendant matches the filter criteria</returns>
		protected override bool MatchDescendant(ITest test)
		{
			if (!test.IsSuite || test.Tests == null || topLevel && test.RunState == RunState.Explicit)
				return false;

			foreach (ITest child in test.Tests)
			{
				if (Match(child) || MatchDescendant(child))
					return true;
			}

			return false;
		}

        /// <summary>
        /// Return string representation of the filter
        /// </summary>
        public override string ToString()
        {
            return "not " + baseFilter.ToString();
        }
	}
}
