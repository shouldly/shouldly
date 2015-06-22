using System;
using System.Collections.Generic;
using System.Linq;

namespace Shouldly.DifferenceHighlighting
{
    internal class DifferenceIndexConsolidator
    {
        private readonly int _maxDiffLength;
        private readonly int _maxLengthOfStrings;
        private readonly List<int> _indicesOfAllDifferences;

        public DifferenceIndexConsolidator(int maxDiffLength, int maxLengthOfStrings, List<int> indicesOfAllDifferences)
        {
            _maxDiffLength = maxDiffLength;
            _maxLengthOfStrings = maxLengthOfStrings;
            _indicesOfAllDifferences = indicesOfAllDifferences.OrderBy(x => x).ToList();
        }

        public List<int> GetConsolidatedIndices()
        {
            var diffIndicesWithDistanceToNextDiffIndex = CalculateDistanceBetweenDiffIndices(_indicesOfAllDifferences);
            var consolidatedIndices = new List<List<int>>();

            var diffDistanceCounter = 0;
            var currentConsolidationRun = new List<int>();

            foreach (var diffIndexWithDistance in diffIndicesWithDistanceToNextDiffIndex)
            {
                currentConsolidationRun.Add(diffIndexWithDistance.Key);
                diffDistanceCounter = diffDistanceCounter + diffIndexWithDistance.Value;
                if (diffDistanceCounter >= _maxDiffLength || diffIndexWithDistance.Value == 0)
                {
                    consolidatedIndices.Add(currentConsolidationRun);
                    diffDistanceCounter = 0;
                    currentConsolidationRun = new List<int>();
                }
            }

            var centeredConsolidatedDiffs = consolidatedIndices.Select(CenterDiffIndices).ToList();

            return centeredConsolidatedDiffs;
        }

        private int CenterDiffIndices(List<int> diffIndices)
        {
            var averageIndexOfAllDiffs = Math.Round(diffIndices.Average());
            var requiredAdjustmentToCenterDiff = (_maxDiffLength - 1) / 2;
            int adjustedIndex;
            var isCloseToTheEnd = averageIndexOfAllDiffs + requiredAdjustmentToCenterDiff >= _maxLengthOfStrings;
            if (!isCloseToTheEnd)
            {
                adjustedIndex = (int) Math.Max(0, averageIndexOfAllDiffs - requiredAdjustmentToCenterDiff);
            }
            else
            {
                adjustedIndex = _maxLengthOfStrings - _maxDiffLength;
            }

            return adjustedIndex;
        }

        private List<KeyValuePair<int, int>> CalculateDistanceBetweenDiffIndices(List<int> diffIndices)
        {
            var diffToNextIndex = diffIndices.Zip(diffIndices.Skip(1), (i1, i2) => i2 - i1).ToList();

            diffToNextIndex.Add(0);

            var result = diffIndices.Zip(diffToNextIndex, (i1, i2) => new KeyValuePair<int, int>(i1, i2)).ToList();
            return result;
        }
    }

#if !net40
    static class ZipPolyfill
    {
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (resultSelector == null) throw new ArgumentNullException("resultSelector");
            return ZipIterator(first, second, resultSelector);
        }

        private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>
            (IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            using (IEnumerator<TFirst> e1 = first.GetEnumerator())
            using (IEnumerator<TSecond> e2 = second.GetEnumerator())
                while (e1.MoveNext() && e2.MoveNext())
                    yield return resultSelector(e1.Current, e2.Current);
        }
    }
#endif
}
