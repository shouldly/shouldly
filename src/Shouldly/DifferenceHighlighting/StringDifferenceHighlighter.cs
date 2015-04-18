using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    internal class StringDifferenceHighlighter : IDifferenceHighlighter
    {
        private int maxDiffLength = 20;

        public bool CanProcess(IShouldlyAssertionContext context)
        {
            return context.Expected != null && 
                   context.Actual != null && 
                   context.Expected is string && 
                   context.Actual is string && 
                   context.ShouldMethod == "ShouldBe";
        }

        public string HighlightDifferences(IShouldlyAssertionContext context)
        {
            var actualValue = context.Actual as string;
            var expectedValue = context.Expected as string;
            int maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);

            var output = new StringBuilder();

            if (maxLengthOfStrings <= maxDiffLength)
            {
                var sdf = new StringDifferenceFormatter(actualValue, expectedValue, context.CaseSensitivity, 0);
                output.AppendLine(string.Format("{0}", context.CaseSensitivity == Case.Insensitive ? "Case Insensitive Comparison" : "Case Sensitive Comparison"));
                output.Append(sdf.ToString());
                return output.ToString();
            }
            else
            {
                var startIndexOfDiffString = ConsolidatedIndicesOfDifferencesToStartIndexOfDiffString(actualValue, expectedValue, context.CaseSensitivity).First();

                var trimmedActualValue = actualValue.Substring(startIndexOfDiffString, maxDiffLength + 1);
                var trimmedExpectedValue = expectedValue.Substring(startIndexOfDiffString, maxDiffLength + 1);

                var prefixWithDots = startIndexOfDiffString != 0;
                var suffixWithDots = startIndexOfDiffString + maxDiffLength + 1 < maxLengthOfStrings;
                var sdf = new StringDifferenceFormatter(trimmedActualValue, trimmedExpectedValue, context.CaseSensitivity, startIndexOfDiffString, prefixWithDots, suffixWithDots);
                output.AppendLine(string.Format("{0}", context.CaseSensitivity == Case.Insensitive ? "Case Insensitive Comparison" : "Case Sensitive Comparison"));
                output.Append(sdf.ToString());
                return output.ToString();
            }
        }

        private List<int> ConsolidatedIndicesOfDifferencesToStartIndexOfDiffString(string actualValue, string expectedValue, Case caseSensitivity)
        {
            var trimmedIndicesOfDifference = new List<int>();
            int maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);

            var indicesOfAllDifferences = GetIndicesOfAllDifferences(actualValue, expectedValue, caseSensitivity);

            foreach (var indexOfDifference in indicesOfAllDifferences)
            {
                if (!IsIndexOfDifferencesAlreadyAccountedFor(trimmedIndicesOfDifference, indexOfDifference))
                {
                    var trimmedStartOfDifference = Math.Max(0, indexOfDifference - maxDiffLength / 2);
                    trimmedStartOfDifference = Math.Min(trimmedStartOfDifference, maxLengthOfStrings - (maxDiffLength + 1));

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
                if (currentIndex >= lastTrimmedIndexOfDifference && currentIndex <= lastTrimmedIndexOfDifference + maxDiffLength)
                {
                    return true;
                }
            }

            return false;
        }

        private List<int> GetIndicesOfAllDifferences(string actualValue, string expectedValue, Case caseSensitivity)
        {
            // TODO: Refactor?
            var indicesOfAlldifferences = new List<int>();
            int maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);
            int minLenOfStrings = Math.Min(actualValue.Length, expectedValue.Length);
            bool isEqual;

            for (int index = 0; index < maxLengthOfStrings; index++)
            {
                if (caseSensitivity == Case.Insensitive)
                {
                    isEqual = StringComparer.InvariantCultureIgnoreCase.Equals(actualValue[index].ToString(), expectedValue[index].ToString());
                }
                else
                {
                    isEqual = Equals(actualValue[index], expectedValue[index]);
                }

                if (!isEqual)
                    indicesOfAlldifferences.Add(index);
            }

            return indicesOfAlldifferences;
        }

    }
}
