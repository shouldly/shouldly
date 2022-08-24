using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    internal class StringDifferenceHighlighter : IStringDifferenceHighlighter
    {
        private const int maxDiffLength = 21;
        private const int maxNumberOfDiffs = 10;

        private readonly Case _sensitivity;
        private readonly Func<string, string> _transform;

        public StringDifferenceHighlighter(Case sensitivity, Func<string, string>? transform = null)
        {
            _sensitivity = sensitivity;
            _transform = transform ?? (s => s);
        }

        public string? HighlightDifferences(string? expected, string? actual)
        {
            if (expected == null || actual == null) return null;

            expected = _transform(expected);
            actual = _transform(actual);
            var maxLengthOfStrings = Math.Max(actual.Length, expected.Length);

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
                    output.AppendLine($"Showing some of the {indicesOfAllDiffs.Count} differences");
                    startIndicesOfAllDiffs = startIndicesOfAllDiffs.Take(maxNumberOfDiffs).ToList();
                }

                for (var index = 0; index < startIndicesOfAllDiffs.Count; index++)
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
            var indicesOfAllDifferences = new List<int>();
            var maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);

            for (var index = 0; index < maxLengthOfStrings; index++)
            {
                if (!CharAtIndexIsEqual(actualValue, expectedValue, index))
                    indicesOfAllDifferences.Add(index);
            }

            return indicesOfAllDifferences;
        }

        private bool CharAtIndexIsEqual(string actual, string expected, int index)
        {
            return index < actual.Length &&
                index < expected.Length &&
                Equals(actual[index], expected[index]);
        }
    }
}
