namespace Shouldly.DifferenceHighlighting;

class FormattedDetailedDifferenceString
{
    private readonly string _actualValue;
    private readonly string _expectedValue;
    private readonly Case _caseSensitivity;
    private readonly int _indexOffset;
    private readonly StringBuilder differenceStringLineOneBuilder;
    private readonly StringBuilder actualCodeStringBuilder;
    private readonly StringBuilder differenceStringLineTwoBuilder;
    private readonly StringBuilder indexStringBuilder;
    private readonly StringBuilder expectedValueStringBuilder;
    private readonly StringBuilder actualValueStringBuilder;
    private readonly StringBuilder expectedCodeStringBuilder;

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

        differenceStringLineOneBuilder = new();
        differenceStringLineTwoBuilder = new();
        indexStringBuilder = new();
        expectedValueStringBuilder = new();
        actualValueStringBuilder = new();
        expectedCodeStringBuilder = new();
        actualCodeStringBuilder = new();
    }

    public override string ToString() =>
        GenerateFormattedString();

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

            differenceStringLineOneBuilder.Append($"{(isEqual ? " " : " | "),-5}");
            differenceStringLineTwoBuilder.Append($"{(isEqual ? " " : @"\|/"),-5}");
            indexStringBuilder.Append($"{index + _indexOffset,-5}");
            expectedValueStringBuilder.Append($"{(index < _expectedValue.Length ? _expectedValue[index].ToSafeString() : ""),-5}");
            actualValueStringBuilder.Append($"{(index < _actualValue.Length ? _actualValue[index].ToSafeString() : ""),-5}");
            expectedCodeStringBuilder.Append($"{(index < _expectedValue.Length ? ((int)_expectedValue[index]).ToString() : ""),-5}");
            actualCodeStringBuilder.Append($"{(index < _actualValue.Length ? ((int)_actualValue[index]).ToString() : ""),-5}");
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

    private string GetFormattedString()
    {
        var output = new StringBuilder();
        output.AppendLine("Difference     | " + differenceStringLineOneBuilder);
        output.AppendLine("               | " + differenceStringLineTwoBuilder);
        output.AppendLine("Index          | " + indexStringBuilder);
        output.AppendLine("Expected Value | " + expectedValueStringBuilder);
        output.AppendLine("Actual Value   | " + actualValueStringBuilder);
        output.AppendLine("Expected Code  | " + expectedCodeStringBuilder);
        output.Append("Actual Code    | " + actualCodeStringBuilder);

        var outputString = output.ToString();
        return outputString;
    }
}