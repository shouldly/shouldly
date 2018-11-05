using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    internal class StringDifferenceHighlighter : IStringDifferenceHighlighter
    {
        private const int MaxDiffLength = 21;
        private const int MaxNumberOfDiffs = 10;

        private readonly Case _sensitivity;
        private readonly Func<string, string> _transform;

        public StringDifferenceHighlighter(Case sensitivity, Func<string, string> transform = null)
        {
            _sensitivity = sensitivity;
            _transform = transform ?? (s => s);
        }
        
        public string HighlightDifferences(string expected, string actual)
        {
            if (expected == null) expected = "null";
            if (actual == null) actual = "null";

            expected = _transform(expected);
            actual = _transform(actual);
            int maxLengthOfStrings = Math.Max(actual.Length, expected.Length);

            var output = new StringBuilder();

            if (maxLengthOfStrings < MaxDiffLength)
            {
                var formattedDetailedDiffString = new FormattedDetailedDifferenceString(actual, expected, _sensitivity, 0);
                output.Append(formattedDetailedDiffString);
                return output.ToString();
            }
            
            var indicesOfAllDiffs = GetIndicesOfAllDifferences(actual, expected);
            var differenceIndexConsolidator = new DifferenceIndexConsolidator(MaxDiffLength, maxLengthOfStrings, indicesOfAllDiffs);
            var startIndicesOfAllDiffs = differenceIndexConsolidator.GetConsolidatedIndices();

            if (startIndicesOfAllDiffs.Count > MaxNumberOfDiffs)
            {
                output.AppendLine($"Showing some of the {indicesOfAllDiffs.Count} differences");
                startIndicesOfAllDiffs = startIndicesOfAllDiffs.Take(MaxNumberOfDiffs).ToList();
            }

            for (int index = 0; index < startIndicesOfAllDiffs.Count; index++)
            {
                var startIndexOfDiffString = startIndicesOfAllDiffs[index];
                var trimmedActualValue = TrimmedValue(actual, startIndexOfDiffString);
                var trimmedExpectedValue = TrimmedValue(expected, startIndexOfDiffString);

                var prefixWithDots = startIndexOfDiffString != 0;
                var suffixWithDots = startIndexOfDiffString + MaxDiffLength < maxLengthOfStrings;
                var formattedDetailedDiffString = new FormattedDetailedDifferenceString(
                    trimmedActualValue, trimmedExpectedValue, _sensitivity,
                    startIndexOfDiffString, prefixWithDots, suffixWithDots);
                if (index > 0)
                {
                    output.AppendLine();
                    output.AppendLine();
                }
                output.Append(formattedDetailedDiffString);
            }
            
            return output.ToString();
        }

        private static string TrimmedValue(string value, int index)
        {
            if (index >= value.Length) return "";

            if (value.Length >= index + MaxDiffLength)
            {
                return value.Substring(index, MaxDiffLength);
            }
            
            return value.Substring(index);
        }

        private static List<int> GetIndicesOfAllDifferences(string actualValue, string expectedValue)
        {
            var indicesOfAllDifferences = new List<int>();
            int maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);

            for (int index = 0; index < maxLengthOfStrings; index++)
            {
                if (!CharAtIndexIsEqual(actualValue, expectedValue, index))
                    indicesOfAllDifferences.Add(index);
            }
            return indicesOfAllDifferences;
        }

        private static bool CharAtIndexIsEqual(string actual, string expected, int index)
        {
            return index < actual.Length &&
                index < expected.Length &&
                Equals(actual[index], expected[index]);
        }
    }
}
