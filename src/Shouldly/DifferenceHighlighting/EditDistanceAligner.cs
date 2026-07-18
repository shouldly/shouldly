namespace Shouldly.DifferenceHighlighting;

static class EditDistanceAligner
{
    private const int MaxAlignmentSize = 200;

    /// <summary>
    /// Aligns two sequences of grapheme clusters using edit distance, returning
    /// per-cluster edit flags. Each element in the returned bool[] corresponds
    /// to a grapheme cluster (not a UTF-16 char).
    /// </summary>
    internal static (bool[]? expectedEdits, bool[]? actualEdits) AlignClusters(
        string[] expected, string[] actual, Case caseSensitivity)
    {
        var m = expected.Length;
        var n = actual.Length;

        if (m > MaxAlignmentSize || n > MaxAlignmentSize)
            return (null, null);

        var dp = new int[m + 1, n + 1];
        for (var i = 0; i <= m; i++) dp[i, 0] = i;
        for (var j = 0; j <= n; j++) dp[0, j] = j;

        for (var i = 1; i <= m; i++)
        {
            for (var j = 1; j <= n; j++)
            {
                var substituteCost = GraphemeClusterHelper.ClustersEqual(
                    expected[i - 1], actual[j - 1], caseSensitivity) ? 0 : 1;
                dp[i, j] = Math.Min(
                    Math.Min(
                        dp[i - 1, j] + 1,
                        dp[i, j - 1] + 1),
                    dp[i - 1, j - 1] + substituteCost);
            }
        }

        var expectedEdits = new bool[m];
        var actualEdits = new bool[n];

        var bi = m;
        var bj = n;
        while (bi > 0 || bj > 0)
        {
            if (bi > 0 && bj > 0)
            {
                var substituteCost = GraphemeClusterHelper.ClustersEqual(
                    expected[bi - 1], actual[bj - 1], caseSensitivity) ? 0 : 1;

                if (dp[bi, bj] == dp[bi - 1, bj - 1] + substituteCost)
                {
                    if (substituteCost == 1)
                    {
                        expectedEdits[bi - 1] = true;
                        actualEdits[bj - 1] = true;
                    }
                    bi--;
                    bj--;
                    continue;
                }
            }

            if (bi > 0 && dp[bi, bj] == dp[bi - 1, bj] + 1)
            {
                expectedEdits[bi - 1] = true;
                bi--;
            }
            else
            {
                actualEdits[bj - 1] = true;
                bj--;
            }
        }

        return (expectedEdits, actualEdits);
    }
}
