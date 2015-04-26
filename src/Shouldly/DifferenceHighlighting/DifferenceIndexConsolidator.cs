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
            _maxDiffLength = maxDiffLength - 1;
            _maxLengthOfStrings = maxLengthOfStrings;
            _indicesOfAllDifferences = indicesOfAllDifferences;
        }


        public List<int> GetConsolidatedIndexes()
        {
            var trimmedIndicesOfDifference = new List<int>();

            foreach (var indexOfDifference in _indicesOfAllDifferences)
            {
                if (!IsIndexOfDifferencesAlreadyAccountedFor(trimmedIndicesOfDifference, indexOfDifference))
                {
                    var trimmedStartOfDifference = Math.Max(0, indexOfDifference - _maxDiffLength / 2);
                    trimmedStartOfDifference = Math.Min(trimmedStartOfDifference, _maxLengthOfStrings - (_maxDiffLength + 1));

                    trimmedIndicesOfDifference.Add(trimmedStartOfDifference);
                }
            }

            return trimmedIndicesOfDifference;
        }

        private bool IsIndexOfDifferencesAlreadyAccountedFor(List<int> trimmedIndicesOfDifference, int currentIndex)
        {
            if (trimmedIndicesOfDifference != null && trimmedIndicesOfDifference.Any())
            {
                var lastTrimmedIndexOfDifference = trimmedIndicesOfDifference.Last();
                if (currentIndex >= lastTrimmedIndexOfDifference && currentIndex <= lastTrimmedIndexOfDifference + _maxDiffLength)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
