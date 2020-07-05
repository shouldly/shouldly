using System;
using System.Text;

namespace Shouldly.DifferenceHighlighting
{
    internal class FormattedDetailedDifferenceString 
    {
        private readonly string _actualValue;
        private readonly string _expectedValue;
        private readonly Case _caseSensitivity;
        private readonly int _indexOffset;
        private StringBuilder differenceStringLineOneBuilder;
        private StringBuilder actualCodeStringBuilder;
        private StringBuilder differenceStringLineTwoBuilder;
        private StringBuilder indexStringBuilder;
        private StringBuilder expectedValueStringBuilder;
        private StringBuilder actualValueStringBuilder;
        private StringBuilder expectedCodeStringBuilder;

        private readonly bool _prefixWithDots;
        private readonly bool _suffixWithDots;

        internal FormattedDetailedDifferenceString(string actualValue, string expectedValue, Case? caseSensitivity, int indexOffset, bool prefixWithDots = false, bool suffixWithDots = false)
        {
            _actualValue = actualValue;
            _expectedValue = expectedValue;
            _caseSensitivity = caseSensitivity ?? Case.Sensitive;
            _indexOffset = indexOffset;
            _prefixWithDots = prefixWithDots;
            _suffixWithDots = suffixWithDots;

            differenceStringLineOneBuilder = new StringBuilder();
            differenceStringLineTwoBuilder = new StringBuilder();
            indexStringBuilder = new StringBuilder();
            expectedValueStringBuilder = new StringBuilder();
            actualValueStringBuilder = new StringBuilder();
            expectedCodeStringBuilder = new StringBuilder();
            actualCodeStringBuilder = new StringBuilder();
        }

        public override string ToString()
        {
            return GenerateFormattedString();
        }

        public string GenerateFormattedString()
        {
            var maxLengthOfStrings = Math.Max(_actualValue.Length, _expectedValue.Length);
            var minLenOfStrings = Math.Min(_actualValue.Length, _expectedValue.Length);

            if (_prefixWithDots)
            {
                AddDots();
            }

            for (var index = 0; index < maxLengthOfStrings; index++)
            {
                var isEqual = CheckEquality(index, minLenOfStrings);

                differenceStringLineOneBuilder.Append(string.Format("{0,-5}", isEqual ? " " : @" | " ));
                differenceStringLineTwoBuilder.Append(string.Format("{0,-5}", isEqual ? " " : @"\|/" ));
                indexStringBuilder.Append(string.Format("{0,-5}", index + _indexOffset));
                expectedValueStringBuilder.Append(string.Format("{0,-5}", index < _expectedValue.Length ? _expectedValue[index].ToSafeString() : ""));
                actualValueStringBuilder.Append(string.Format("{0,-5}", index < _actualValue.Length ? _actualValue[index].ToSafeString() : ""));
                expectedCodeStringBuilder.Append(string.Format("{0,-5}", index < _expectedValue.Length ? ((int)_expectedValue[index]).ToString() : ""));
                actualCodeStringBuilder.Append(string.Format("{0,-5}", index < _actualValue.Length ? ((int)_actualValue[index]).ToString() : ""));
            }

            if (_suffixWithDots)
            {
                AddDots();
            }

            return GetFormattedString();
        }

        private void AddDots()
        {
            differenceStringLineOneBuilder.Append("     ");
            differenceStringLineTwoBuilder.Append("     ");
            indexStringBuilder.Append("...  ");
            expectedValueStringBuilder.Append("...  ");
            actualValueStringBuilder.Append("...  ");
            expectedCodeStringBuilder.Append("...  ");
            actualCodeStringBuilder.Append("...  ");
        }

        private bool CheckEquality(int index, int minLengthOfStrings)
        {
            var isEqual = false;
            if (index < minLengthOfStrings)
            {
                if (_caseSensitivity == Case.Insensitive)
                {
                    isEqual = StringComparer.OrdinalIgnoreCase.Equals(_actualValue[index].ToString(), _expectedValue[index].ToString());
                }
                else
                {
                    isEqual = Equals(_actualValue[index], _expectedValue[index]);
                }
            }
            return isEqual;
        }

        string GetFormattedString()
        {
            var output = new StringBuilder();
            output.AppendLine("Difference     | " + differenceStringLineOneBuilder);
            output.AppendLine("               | " + differenceStringLineTwoBuilder);
            output.AppendLine("Index          | " + indexStringBuilder);
            output.AppendLine("Expected Value | " + expectedValueStringBuilder);
            output.AppendLine("Actual Value   | " + actualValueStringBuilder);
            output.AppendLine("Expected Code  | " + expectedCodeStringBuilder);
            output.Append    ("Actual Code    | " + actualCodeStringBuilder);

            var outputString = output.ToString();
            return outputString;
        }
    }
}
