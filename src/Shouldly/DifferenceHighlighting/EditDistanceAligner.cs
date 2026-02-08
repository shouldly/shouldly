namespace Shouldly.DifferenceHighlighting;

static class EditDistanceAligner
{
    internal static (bool[] expectedEdits, bool[] actualEdits) Align(
        string expected, string actual, Case caseSensitivity)
    {
        var m = expected.Length;
        var n = actual.Length;

        // Build DP matrix
        var dp = new int[m + 1, n + 1];
        for (var i = 0; i <= m; i++) dp[i, 0] = i;
        for (var j = 0; j <= n; j++) dp[0, j] = j;

        for (var i = 1; i <= m; i++)
        {
            for (var j = 1; j <= n; j++)
            {
                var substituteCost = CharsEqual(expected[i - 1], actual[j - 1], caseSensitivity) ? 0 : 1;
                dp[i, j] = Math.Min(
                    Math.Min(
                        dp[i - 1, j] + 1,      // delete from expected
                        dp[i, j - 1] + 1),      // insert into actual
                    dp[i - 1, j - 1] + substituteCost); // match or substitute
            }
        }

        // Backtrace to recover alignment
        var expectedEdits = new bool[m];
        var actualEdits = new bool[n];

        var bi = m;
        var bj = n;
        while (bi > 0 || bj > 0)
        {
            if (bi > 0 && bj > 0)
            {
                var substituteCost = CharsEqual(expected[bi - 1], actual[bj - 1], caseSensitivity) ? 0 : 1;

                if (dp[bi, bj] == dp[bi - 1, bj - 1] + substituteCost)
                {
                    // Match or substitute
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
                // Delete from expected
                expectedEdits[bi - 1] = true;
                bi--;
            }
            else
            {
                // Insert into actual
                actualEdits[bj - 1] = true;
                bj--;
            }
        }

        return (expectedEdits, actualEdits);
    }

    private static bool CharsEqual(char a, char b, Case caseSensitivity)
    {
        if (caseSensitivity == Case.Insensitive)
            return StringComparer.OrdinalIgnoreCase.Equals(a.ToString(), b.ToString());
        return a == b;
    }
}
