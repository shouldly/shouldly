using System;
using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    internal class StringDifferenceHighlighter : IDifferenceHighlighter
    {
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

            var indexStringBuilder = new StringBuilder();
            var expectedValueStringBuilder = new StringBuilder();
            var actualValueStringBuilder = new StringBuilder();
            var expectedCodeStringBuilder = new StringBuilder();
            var actualCodeStringBuilder = new StringBuilder();
            var differenceStringLineOneBuilder = new StringBuilder();
            var differenceStringLineTwoBuilder = new StringBuilder();

            int maxLengthOfStrings = Math.Max(actualValue.Length, expectedValue.Length);
            int minLenOfStrings = Math.Min(actualValue.Length, expectedValue.Length);

            for (int index = 0; index < maxLengthOfStrings; index++)
            {
                var isEqual = CheckEquality(context.CaseSensitivity, index, minLenOfStrings, actualValue, expectedValue);

                differenceStringLineOneBuilder.Append(string.Format("{0,-5}", isEqual ? " " : @" | " ));
                differenceStringLineTwoBuilder.Append(string.Format("{0,-5}", isEqual ? " " : @"\|/" ));
                indexStringBuilder.Append(string.Format("{0,-5}", index));
                expectedValueStringBuilder.Append(string.Format("{0,-5}", index < expectedValue.Length ? expectedValue[index].ToSafeString() : ""));
                actualValueStringBuilder.Append(string.Format("{0,-5}", index < actualValue.Length ? actualValue[index].ToSafeString() : ""));
                expectedCodeStringBuilder.Append(string.Format("{0,-5}", index < expectedValue.Length ? ((int)expectedValue[index]).ToString() : ""));
                actualCodeStringBuilder.Append(string.Format("{0,-5}", index < actualValue.Length ? ((int)actualValue[index]).ToString() : ""));
            }

            var outputString = GenerateDifferenceString(context.CaseSensitivity, 
                                                        differenceStringLineOneBuilder, differenceStringLineTwoBuilder, indexStringBuilder, 
                                                        expectedValueStringBuilder, actualValueStringBuilder, expectedCodeStringBuilder, 
                                                        actualCodeStringBuilder);
            return outputString;

        }

        private bool CheckEquality(Case caseSensitivity, int index, int minLengthOfStrings, string actualValue, string expectedValue)
        {
            bool isEqual = false;
            if (index < minLengthOfStrings)
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
            return isEqual;
        }

        private string GenerateDifferenceString(Case caseSensitivity, 
                                                StringBuilder differenceStringLineOneBuilder, StringBuilder differenceStringLineTwoBuilder, StringBuilder indexStringBuilder, 
                                                StringBuilder expectedValueStringBuilder, StringBuilder actualValueStringBuilder, StringBuilder expectedCodeStringBuilder, 
                                                StringBuilder actualCodeStringBuilder)
        {
            var output = new StringBuilder();
            output.AppendLine(string.Format("{0}", caseSensitivity == Case.Insensitive ? "Case Insensitive Comparison" : "Case Sensitive Comparison"));
            output.AppendLine("Difference     | " + differenceStringLineOneBuilder);
            output.AppendLine("               | " + differenceStringLineTwoBuilder);
            output.AppendLine("Index          | " + indexStringBuilder);
            output.AppendLine("Expected Value | " + expectedValueStringBuilder);
            output.AppendLine("Actual Value   | " + actualValueStringBuilder);
            output.AppendLine("Expected Code  | " + expectedCodeStringBuilder);
            output.AppendLine("Actual Code    | " + actualCodeStringBuilder);
            output.AppendLine();

            var outputString = output.ToString();
            return outputString;
        }
    }
}
