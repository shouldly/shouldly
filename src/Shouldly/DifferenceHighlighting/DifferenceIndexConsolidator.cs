namespace Shouldly.DifferenceHighlighting;

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
                currentConsolidationRun = new();
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
            adjustedIndex = (int)Math.Max(0, averageIndexOfAllDiffs - requiredAdjustmentToCenterDiff);
        }
        else
        {
            adjustedIndex = _maxLengthOfStrings - _maxDiffLength;
        }

        return adjustedIndex;
    }

    private static List<KeyValuePair<int, int>> CalculateDistanceBetweenDiffIndices(List<int> diffIndices)
    {
        var diffToNextIndex = diffIndices.Zip(diffIndices.Skip(1), (i1, i2) => i2 - i1).ToList();

        diffToNextIndex.Add(0);

        return diffIndices.Zip(diffToNextIndex, (i1, i2) => new KeyValuePair<int, int>(i1, i2)).ToList();
    }
}