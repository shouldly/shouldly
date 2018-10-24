using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    internal class StringDifferenceHighlighter : IStringDifferenceHighlighter
    {
        int maxDiffLength = 21;
        int maxNumberOfDiffs = 10;

        readonly Case _sensitivity;
        readonly Func<string, string> _transform;

        public StringDifferenceHighlighter(Case sensitivity, Func<string, string> transform = null)
        {
            _sensitivity = sensitivity;
            _transform = transform ?? (s => s);
        }
        public string HighlightDifferences(string expected, string actual)
        {
            if (expected == null || actual == null) return null;

            expected = _transform(expected);
            actual = _transform(actual);
            int maxLengthOfStrings = Math.Max(actual.Length, expected.Length);

            var output = new StringBuilder();

            if (maxLengthOfStrings < maxDiffLength)
            {
                var formattedDetailedDiffString = new FormattedDetailedDifferenceString(actual, expected, _sensitivity, 0);
                output.Append(formattedDetailedDiffString);
                return output.ToString();
            }
            else
            {
                var indicesOfAllDiffs = GetIndicesOfAllDifferences(actual, expected);
                var differenceIndexConsolidator = new DifferenceIndexConsolidator(maxDiffLength, maxLengthOfStrings, indicesOfAllDiffs);
                var startIndicesOfAllDiffs = differenceIndexConsolidator.GetConsolidatedIndices();

                if (startIndicesOfAllDiffs.Count > maxNumberOfDiffs)
                {
                    output.AppendLine(string.Format("Showing some of the {0} differences", indicesOfAllDiffs.Count));
                    startIndicesOfAllDiffs = startIndicesOfAllDiffs.Take(maxNumberOfDiffs).ToList();
                }

                for (int index = 0; index < startIndicesOfAllDiffs.Count; index++)
                {
                    var startIndexOfDiffString = startIndicesOfAllDiffs[index];
                    var trimmedActualValue = TrimmedValue(actual, startIndexOfDiffString);
                    var trimmedExpectedValue = TrimmedValue(expected, startIndexOfDiffString);

                    var prefixWithDots = startIndexOfDiffString != 0;
                    var suffixWithDots = startIndexOfDiffString + maxDiffLength < maxLengthOfStrings;
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
        }

        private string TrimmedValue(string value, int index)
        {
            if (index >= value.Length) return "";

            if (value.Length >= index + maxDiffLength)
            {
                return value.Substring(index, maxDiffLength);
            }
            return value.Substring(index);
        }

        private List<int> GetIndicesOfAllDifferences(string actualValue, string expectedValue)
        {
            var indicesOfAlldifferences = new List<int>();
            int maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);

            for (int index = 0; index < maxLengthOfStrings; index++)
            {
                if (!CharAtIndexIsEqual(actualValue, expectedValue, index))
                    indicesOfAlldifferences.Add(index);
            }
            return indicesOfAlldifferences;
        }

        private bool CharAtIndexIsEqual(string actual, string expected, int index)
        {
            return index < actual.Length &&
                index < expected.Length &&
                Equals(actual[index], expected[index]);
        }
    }
}
