using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    internal class StringDifferenceHighlighter : IDifferenceHighlighter
    {
        private int maxDiffLength = 21;

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
            var caseSensitivity = context.CaseSensitivity ?? Case.Sensitive;
            output.AppendLine(string.Format("{0}", caseSensitivity == Case.Insensitive ? "Case Insensitive Comparison" : "Case Sensitive Comparison"));

            if (maxLengthOfStrings < maxDiffLength)
            {
                var formattedDetailedDiffString = new FormattedDetailedDifferenceString(actualValue, expectedValue, caseSensitivity, 0);
                output.Append(formattedDetailedDiffString);
                return output.ToString();
            }
            else
            {
                var indicesOfAllDiffs = GetIndicesOfAllDifferences(actualValue, expectedValue, caseSensitivity);
                var differenceIndexConsolidator = new DifferenceIndexConsolidator(maxDiffLength, maxLengthOfStrings, indicesOfAllDiffs);
                var startIndicesOfAllDiffs = differenceIndexConsolidator.GetConsolidatedIndices(); 

                if (startIndicesOfAllDiffs.Count > 10)
                    startIndicesOfAllDiffs = startIndicesOfAllDiffs.Take(10).ToList();

                foreach (var startIndexOfDiffString in startIndicesOfAllDiffs)
                {

                    string trimmedActualValue = "";
                    string trimmedExpectedValue = "";

                    if (startIndexOfDiffString < actualValue.Length)
                    {
                       if(actualValue.Length >= startIndexOfDiffString + maxDiffLength)
                        {
                            trimmedActualValue = actualValue.Substring(startIndexOfDiffString, maxDiffLength);
                        }
                       else
                       {
                            trimmedActualValue = actualValue.Substring(startIndexOfDiffString);
                       }
                    }

                    if (startIndexOfDiffString < expectedValue.Length)
                    {
                       if(expectedValue.Length >= startIndexOfDiffString + maxDiffLength)
                        {
                            trimmedExpectedValue = expectedValue.Substring(startIndexOfDiffString, maxDiffLength);
                        }
                       else
                       {
                            trimmedExpectedValue = expectedValue.Substring(startIndexOfDiffString);
                       }
                    }

                    var prefixWithDots = startIndexOfDiffString != 0;
                    var suffixWithDots = startIndexOfDiffString + maxDiffLength < maxLengthOfStrings;
                    var formattedDetailedDiffString  = new FormattedDetailedDifferenceString(trimmedActualValue, trimmedExpectedValue, caseSensitivity, startIndexOfDiffString, prefixWithDots, suffixWithDots);
                    output.Append(formattedDetailedDiffString);
                }
                    return output.ToString();
            }
        }

        private List<int> GetIndicesOfAllDifferences(string actualValue, string expectedValue, Case? caseSensitivity)
        {
            var indicesOfAlldifferences = new List<int>();
            int maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);
            bool isEqual;

            for (int index = 0; index < maxLengthOfStrings; index++)
            {
                if (index < actualValue.Length && index < expectedValue.Length)
                {
                    if (caseSensitivity == Case.Insensitive)
                    {
                        isEqual = StringComparer.InvariantCultureIgnoreCase.Equals(actualValue[index].ToString(), expectedValue[index].ToString());
                    }
                    else
                    {
                        isEqual = Equals(actualValue[index], expectedValue[index]);
                    }
                }
                else
                {
                    isEqual = false;
                }

                if (!isEqual)
                    indicesOfAlldifferences.Add(index);
            }

            return indicesOfAlldifferences;
        }

    }
}
